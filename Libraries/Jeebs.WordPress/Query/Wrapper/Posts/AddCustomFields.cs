// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Jeebs.Data;
using JeebsF;
using Jm.WordPress.Query.Wrapper.Posts;
using static JeebsF.OptionF;

namespace Jeebs.WordPress
{
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Add custom fields to posts
		/// </summary>
		/// <typeparam name="TList">List type</typeparam>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="posts">Posts</param>
		private async Task<Option<TList>> AddCustomFieldsAsync<TList, TModel>(TList posts)
			where TList : List<TModel>
			where TModel : IEntity
		{
			// Only proceed if there are custom fields, and a meta property for this model
			var fields = GetCustomFields<TModel>();
			if (fields.Count == 0)
			{
				return posts;
			}

			// Meta dictionary is required
			return GetMetaDictionaryInfo<TModel>() switch
			{
				Some<Meta<TModel>> meta when fields.Count > 0 =>
					await Return(posts)
						.BindAsync(
							x => hydrateAsync(x, meta.Value, fields),
							e => new AddCustomFieldsExceptionMsg(e)
						),

				_ =>
					None<TList>(new MetaDictionaryNotFoundMsg())
			};

			//
			// Hydrate each custom field
			//
			async Task<Option<TList>> hydrateAsync(TList posts, Meta<TModel> meta, List<PropertyInfo> customFields)
			{
				// Hydrate all custom fields for all posts
				foreach (var post in posts)
				{
					// If post is null, continue
					if (post == null)
					{
						continue;
					}

					// Get meta
					var metaDictionary = meta.Get(post);

					// Add each custom field
					foreach (var info in customFields)
					{
						// Get custom field
						if (getCustomField(post, info) is ICustomField customField)
						{
							// Hydrate the field
							var result = await customField.HydrateAsync(db, UnitOfWork, metaDictionary).ConfigureAwait(false);

							if (result is None<bool> && customField.IsRequired)
							{
								return None<TList>(new RequiredCustomFieldNotFoundMsg(post.Id, info.Name, customField.Key));
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
			static ICustomField? getCustomField(TModel post, PropertyInfo info)
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
		}
	}
}
