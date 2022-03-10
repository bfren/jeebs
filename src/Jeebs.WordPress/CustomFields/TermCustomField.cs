// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.Messages;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Entities.StrongIds;
using Maybe;
using Maybe.Functions;

namespace Jeebs.WordPress.CustomFields;

/// <summary>
/// Term Taxonomy Custom Field
/// </summary>
public abstract class TermCustomField : CustomField<TermCustomField.Term>
{
	/// <summary>
	/// IQueryTerms
	/// </summary>
	protected IQueryTerms QueryTerms { get; private init; }

	/// <inheritdoc cref="CustomField{T}.CustomField(string, T)"/>
	protected TermCustomField(string key) : this(new Query.Terms(), key) { }

	/// <summary>
	/// Create object from terms
	/// </summary>
	/// <param name="queryTerms">IQueryTerms</param>
	/// <param name="key">Meta key (for post_meta table)</param>
	protected TermCustomField(IQueryTerms queryTerms, string key) : base(key, new Term()) =>
		QueryTerms = queryTerms;

	/// <inheritdoc/>
	public override Task<Maybe<bool>> HydrateAsync(IWpDb db, IUnitOfWork w, MetaDictionary meta, bool isRequired)
	{
		// First, get the Term ID from the meta dictionary
		// If meta doesn't contain the key and this is a required field, return failure
		// Otherwise return success
		if (meta.TryGetValue(Key, out var value) && !string.IsNullOrWhiteSpace(value))
		{
			ValueStr = value;
		}
		else
		{
			if (isRequired)
			{
				return MaybeF.None<bool>(new M.MetaKeyNotFoundMsg(GetType(), Key)).AsTask;
			}

			return MaybeF.False.AsTask;
		}

		// If we're here we have a Term ID, so get it and hydrate the custom field
		return
			MaybeF.Some(
				ValueStr
			)
			.Bind(
				x => ParseTermId(GetType(), x)
			)
			.BindAsync(
				x => QueryTerms.ExecuteAsync<Term>(db, w, opt => opt with { Id = x })
			)
			.UnwrapAsync(
				x => x.SingleValue<Term>(
					tooMany: () => new M.MultipleTermsFoundMsg(ValueStr)
				)
			)
			.MapAsync(
				x =>
				{
					ValueObj = x;
					return true;
				},
				MaybeF.DefaultHandler
			);
	}

	/// <summary>
	/// Parse the Term ID
	/// </summary>
	/// <param name="type">Term Custom Field type</param>
	/// <param name="value">Term ID value</param>
	internal static Maybe<WpTermId> ParseTermId(Type type, string value)
	{
		if (!long.TryParse(value, out var termId))
		{
			return MaybeF.None<WpTermId>(new M.ValueIsInvalidTermIdMsg(type, value));
		}

		return new WpTermId(termId);
	}

	/// <summary>
	/// Return Term Title
	/// </summary>
	protected override string GetValueAsString() =>
		ValueObj.Title;

	internal string GetValueAsStringTest() =>
		GetValueAsString();

	/// <summary>
	/// Term class
	/// </summary>
	public sealed record class Term : WpTermEntity { }

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>Meta key not found in MetaDictionary</summary>
		/// <param name="Type">Custom Field type</param>
		/// <param name="Value">Meta Key</param>
		public sealed record class MetaKeyNotFoundMsg(Type Type, string Value) : WithValueMsg<string> { }

		/// <summary>Multiple matching terms were found (should always be 1)</summary>
		/// <param name="Value">Term ID</param>
		public sealed record class MultipleTermsFoundMsg(string Value) : WithValueMsg<string> { }

		/// <summary>The value in the meta dictionary is not a valid ID</summary>
		/// <param name="Type">Custom Field type</param>
		/// <param name="Value">Meta Key</param>
		public sealed record class ValueIsInvalidTermIdMsg(Type Type, string Value) : WithValueMsg<string> { }
	}
}
