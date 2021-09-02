// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.WordPress.Data.Entities;
using static F.OptionF;

namespace Jeebs.WordPress.Data;

/// <summary>
/// Term Taxonomy Custom Field
/// </summary>
public abstract class TermCustomField : CustomField<TermCustomField.Term>
{
	/// <summary>
	/// IQueryTerms
	/// </summary>
	protected IQueryTerms QueryTerms { get; private init; }

	/// <inheritdoc/>
	protected TermCustomField(string key) : this(new Query.Terms(), key) { }

	internal TermCustomField(IQueryTerms queryTerms, string key) : base(key, new Term()) =>
		QueryTerms = queryTerms;

	/// <inheritdoc/>
	public override Task<Option<bool>> HydrateAsync(IWpDb db, IUnitOfWork w, MetaDictionary meta, bool isRequired)
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
				return None<bool>(new Msg.MetaKeyNotFoundMsg(GetType(), Key)).AsTask;
			}

			return False.AsTask;
		}

		// If we're here we have a Term ID, so get it and hydrate the custom field
		return
			Some(
				ValueStr
			)
			.Bind(
				x => ParseTermId(GetType(), x)
			)
			.BindAsync(
				x => QueryTerms.ExecuteAsync<Term>(db, w, opt => opt with { Id = x })
			)
			.UnwrapAsync(
				x => x.Single<Term>(
					tooMany: () => new Msg.MultipleTermsFoundMsg(ValueStr)
				)
			)
			.MapAsync(
				x =>
				{
					ValueObj = x;
					return true;
				},
				DefaultHandler
			);
	}

	/// <summary>
	/// Parse the Term ID
	/// </summary>
	/// <param name="type">Term Custom Field type</param>
	/// <param name="value">Term ID value</param>
	internal static Option<WpTermId> ParseTermId(Type type, string value)
	{
		if (!ulong.TryParse(value, out ulong termId))
		{
			return None<WpTermId>(new Msg.ValueIsInvalidTermIdMsg(type, value));
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
	public static class Msg
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
