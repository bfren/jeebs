using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.Data.Querying;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Enums;
using Jm.WordPress.Query.Wrapper.Posts;

namespace Jeebs.WordPress
{
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Query Posts
		/// <para>Returns:</para>
		/// <para>Result.Failure - if there is an error executing the query, or processing the pages</para>
		/// <para>Result.NotFound - if the query executes successfully but no posts are found</para>
		/// <para>Result.Success - if the query and post processing execute successfully</para>
		/// </summary>
		/// <typeparam name="TModel">Entity type</typeparam>
		/// <param name="r">Result</param>
		/// <param name="modify">[Optional] Action to modify the options for this query</param>
		/// <param name="filters">[Optional] Content filters to apply to matching posts</param>
		public async Task<IR<List<TModel>>> QueryPostsAsync<TModel>(IOk r, Action<QueryPosts.Options>? modify = null, params ContentFilter[] filters)
			where TModel : IEntity
		{
			// Get query
			var query = GetQuery<TModel>(modify);

			// Execute query
			return await query.ExecuteQueryAsync(r).ConfigureAwait(false) switch
			{
				IOkV<List<TModel>> x when x.Value.Count == 0 => x,
				IOkV<List<TModel>> x => Process<List<TModel>, TModel>(x, filters).Await(),
				{ } x => x.Error(),
			};
		}

		/// <summary>
		/// Query Posts
		/// <para>Returns:</para>
		/// <para>Result.Failure - if there is an error executing the query, or processing the pages</para>
		/// <para>Result.NotFound - if the query executes successfully but no posts are found</para>
		/// <para>Result.Success - if the query and post processing execute successfully</para>
		/// </summary>
		/// <typeparam name="TModel">Entity type</typeparam>
		/// <param name="r">Result</param>
		/// <param name="page">Page number</param>
		/// <param name="modify">[Optional] Action to modify the options for this query</param>
		/// <param name="filters">[Optional] Content filters to apply to matching posts</param>
		public async Task<IR<PagedList<TModel>>> QueryPostsAsync<TModel>(IOk r, long page, Action<QueryPosts.Options>? modify = null, params ContentFilter[] filters)
			where TModel : IEntity
		{
			// Get query
			var query = GetQuery<TModel>(modify);

			// Execute query
			return await query.ExecuteQueryAsync(r, page).ConfigureAwait(false) switch
			{
				IOkV<PagedList<TModel>> x when x.Value.Count == 0 => x,
				IOkV<PagedList<TModel>> x => Process<PagedList<TModel>, TModel>(x, filters).Await(),
				{ } x => x.Error<PagedList<TModel>>(),
			};
		}

		/// <summary>
		/// Get query object
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="modify">[Optional] Action to modify the options for this query</param>
		private IQuery<TModel> GetQuery<TModel>(Action<QueryPosts.Options>? modify = null)
		{
			return StartNewQuery()
				.WithModel<TModel>()
				.WithOptions(modify)
				.WithParts(new QueryPosts.Builder<TModel>(db))
				.GetQuery();
		}

		/// <summary>
		/// Process a list of posts
		/// </summary>
		/// <typeparam name="TList">List type</typeparam>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="r">Result</param>
		/// <param name="filters">Content filters</param>
		private async Task<IR<TList>> Process<TList, TModel>(IOkV<TList> r, ContentFilter[] filters)
			where TList : List<TModel>
			where TModel : IEntity
		{
			return r
				.Link()
					.Handle().With<AddMetaExceptionMsg>()
					.MapAsync(AddMetaAsync<TList, TModel>).Await()
				.Link()
					.Handle().With<AddCustomFieldsExceptionMsg>()
					.MapAsync(AddCustomFieldsAsync<TList, TModel>).Await()
				.Link()
					.Handle().With<AddTaxonomiesExceptionMsg>()
					.MapAsync(AddTaxonomiesAsync<TList, TModel>).Await()
				.Link()
					.Handle().With<ApplyContentFiltersExceptionMsg>()
					.Map(okV => ApplyContentFilters<TList, TModel>(okV, filters));
		}

