// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Data;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;
using static F.OptionF;

namespace F.WordPressF.DataF
{
	public static partial class QueryPostsF
	{
		/// <summary>
		/// Add Taxonomies to posts
		/// </summary>
		/// <typeparam name="TList">List type</typeparam>
		/// <typeparam name="TModel">Post type</typeparam>
		/// <param name="db">IWpDb</param>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="posts">Posts</param>
		internal static Task<Option<TList>> AddTaxonomiesAsync<TList, TModel>(IWpDb db, IUnitOfWork w, TList posts)
			where TList : IEnumerable<TModel>
			where TModel : IWithId<WpPostId>
		{
			// If there are no posts, do nothing
			if (!posts.Any())
			{
				return Return(posts).AsTask;
			}

			// Only proceed if there is at least one term list in this model
			var termLists = GetTermLists<TModel>();
			if (termLists.Count == 0)
			{
				return Return(posts).AsTask;
			}

			// Get terms and add them to the posts
			return QueryPostsTaxonomyF
				.ExecuteAsync<Term>(db, w, opt => opt with
				{
					PostIds = posts.Select(p => p.Id).ToImmutableList()
				})
				.BindAsync(
					x => SetTaxonomies<TList, TModel>(posts, x, termLists)
				);
		}

		/// <summary>
		/// Set Taxonomies for each post
		/// </summary>
		/// <typeparam name="TList">List type</typeparam>
		/// <typeparam name="TModel">Post type</typeparam>
		/// <param name="posts">Posts</param>
		/// <param name="terms">Terms</param>
		/// <param name="termLists">Term List properties</param>
		internal static Option<TList> SetTaxonomies<TList, TModel>(TList posts, IEnumerable<Term> terms, List<PropertyInfo> termLists)
			where TList : IEnumerable<TModel>
			where TModel : IWithId<WpPostId>
		{
			foreach (var post in posts)
			{
				foreach (var info in termLists)
				{
					// Get PropertyInfo<> for the TermList
					var list = new PropertyInfo<TModel, TermList>(info).Get(post);

					// Get terms
					var termsForThisPost = from t in terms
										   where t.PostId == post.Id.Value
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

		/// <summary>
		/// Internal term type for linking terms with posts
		/// </summary>
		internal sealed record Term : TermList.Term
		{
			/// <summary>
			/// Enables query for multiple posts and multiple taxonomies
			/// </summary>
			public long PostId { get; init; }

			/// <summary>
			/// Enables query for multiple posts and multiple taxonomies
			/// </summary>
			public Taxonomy Taxonomy { get; init; } = Taxonomy.Blank;
		}
	}
}
