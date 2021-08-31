// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Reflection;
using Jeebs;
using Jeebs.Data;
using Jeebs.Internals;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using static F.OptionF;

namespace F.WordPressF.DataF
{
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
		internal static async Task<Option<TList>> HydrateAsync<TList, TModel>(
			IWpDb db,
			IUnitOfWork w,
			TList posts,
			Meta<TModel> meta,
			List<PropertyInfo> fields
		)
			where TList : IEnumerable<TModel>
			where TModel : IWithId<WpPostId>
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
							return None<TList>(none.Reason);
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
}
