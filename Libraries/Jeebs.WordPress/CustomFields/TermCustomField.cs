// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.WordPress.Entities;
using static F.OptionF;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Term Taxonomy Custom Field
	/// </summary>
	public abstract class TermCustomField : CustomField<TermCustomField.Term>
	{
		/// <inheritdoc/>
		protected TermCustomField(string key, bool isRequired = false) : base(key, new Term(), isRequired) { }

		/// <inheritdoc/>
		public override Task<Option<bool>> HydrateAsync(IWpDb db, IUnitOfWork unitOfWork, MetaDictionary meta)
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
			return Return(ValueStr)
				.Bind(
					parseTermId
				)
				.BindAsync(
					getTerms
				)
				.UnwrapAsync(
					x => x.Single<Term>(tooMany: () => new Msg.MultipleTermsFoundMsg(ValueStr))
				)
				.MapAsync(
					hydrate,
					DefaultHandler
				);

			//
			// Parse the Term ID
			//
			Option<long> parseTermId(string value)
			{
				if (!long.TryParse(value, out var termId))
				{
					return None<long>(new Msg.ValueIsInvalidTermIdMsg(GetType(), value));
				}

				return termId;
			}

			//
			// Get the Term by ID
			//
			Task<Option<List<Term>>> getTerms(long termId)
			{
				// Create new query
				using var w = db.GetQueryWrapper();

				// Get matching terms
				return w.QueryTaxonomyAsync<Term>(modify: opt => opt.Id = termId);
			}

			//
			// Hydrate the custom field using Term info
			//
			bool hydrate(Term term)
			{
				// Get term
				ValueObj = term;
				return true;
			}
		}

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