		/// <summary>
		/// Add meta dictionary to posts
		/// </summary>
		/// <typeparam name="TList">List type</typeparam>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="r">Result - value is list of posts</param>
		private async Task<IR<TList>> AddMetaAsync<TList, TModel>(IOkV<TList> r)
			where TList : List<TModel>
			where TModel : IEntity
		{
			// Only proceed if there is a meta property for this model
			if (GetMetaDictionaryInfo<TModel>() is Some<Meta<TModel>> s)
			{
				return r
					.Link()
						.Handle().With<GetMetaExceptionMsg>()
						.MapAsync(getMetaAsync).Await()
					.Link()
						.Handle().With<SetMetaExceptionMsg>()
						.Map(okV => setMeta(okV, s.Value));
			}

			return r;

			//
			// Get Post Meta values
			//
			async Task<IR<(TList, List<PostMeta>)>> getMetaAsync(IOkV<TList> r)
			{
				// Create options
				var options = new QueryPostsMeta.Options
				{
					PostIds = r.Value.Select(p => p.Id).ToList()
				};

				// Get query
				var query = StartNewQuery()
					.WithModel<PostMeta>()
					.WithOptions(options)
					.WithParts(new QueryPostsMeta.Builder<PostMeta>(db))
					.GetQuery();

				// Get meta
				return (await query.ExecuteQueryAsync(r).ConfigureAwait(false)).Switch(
					x => x.OkV((r.Value, x.Value))
				);
			}

			//
			// Set Meta for each Post
			//
			IR<TList> setMeta(IOkV<(TList, List<PostMeta>)> r, Meta<TModel> meta)
			{
				var (posts, postsMeta) = r.Value;

				// If no meta, return success
				if (postsMeta.Count == 0)
				{
					return r.OkV(posts);
				}

				// Add meta to each post
				foreach (var post in posts)
				{
					var postMeta = from m in postsMeta
								   where m.PostId == post.Id
								   select new KeyValuePair<string, string>(m.Key, m.Value);

					if (!postMeta.Any())
					{
						continue;
					}

					// Set the value of the meta property
					meta.Set(post, new MetaDictionary(postMeta));
				}

				return r.OkV(posts);
			}
		}

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
				Some<Meta<TModel>> x when fields.Count > 0 => r
					.Link()
						.Handle().With<HydrateCustomFieldExceptionMsg>()
						.MapAsync(okV => hydrateAsync(okV, x.Value, fields)).Await(),
				_ => r
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
						var customField = getCustomField(post, info);

						// Hydrate the field
						var result = await customField.HydrateAsync(r, db, unitOfWork, metaDictionary).ConfigureAwait(false);
						if (result is IError && customField.IsRequired)
						{
							return result.Error<TList>().AddMsg(new RequiredCustomFieldNotFoundMsg(post.Id, info.Name, customField.Key));
						}

