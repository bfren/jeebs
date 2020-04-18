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
		public async Task<IResult<List<T>>> QueryPostsAsync<T>(Action<QueryPosts.Options>? modifyOptions = null)
			where T : IEntity
		{
			// Get query
			var query = StartNewQuery()
				.WithModel<T>()
				.WithOptions(modifyOptions)
				.WithParts(new QueryPosts.Builder<T>(db))
				.GetQuery();

			// Execute query
			var results = await query.ExecuteQueryAsync();
			if (results.Err is IErrorList)
			{
				return Result.Failure<List<T>>(results.Err);
			}

			// If nothing matches, return empty list
			if (!results.Val.Any())
			{
				return Result.Success(new List<T>());
			}

			// If the type has a MetaDictionary, add Meta and check for Custom Fields
			var posts = results.Val.ToList();
			if (GetMetaDictionary<T>() is PropertyInfo info)
			{
				// Convert to info class
				var meta = new PropertyInfo<T, MetaDictionary>(info);

				// Get Meta
				var addMetaResult = await AddMetaToPostsAsync(posts, meta);
				if (addMetaResult.Err is IErrorList)
				{
					return Result.Failure<List<T>>(addMetaResult.Err);
				}

				// Get Custom Fields
				var addCustomFieldsResult = await AddCustomFieldsToPostsAsync(posts, meta);
				if (addCustomFieldsResult.Err is IErrorList)
				{
					return Result.Failure<List<T>>(addCustomFieldsResult.Err);
				}
			}

			// Add Taxonomies
			var addTaxonomiesResult = await AddTaxonomiesToPostsAsync(posts);
			if (addTaxonomiesResult.Err is IErrorList)
			{
				return Result.Failure<List<T>>(addTaxonomiesResult.Err);
			}

			// Return posts
			return Result.Success(posts);
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
		private async Task<IResult<bool>> AddCustomFieldsToPostsAsync<T>(IEnumerable<T> posts, PropertyInfo<T, MetaDictionary> meta)
			where T : IEntity
		{
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
		/// <param name="posts"></param>
		private async Task<IResult<bool>> AddTaxonomiesToPostsAsync<T>(IEnumerable<T> posts)
			where T : IEntity
		{
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
