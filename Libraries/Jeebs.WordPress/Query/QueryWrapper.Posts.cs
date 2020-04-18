using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Reflection;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Enums;
using Jeebs.WordPress.Tables;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Query wrapper
	/// </summary>
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Query Posts
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="modifyOptions">[Optional] Action to modify the options for this query</param>
		/// <param name="contentFilters">[Optional] Content filters to apply to matching posts</param>
		public async Task<IResult<List<T>>> QueryPostsAsync<T>(Action<QueryPosts.Options>? modifyOptions = null, params ContentFilter[] contentFilters)
			where T : IEntity
		{
			//
			//
			//	EXECUTE MAIN QUERY - GET POSTS
			//
			//

			// Execute query
			var results = await StartNewQuery()
				.WithModel<T>()
				.WithOptions(modifyOptions)
				.WithParts(new QueryPosts.Builder<T>(db))
				.GetQuery()
				.ExecuteQueryAsync();

			if (results.Err is IErrorList)
			{
				return Result.Failure<List<T>>(results.Err);
			}

			// If nothing matches, return empty list
			if (!results.Val.Any())
			{
				return Result.Success(new List<T>());
			}

			var posts = results.Val.ToList();



			//
			//
			//	ADD META AND CUSTOM FIELDS
			//
			//

			// If the type has a MetaDictionary, add Meta and check for Custom Fields
			if (GetMetaDictionary<T>() is PropertyInfo metaInfo)
			{
				// Convert to info class
				var meta = new PropertyInfo<T, MetaDictionary>(metaInfo);

				// Add meta
				var addMetaResult = await AddMetaToPostsAsync(posts, meta);
				if (addMetaResult.Err is IErrorList)
				{
					return Result.Failure<List<T>>(addMetaResult.Err);
				}

				// Get custom fields from the cache
				var customFields = GetCustomFields<T>();
				if (customFields.Count > 0)
				{
					// Add custom fields
					var addCustomFieldsResult = await AddCustomFieldsToPostsAsync(posts, meta, customFields);
					if (addCustomFieldsResult.Err is IErrorList)
					{
						return Result.Failure<List<T>>(addCustomFieldsResult.Err);
					}
				}
			}



			//
			//
			//	ADD TAXONOMIES
			//
			//

			// Get term lists from the cache
			var termLists = GetTermLists<T>();
			if (termLists.Count > 0)
			{
				// Add taxonomies
				var addTaxonomiesResult = await AddTaxonomiesToPostsAsync(posts, termLists);
				if (addTaxonomiesResult.Err is IErrorList)
				{
					return Result.Failure<List<T>>(addTaxonomiesResult.Err);
				}
			}



			//
			//
			//	APPLY CONTENT FILTERS
			//
			//

			// If there are some filters to apply, get post content from the cache
			if (contentFilters.Length > 0)
			{
				if (GetPostContent<T>() is PropertyInfo contentInfo)
				{
					// Convert to info class
					var content = new PropertyInfo<T, string>(contentInfo);

					// Apply content filters
					var applyContentFiltersResult = ApplyContentFilters(posts, content, contentFilters);
					if (applyContentFiltersResult.Err is IErrorList)
					{
						return Result.Failure<List<T>>(applyContentFiltersResult.Err);
					}
				}
				else
				{
					throw new Jx.WordPress.QueryException($"Cannot find the {nameof(WpPostEntity.Content)} property of {typeof(T)}.");
				}
			}



			//
			//
			//	RETURN POSTS
			//
			//

			// Return posts
			return Result.Success(posts);
		}

		/// <summary>
		/// Add meta values to posts
		/// </summary>
		/// <typeparam name="T">Post model</typeparam>
		/// <param name="posts">Posts</param>
		/// <param name="meta">MetaDictionary property</param>
		private async Task<IResult<bool>> AddMetaToPostsAsync<T>(List<T> posts, PropertyInfo<T, MetaDictionary> meta)
			where T : IEntity
		{
			// Create options
			var options = new QueryPostsMeta.Options
			{
				PostIds = posts.Select(p => p.Id).ToList()
			};

			// Get Exec
			var query = StartNewQuery()
				.WithModel<PostMeta>()
				.WithOptions(options)
				.WithParts(new QueryPostsMeta.Builder<PostMeta>(db))
				.GetQuery();

			// Get meta
			var metaResult = await query.ExecuteQueryAsync();
			if (metaResult.Err is IErrorList)
			{
				return Result.Failure(metaResult.Err);
			}
			// If no meta, return success
			else if (!metaResult.Val.Any())
			{
				return Result.Success();
			}

			// Add meta to each post
			foreach (var post in posts)
			{
				var postMeta = from m in metaResult.Val
							   where m.PostId == post.Id
							   select new KeyValuePair<string, string>(m.Key, m.Value);

				if (!postMeta.Any())
				{
					continue;
				}

				// Set the value of the meta property
				meta.Set(post, new MetaDictionary(postMeta));
			}

			// Return success
			return Result.Success();
		}

		/// <summary>
		/// Add custom fields to posts
		/// </summary>
		/// <typeparam name="T">Post model</typeparam>
		/// <param name="posts">Posts</param>
		/// <param name="meta">MetaDictionary property</param>
		/// <param name="customFields">List of CustomFields</param>
		private async Task<IResult<bool>> AddCustomFieldsToPostsAsync<T>(List<T> posts, PropertyInfo<T, MetaDictionary> meta, List<PropertyInfo> customFields)
			where T : IEntity
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
					// Get field property
					var customField = (ICustomField)post.GetProperty(info.Name);

					// Hydrate the field
					var result = await customField.HydrateAsync(db, unitOfWork, metaDictionary);
					if (result.Err is IErrorList err && customField.IsRequired)
					{
						return Result.Failure(err);
					}
				}
			}

			// Return success
			return Result.Success();
		}

		/// <summary>
		/// Add Taxonomies to posts
		/// </summary>
		/// <typeparam name="T">Post model</typeparam>
		/// <param name="posts">Posts</param>
		private async Task<IResult<bool>> AddTaxonomiesToPostsAsync<T>(List<T> posts, List<PropertyInfo> termLists)
			where T : IEntity
		{
			// Create options
			var options = new QueryPostsTaxonomy.Options
			{
				PostIds = posts.Select(p => p.Id).ToList()
			};

			// Add each taxonomy to the query options
			var firstPost = posts.First();
			foreach (var info in termLists)
			{
				// Get taxonomy
				var taxonomy = new PropertyInfo<T, TermList>(info).Get(firstPost).Taxonomy;

				// Make sure taxonomy has been registered
				if (!Taxonomy.IsRegistered(taxonomy))
				{
					throw new Jx.WordPress.QueryException($"Taxonomy '{taxonomy}' must be registered in WpDb.RegisterCustomTaxonomies().");
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

			// Get terms
			var termsResult = await query.ExecuteQueryAsync();
			if (termsResult.Err is IErrorList)
			{
				return Result.Failure(termsResult.Err);
			}

			// Now add the terms to each post
			foreach (var post in posts)
			{
				foreach (var info in termLists)
				{
					// Get PropertyInfo<> for the TermList
					var list = new PropertyInfo<T, TermList>(info).Get(post);

					// Get terms
					var terms = from t in termsResult.Val
								where t.PostId == post.Id
								&& t.Taxonomy == list.Taxonomy
								select (TermList.Term)t;

					// Add terms to post
					if (!terms.Any())
					{
						continue;
					}

					list.AddRange(terms);
				}
			}

			// Return success
			return Result.Success();
		}

		/// <summary>
		/// Apply Content Filters to post content
		/// </summary>
		/// <typeparam name="T">Post model</typeparam>
		/// <param name="posts">Posts</param>
		/// <param name="content">Content property</param>
		/// <param name="contentFilters">Content filters</param>
		private IResult<bool> ApplyContentFilters<T>(List<T> posts, PropertyInfo<T, string> content, ContentFilter[] contentFilters)
		{
			// Apply filters to each post
			foreach (var post in posts)
			{
				// Get post content
				var postContent = content.Get(post);

				// Apply filters
				foreach (var filter in contentFilters)
				{
					postContent = filter.Execute(postContent);
				}

				// Set filtered content
				content.Set(post, postContent);
			}

			// Return success
			return Result.Success();
		}

		/// <summary>
		/// Private PostMeta entity for meta queries
		/// </summary>
		private class PostMeta : Entities.WpPostMetaEntity { }

		/// <summary>
		/// Private Term entity for taxonomy queries
		/// </summary>
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
