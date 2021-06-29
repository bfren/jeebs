// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Data;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using static F.OptionF;

namespace F.WordPressF.DataF
{
	public static partial class QueryPostsF
	{
		/// <summary>
		/// Add custom fields to posts
		/// </summary>
		/// <typeparam name="TList">List type</typeparam>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="db">IWpDb</param>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="posts">Posts</param>
		internal static Task<Option<TList>> AddCustomFieldsAsync<TList, TModel>(IWpDb db, IUnitOfWork w, TList posts)
			where TList : IEnumerable<TModel>
			where TModel : IWithId<WpPostId>
		{
			// If there are no posts, do nothing
			if (!posts.Any())
			{
				return Return(posts).AsTask;
			}

			// Only proceed if there are custom fields, and a meta property for this model
			var fields = GetCustomFields<TModel>();
			if (fields.Count == 0)
			{
				return Return(posts).AsTask;
			}

			// Get terms and add them to the posts
			return GetMetaDictionary<TModel>()
				.BindAsync(
					x => HydrateAsync(db, w, posts, x, fields)
				);
		}

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
						if (result is Some<bool> ok && ok.Value)
						{
							info.SetValue(post, customField);
						}
					}
				}
			}

			return posts;
		}

		// Get a custom field - if it's null, create it
		internal static ICustomField? GetCustomField<TModel>(TModel post, PropertyInfo info)
		{
			if (info.GetValue(post) is ICustomField field)
			{
				return field;
			}

			return Activator.CreateInstance(info.PropertyType) switch
			{
				ICustomField customField =>
					customField,

				_ =>
					null
			};
		}

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>An exception occured while adding custom fields to posts</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record AddCustomFieldsExceptionMsg<T>(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Required Custom Field property not found on model</summary>
			/// <typeparam name="T">Post Model type</typeparam>
			/// <param name="PostId">Post ID</param>
			/// <param name="Property">Property name</param>
			/// <param name="Key">Custom Field Key</param>
			public sealed record RequiredCustomFieldNotFoundMsg<T>(StrongId PostId, string Property, string Key) : IMsg { }
		}
	}
}
