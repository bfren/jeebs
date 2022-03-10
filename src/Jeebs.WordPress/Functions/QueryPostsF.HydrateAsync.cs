// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.WordPress.CustomFields;
using Jeebs.WordPress.Entities.StrongIds;
using Maybe;
using Maybe.Functions;
using Maybe.Internals;

namespace Jeebs.WordPress.Functions;

public static partial class QueryPostsF
{
	/// <summary>
	/// Hydrate the custom fields for the list of posts
	/// </summary>
	/// <typeparam name="TList">List type</typeparam>
	/// <typeparam name="TModel">Model type</typeparam>
	/// <param name="db">IWpDb</param>
	/// <param name="w">IUnitOfWork</param>
	/// <param name="posts">Posts</param>
	/// <param name="meta">Meta property</param>
	/// <param name="fields">Custom Fields</param>
	internal static async Task<Maybe<TList>> HydrateAsync<TList, TModel>(
		IWpDb db,
		IUnitOfWork w,
		TList posts,
		Meta<TModel> meta,
		List<PropertyInfo> fields
	)
		where TList : IEnumerable<TModel>
		where TModel : Id.IWithId<WpPostId>
	{
		// Hydrate all custom fields for all posts
		foreach (var post in posts)
		{
			// Get meta
			var metaDict = meta.Get(post);

			// Add each custom field
			foreach (var info in fields)
			{
				// Whether or not the field is required
				var required = info.GetValue(post) is not null;

				// Get custom field
				if (GetCustomField(post, info) is ICustomField customField)
				{
					// Hydrate the field
					var result = await customField.HydrateAsync(db, w, metaDict, required).ConfigureAwait(false);

					// If it failed and it's required, return None
					if (result is None<bool> none && required)
					{
						return MaybeF.None<TList>(none.Reason);
					}

					// Set the value
					if (result is Some<bool> some && some.Value)
					{
						info.SetValue(post, customField);
					}
				}
			}
		}

		return posts;
	}
}
