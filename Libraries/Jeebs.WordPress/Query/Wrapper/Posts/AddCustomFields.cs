// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Jeebs.Data;
using Jm.WordPress.Query.Wrapper.Posts;

namespace Jeebs.WordPress
{
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Add custom fields to posts
		/// </summary>
		/// <typeparam name="TList">List type</typeparam>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="r">Result - value is list of posts</param>
		private async Task<IR<TList>> AddCustomFieldsAsync<TList, TModel>(IOkV<TList> r)
			where TList : List<TModel>
			where TModel : IEntity
		{
			// Only proceed if there are custom fields, and a meta property for this model
			var fields = GetCustomFields<TModel>();
			return GetMetaDictionaryInfo<TModel>() switch
			{
				Some<Meta<TModel>> x when fields.Count > 0 =>
					r.Link()
						.Catch().AllUnhandled().With<HydrateCustomFieldExceptionMsg>()
						.MapAsync(okV => hydrateAsync(okV, x.Value, fields)).Await(),
				_ =>
					r
			};

			//
			// Hydrate each custom field
			//
			async Task<IR<TList>> hydrateAsync(IOkV<TList> r, Meta<TModel> meta, List<PropertyInfo> customFields)
			{
				var posts = r.Value;

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
							var result = await customField.HydrateAsync(r, db, UnitOfWork, metaDictionary).ConfigureAwait(false);
							if (result is IError && customField.IsRequired)
							{
								return result.Error<TList>().AddMsg(new RequiredCustomFieldNotFoundMsg(post.Id, info.Name, customField.Key));
							}

							// Set the value
							if (result is IOkV<bool> ok && ok.Value)
							{
								info.SetValue(post, customField);
							}
						}
					}
				}

				return r.OkV(posts);
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
