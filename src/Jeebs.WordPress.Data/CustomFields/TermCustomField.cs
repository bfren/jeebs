// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.WordPress.Data.Entities;
using F.WordPressF.DataF;
using static F.OptionF;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// Term Taxonomy Custom Field
	/// </summary>
	public abstract record TermCustomField : CustomField<TermCustomField.Term>
	{
		/// <inheritdoc/>
		protected TermCustomField(string key, bool isRequired = false) : base(key, new Term(), isRequired) { }

		/// <inheritdoc/>
		public override Task<Option<bool>> HydrateAsync(IWpDb db, MetaDictionary meta)
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
				if (IsRequired)
				{
					return None<bool>(new Msg.MetaKeyNotFoundMsg(GetType(), Key)).AsTask;
				}

				return False.AsTask;
			}

			// If we're here we have an Attachment Post ID, so get it and hydrate the custom field
			return
				Return(
					ValueStr
				)
				.Bind(
					x => ParseTermId(GetType(), x)
				)
				.BindAsync(
					x => GetTerms(db, x)
				)
				.UnwrapAsync(
					x => x.Single<Term>(tooMany: () => new Msg.MultipleTermsFoundMsg(ValueStr))
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
			if (!long.TryParse(value, out var termId))
			{
				return None<WpTermId>(new Msg.ValueIsInvalidTermIdMsg(type, value));
			}

			return new WpTermId(termId);
		}

		/// <summary>
		/// Get the Term by ID
		/// </summary>
		/// <param name="db">IWpDb</param>
		/// <param name="termId">Term ID</param>
		internal static Task<Option<IEnumerable<Term>>> GetTerms(IWpDb db, WpTermId termId) =>
			QueryTermsF.ExecuteAsync<Term>(db, opt => opt with { Id = termId });

		/// <summary>
		/// Return term Title
		/// </summary>
		public override string ToString() =>
			ValueObj?.Title ?? base.ToString();

		/// <summary>
		/// Term class
		/// </summary>
		public sealed record Term : WpTermEntity { }

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Meta key not found in MetaDictionary</summary>
			/// <param name="Type">Custom Field type</param>
			/// <param name="Value">Meta Key</param>
			public sealed record MetaKeyNotFoundMsg(Type Type, string Value) : WithValueMsg<string> { }

			/// <summary>Multiple matching terms were found (should always be 1)</summary>
			/// <param name="Value">Term ID</param>
			public sealed record MultipleTermsFoundMsg(string Value) : WithValueMsg<string> { }

			/// <summary>The value in the meta dictionary is not a valid ID</summary>
			/// <param name="Type">Custom Field type</param>
			/// <param name="Value">Meta Key</param>
			public sealed record ValueIsInvalidTermIdMsg(Type Type, string Value) : WithValueMsg<string> { }
		}
	}
}