						// Set the value
						info.SetValue(post, customField);
					}
				}

				return r.OkV(posts);
			}

			// Get a custom field - if it's null, create it
			static ICustomField getCustomField(TModel post, PropertyInfo info)
			{
				if (info.GetValue(post) is ICustomField field)
				{
					return field;
				}

				return (ICustomField)Activator.CreateInstance(info.PropertyType);
			}
		}

		/// <summary>
		/// Add Taxonomies to posts
		/// </summary>
		/// <typeparam name="TList">List type</typeparam>
		/// <typeparam name="TModel">Post type</typeparam>
		/// <param name="r">Result</param>
		private async Task<IR<TList>> AddTaxonomiesAsync<TList, TModel>(IOkV<TList> r)
			where TList : List<TModel>
			where TModel : IEntity
		{
			// Only proceed if there is at least one term list in this model
			var termLists = GetTermLists<TModel>();
			if (termLists.Count == 0)
			{
				return r;
			}

			return r
				.Link()
					.Handle().With<GetTermsExceptionMsg>()
					.MapAsync(okV => getTermsAsync(okV, termLists)).Await()
				.Link()
					.Handle().With<SetTermsExceptionMsg>()
					.Map(okV => addTerms(okV, termLists));

			//
			//	Get terms
			//
			async Task<IR<(TList, List<Term>)>> getTermsAsync(IOkV<TList> r, List<PropertyInfo> termLists)
			{
				// Create options
				var options = new QueryPostsTaxonomy.Options
				{
					PostIds = r.Value.Select(p => p.Id).ToList()
				};

				// Add each taxonomy to the query options
				var firstPost = r.Value[0];
				foreach (var info in termLists)
				{
					// Get taxonomy
					var taxonomy = new PropertyInfo<TModel, TermList>(info).Get(firstPost).Taxonomy;

					// Make sure taxonomy has been registered
					if (!Taxonomy.IsRegistered(taxonomy))
					{
						return r.Error<(TList, List<Term>)>().AddMsg(new TaxonomyNotRegisteredMsg(taxonomy));
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

				// Execute query
				return (await query.ExecuteQueryAsync(r).ConfigureAwait(false)).Switch(
					x => x.OkV((r.Value, x.Value))
				);
			}

			//
			//	Add terms
			//
			static IR<TList> addTerms(IOkV<(TList, List<Term>)> r, List<PropertyInfo> termLists)
			{
				var (posts, terms) = r.Value;

				// Now add the terms to each post
				foreach (var post in posts)
				{
					foreach (var info in termLists)
					{
						// Get PropertyInfo<> for the TermList
						var list = new PropertyInfo<TModel, TermList>(info).Get(post);

						// Get terms
						var termsForThisPost = from t in terms
											   where t.PostId == post.Id
											   && t.Taxonomy == list.Taxonomy
											   select (TermList.Term)t;

						// Add terms to post
						if (!termsForThisPost.Any())
						{
							continue;
						}

						list.AddRange(termsForThisPost);
					}
				}

				return r.OkV(posts);
			}
		}

		/// <summary>
		/// Apply Content Filters to post content
		/// </summary>
		/// <typeparam name="TList">List type</typeparam>
		/// <typeparam name="TModel">Post type</typeparam>
		/// <param name="r">Result</param>
		/// <param name="filters">Content Filters</param>
		private IR<TList> ApplyContentFilters<TList, TModel>(IOkV<TList> r, ContentFilter[] filters)
			where TList : List<TModel>
			where TModel : IEntity
		{
			// Only proceed if there are content filters
			if (filters.Length == 0)
			{
				return r;
			}

			// Post content field is required as we are expected to apply content filters
			return GetPostContentInfo<TModel>() switch
			{
				Some<Content<TModel>> x when x.Value is var content => r
					.Link()
						.Handle().With<ExecuteContentFiltersExceptionMsg>()
						.Map(okV => execute(okV, content, filters)),
				_ => r.Error().AddMsg().OfType<RequiredContentPropertyNotFoundMsg<TModel>>()
			};

			//
			// Apply content filters to each post
			//
			static IR<TList> execute(IOkV<TList> r, Content<TModel> content, ContentFilter[] filters)
			{
				var posts = r.Value;

				foreach (var post in posts)
				{
					// Get post content
					var postContent = content.Get(post);

					// Apply filters
					foreach (var filter in filters)
					{
						postContent = filter.Execute(postContent);
					}

					// Set filtered content
					content.Set(post, postContent);
				}

				return r.OkV(posts);
			}
		}

		private Option<Meta<TModel>> GetMetaDictionaryInfo<TModel>()
			=> GetMetaDictionary<TModel>().Map(x => new Meta<TModel>(x));

		private class Meta<TModel> : PropertyInfo<TModel, MetaDictionary>
		{
			public Meta(PropertyInfo info) : base(info) { }
		}

		private Option<Content<TModel>> GetPostContentInfo<TModel>()
			=> GetPostContent<TModel>().Map(x => new Content<TModel>(x));

		private class Content<TModel> : PropertyInfo<TModel, string>
		{
			public Content(PropertyInfo info) : base(info) { }
		}

		private class PostMeta : WpPostMetaEntity { }

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
