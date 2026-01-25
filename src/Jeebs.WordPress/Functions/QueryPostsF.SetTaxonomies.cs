// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jeebs.Reflection;
using Jeebs.WordPress.Entities.Ids;

namespace Jeebs.WordPress.Functions;

public static partial class QueryPostsF
{
	/// <summary>
	/// Set Taxonomies for each post.
	/// </summary>
	/// <typeparam name="TList">List type.</typeparam>
	/// <typeparam name="TModel">Post type.</typeparam>
	/// <param name="posts">Posts.</param>
	/// <param name="terms">Terms.</param>
	/// <param name="termLists">Term List properties.</param>
	internal static Result<TList> SetTaxonomies<TList, TModel>(TList posts, IEnumerable<Term> terms, List<PropertyInfo> termLists)
		where TList : IEnumerable<TModel>
		where TModel : IWithId<WpPostId, ulong>
	{
		foreach (var post in posts)
		{
			foreach (var info in termLists)
			{
				// Get PropertyInfo<> for the TermList
				_ = new PropertyInfo<TModel, TermList>(info).Get(post).IfSome(list =>
				{
					// Get terms
					var termsForThisPost = from t in terms
										   where t.PostId == post.Id.Value
										   && t.Taxonomy == list.Taxonomy
										   select (TermList.Term)t;

					// Add terms to post
					if (termsForThisPost.Any())
					{
						list.AddRange(termsForThisPost);
					}
				});
			}
		}

		return posts;
	}
}
