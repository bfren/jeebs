// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Querying;
using Jeebs.WordPress.Enums;
using static F.OptionF;

namespace Jeebs.WordPress
{
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Add Taxonomies to posts
		/// </summary>
		/// <typeparam name="TList">List type</typeparam>
		/// <typeparam name="TModel">Post type</typeparam>
		/// <param name="posts">Posts</param>
		private Task<Option<TList>> AddTaxonomiesAsync<TList, TModel>(TList posts)
			where TList : List<TModel>
			where TModel : IEntity
		{
			// Only proceed if there is at least one term list in this model
			var termLists = GetTermLists<TModel>();
			if (termLists.Count == 0)
			{
				return Return(posts).AsTask;
			}

			return Return(posts)
				.BindAsync(
					x => getTermsAsync(x, termLists)
				)
				.MapAsync(
					x => addTerms(x, termLists),
					e => new Msg.AddTaxonomiesExceptionMsg<TModel>(e)
				);

			//
			//	Get terms
			//
			Task<Option<(TList, List<Term>)>> getTermsAsync(TList posts, List<PropertyInfo> termLists)
			{
				return
					Return(
						getOptions,
						DefaultHandler
					)
					.Bind(
						addTaxonomies
					)
					.Bind(
						getQuery
					)
					.BindAsync(
						getTaxonomies
					)
					.MapAsync(
						createTuple,
						DefaultHandler
					);

				// Get query options
				QueryPostsTaxonomy.Options getOptions() =>
					new() { PostIds = posts.Select(p => p.Id).ToList() };

				// Add each taxonomy to the query options
				Option<QueryPostsTaxonomy.Options> addTaxonomies(QueryPostsTaxonomy.Options options)
				{
					var firstPost = posts[0];
					var taxonomies = new List<Taxonomy>();
					foreach (var info in termLists)
					{
						// Get taxonomy
						var taxonomy = new PropertyInfo<TModel, TermList>(info).Get(firstPost).Taxonomy;

						// Make sure taxonomy has been registered
						if (!Taxonomy.IsRegistered(taxonomy))
						{
							return None<QueryPostsTaxonomy.Options>(new Msg.TaxonomyNotRegisteredMsg(taxonomy));
						}

						// Add to query
						options.Taxonomies.Add(taxonomy);
					}

					return options;
				}

				// Build query
				Option<IQuery<Term>> getQuery(QueryPostsTaxonomy.Options options) =>
					Return(
						() => StartNewQuery()
								.WithModel<Term>()
								.WithOptions(options)
								.WithParts(new QueryPostsTaxonomy.Builder<Term>(db))
								.GetQuery(),
						e => new Msg.GetTaxonomyQueryExceptionMsg<TModel>(e)
					);

				// Execute query to get taxonomies
				Task<Option<List<Term>>> getTaxonomies(IQuery<Term> query) =>
					query.ExecuteQueryAsync();

				// Create tuple of posts and taxonomies
				(TList, List<Term>) createTuple(List<Term> meta) =>
					(posts, meta);
			}

			//
			//	Add terms
			//
			static TList addTerms((TList, List<Term>) lists, List<PropertyInfo> termLists)
			{
				var (posts, terms) = lists;

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

				return posts;
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

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>An exception occured while adding Taxonomies to posts</summary>
			/// <typeparam name="T">Post Model type</typeparam>
			/// <param name="Exception">Exception object</param>
			public sealed record AddTaxonomiesExceptionMsg<T>(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>An exception occured while getting terms for posts</summary>
			/// <typeparam name="T">Post Model type</typeparam>
			/// <param name="Exception">Exception object</param>
			public sealed record GetTermsExceptionMsg<T>(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>An exception occured while getting terms for posts</summary>
			/// <typeparam name="T">Post Model type</typeparam>
			/// <param name="Exception">Exception object</param>
			public sealed record GetTaxonomyQueryExceptionMsg<T>(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>The taxonomy being added has not been registered with <see cref="IWp{TConfig}"/></summary>
			/// <param name="Value">Taxonomy</param>
			public sealed record TaxonomyNotRegisteredMsg(Taxonomy Value) : WithValueMsg<Taxonomy> { }
		}
	}
}
