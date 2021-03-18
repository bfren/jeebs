// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Jeebs.Data;
using static F.OptionF;

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
		private Task<Option<TList>> AddCustomFieldsAsync<TList, TModel>(TList posts)
			where TList : List<TModel>
			where TModel : IEntity
		{
			// Only proceed if there are custom fields, and a meta property for this model
			var fields = GetCustomFields<TModel>();
			if (fields.Count == 0)
			{
				return Return(posts).AsTask;
			}

			// Meta dictionary is required
			return GetMetaDictionaryInfo<TModel>() switch
			{
				Some<Meta<TModel>> meta when fields.Count > 0 =>
					Return(posts)
						.BindAsync(
							x => hydrateAsync(x, meta.Value, fields)
						),

				_ =>
					None<TList, Msg.MetaDictionaryNotFoundMsg<TModel>>().AsTask
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
								return None<TList>(new Msg.RequiredCustomFieldNotFoundMsg<TModel>(post.Id, info.Name, customField.Key));
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

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>An exception occured while adding custom fields to posts</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record AddCustomFieldsExceptionMsg<T>(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Meta Dictionary property not found on model</summary>
			/// <typeparam name="T">Post Model type</typeparam>
			public sealed record MetaDictionaryNotFoundMsg<T> : IMsg { }

			/// <summary>Required Custom Field property not found on model</summary>
			/// <typeparam name="T">Post Model type</typeparam>
			/// <param name="PostId">Post ID</param>
			/// <param name="Property">Property name</param>
			/// <param name="Key">Custom Field Key</param>
			public sealed record RequiredCustomFieldNotFoundMsg<T>(long PostId, string Property, string Key) : IMsg { }
		}
	}
}
