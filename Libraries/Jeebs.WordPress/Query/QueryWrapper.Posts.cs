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
		public IQuery<T> QueryPosts<T>(Action<QueryPosts.Options>? modifyOptions = null)
		{
			return StartNewQuery()
				.WithModel<T>()
				.WithOptions(modifyOptions)
				.WithParts(new QueryPosts.Builder<T>(db))
				.GetQuery();
		}

		/// <summary>
		/// Add Meta and Custom Fields to the list of posts
		/// </summary>
		/// <typeparam name="T">Post model</typeparam>
		/// <param name="posts">Posts</param>
		/// <param name="meta">Expression to return MetaDictionary property</param>
		public async Task<IResult<bool>> AddMetaAndCustomFieldsToPostsAsync<T>(IEnumerable<T> posts)
			where T : IEntity
		{
			// Get MetaDictionary from the cache
			if (GetMetaDictionary<T>() is PropertyInfo meta)
			{
				// Convert to info class
				var info = new PropertyInfo<T, MetaDictionary>(meta);

				// Get Meta
				var addMetaResult = await AddMetaToPostsAsync(posts, info);
				if (addMetaResult.Err is IErrorList addMetaErr)
				{
					return Result.Failure(addMetaErr);
				}

				// Get Custom Fields
				var addCustomFieldsResult = await AddCustomFieldsToPostsAsync(posts, info);
				if (addCustomFieldsResult.Err is IErrorList addCustomFieldsErr)
				{
					return Result.Failure(addCustomFieldsErr);
				}

				// Return success
				return Result.Success();
			}

			throw new Jx.WordPress.QueryException($"Model {typeof(T)} does not have a MetaDictionary property.");
		}

		/// <summary>
		/// Add meta values to posts
		/// </summary>
		/// <typeparam name="T">Post model</typeparam>
		/// <param name="posts">Posts</param>
		/// <param name="meta">MetaDictionary property</param>
		private async Task<IResult<bool>> AddMetaToPostsAsync<T>(IEnumerable<T> posts, PropertyInfo<T, MetaDictionary> meta)
			where T : IEntity
		{
			// Don't do anything if there aren't any posts
			if (!posts.Any())
			{
				return Result.Failure("No posts to work on.");
			}

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
			var metaResult = await query.ExecuteQuery();
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
		private async Task<IResult<bool>> AddCustomFieldsToPostsAsync<T>(IEnumerable<T> posts, PropertyInfo<T, MetaDictionary> meta)
			where T : IEntity
		{
			// Don't do anything if there aren't any posts
			if (!posts.Any())
			{
				return Result.Failure("No posts to work on.");
			}

			// Get custom fields from the cache
			var customFields = GetCustomFields<T>();

			// If no custom fields, return success
			if (customFields.Count == 0)
			{
				return Result.Success();
			}

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
				foreach (var customField in customFields)
				{
					// Get field property
					var customFieldProperty = (ICustomField)post.GetProperty(customField.Name);

					// Hydrate the field
					var result = await customFieldProperty.Hydrate(db, unitOfWork, metaDictionary);
					if (result.Err is IErrorList err && customFieldProperty.IsRequired)
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
		/// <param name="posts"></param>
		public async Task<IResult<bool>> AddTaxonomiesToPostsAsync<T>(IEnumerable<T> posts)
			where T : IEntity
		{
			// Don't do anything if there aren't any posts
			if (!posts.Any())
			{
				return Result.Failure("No posts to work on.");
			}

			// Get term lists from the cache
			var termLists = GetTermLists<T>();

			// If no term lists, return success
			if (termLists.Count == 0)
			{
				return Result.Success();
			}

			// Create options
			var options = new QueryPostsTaxonomy.Options
			{
				PostIds = posts.Select(p => p.Id).ToList()
			};

			// Add each taxonomy to the query options
			var firstPost = posts.First();
			foreach (var list in termLists)
			{
				// Get taxonomy
				var taxonomy = new PropertyInfo<T, TermList>(list).Get(firstPost).Taxonomy;

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
			var termsResult = await query.ExecuteQuery();
			if (termsResult.Err is IErrorList)
			{
				return Result.Failure(termsResult.Err);
			}

			// Now add the terms to each post
			foreach (var post in posts)
			{
				foreach (var list in termLists)
				{
					// Get PropertyInfo<>
					var info = new PropertyInfo<T, TermList>(list).Get(post);

					// Get terms
					var terms = from t in termsResult.Val
								where t.PostId == post.Id
								&& t.Taxonomy == info.Taxonomy
								select (TermList.Term)t;

					// Add terms to post
					info.AddRange(terms);
				}
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
