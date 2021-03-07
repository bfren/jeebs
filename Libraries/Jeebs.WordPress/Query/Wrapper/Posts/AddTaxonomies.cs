// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.WordPress.Enums;
using Jm.WordPress.Query.Wrapper.Posts;

namespace Jeebs.WordPress
{
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Add Taxonomies to posts
		/// </summary>
		/// <typeparam name="TList">List type</typeparam>
		/// <typeparam name="TModel">Post type</typeparam>
		/// <param name="r">Result</param>
		private async Task<IR<TList>> AddTaxonomiesAsync<TList, TModel>(IOkV<TList> r)
			where TList : List<TModel>
			where TModel : IEntity
		{
			// Only proceed if there is at least one term list in this model
			var termLists = GetTermLists<TModel>();
			if (termLists.Count == 0)
			{
				return r;
			}

			return r
				.Link()
					.Handle().With<GetTermsExceptionMsg>()
					.MapAsync(okV => getTermsAsync(okV, termLists)).Await()
				.Link()
					.Handle().With<SetTermsExceptionMsg>()
					.Map(okV => addTerms(okV, termLists));

			//
			//	Get terms
			//
			async Task<IR<(TList, List<Term>)>> getTermsAsync(IOkV<TList> r, List<PropertyInfo> termLists)
			{
				// Create options
				var options = new QueryPostsTaxonomy.Options
				{
					PostIds = r.Value.Select(p => p.Id).ToList()
				};

				// Add each taxonomy to the query options
				var firstPost = r.Value[0];
				foreach (var info in termLists)
				{
					// Get taxonomy
					var taxonomy = new PropertyInfo<TModel, TermList>(info).Get(firstPost).Taxonomy;

					// Make sure taxonomy has been registered
					if (!Taxonomy.IsRegistered(taxonomy))
					{
						return r.Error<(TList, List<Term>)>().AddMsg(new TaxonomyNotRegisteredMsg(taxonomy));
					}

					// Add to query
					options.Taxonomies.Add(taxonomy);
				}

				// Build query
				var query = StartNewQuery()
					.WithModel<Term>()
					.WithOptions(options)
					.WithParts(new QueryPostsTaxonomy.Builder<Term>(db))
					.GetQuery();

				// Execute query
				return (await query.ExecuteQueryAsync(r).ConfigureAwait(false)).Switch(
					x => x.OkV((r.Value, x.Value))
				);
			}

			//
			//	Add terms
			//
			static IR<TList> addTerms(IOkV<(TList, List<Term>)> r, List<PropertyInfo> termLists)
			{
				var (posts, terms) = r.Value;

				// Now add the terms to each post
				foreach (var post in posts)
				{
					foreach (var info in termLists)
					{
						// Get PropertyInfo<> for the TermList
						var list = new PropertyInfo<TModel, TermList>(info).Get(post);

						// Get terms
						var termsForThisPost = from t in terms
											   where t.PostId == post.Id
											   && t.Taxonomy == list.Taxonomy
											   select (TermList.Term)t;

						// Add terms to post
						if (!termsForThisPost.Any())
						{
							continue;
						}

						list.AddRange(termsForThisPost);
					}
				}

				return r.OkV(posts);
			}
		}

		private class Term : TermList.Term
		{
			/// <summary>
			/// Enables query for multiple posts and multiple taxonomies
			/// </summary>
			public long PostId { get; set; }

			/// <summary>
			/// Enables query for multiple posts and multiple taxonomies
			/// </summary>
			public Taxonomy Taxonomy { get; set; } = Taxonomy.Blank;
		}
	}
}
