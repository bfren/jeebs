using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.WordPress.Entities;
using Jm.WordPress.CustomFields;
using Jm.WordPress.CustomFields.Term;

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
		public override async Task<IR<bool>> HydrateAsync(IOk r, IWpDb db, IUnitOfWork unitOfWork, MetaDictionary meta)
		{
			// First, get the Term ID from the meta dictionary
			// If meta doesn't contain the key and this is a required field, return failure
			// Otherwise return success
			if (meta.ContainsKey(Key))
			{
				ValueStr = meta[Key];
			}
			else
			{
				if (IsRequired)
				{
					return r.False(new MetaKeyNotFoundMsg(Key));
				}

				return r.True();
			}

			// If we're here we have an Attachment Post ID, so get it and hydrate the custom field
			return await r
				.Link()
					.Handle().With<ParseTermIdExceptionMsg>()
					.Map(parseTermId)
				.Link()
					.Handle().With<GetTermExceptionMsg>()
					.MapAsync(getTerm).Await()
				.Link()
					.Handle().With<HydrateExceptionMsg>()
					.MapAsync(hydrate);

			//
			// Parse the Term ID
			//
			IR<long> parseTermId(IOk r)
			{
				if (!long.TryParse(ValueStr, out var termId))
				{
					return r.Error<long>().AddMsg(new ValueIsInvalidPostIdMsg(ValueStr));
				}

				return r.OkV(termId);
			}

			//
			// Get the Term by ID
			//
			async Task<IR<Term>> getTerm(IOkV<long> r)
			{
				// Create new query
				using var w = db.GetQueryWrapper();

				// Get matching terms
				var terms = await w.QueryPostsAsync<Term>(r, modify: opt => opt.Id = r.Value);

				// If there is more than one term, return an error
				return terms switch
				{
					IOkV<List<Term>> x when x.Value.Count == 1 => x.OkV(x.Value.Single()),
					{ } x => x.Error<Term>().AddMsg().OfType<MultipleTermsFoundMsg>()
				};
			}

			//
			// Hydrate the custom field using Term info
			//
			async Task<IR<bool>> hydrate(IOkV<Term> r)
			{
				// Get term
				ValueObj = r.Value;
				return r.True();
			}
		}

		/// <summary>
		/// Return term Title
		/// </summary>
		public override string ToString() 
			=> ValueObj?.Title ?? base.ToString();

		/// <summary>
		/// Term class
		/// </summary>
		public sealed class Term : WpTermEntity { }
	}
}
