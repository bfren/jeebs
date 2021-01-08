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
using Jm.WordPress.Query.Wrapper.PostsMeta;

namespace Jeebs.WordPress
{
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Query Posts Meta
		/// </summary>
		/// <typeparam name="TModel">Term type</typeparam>
		/// <param name="r">Result</param>
		/// <param name="modify">[Optional] Action to modify the options for this query</param>
		public async Task<IR<List<TModel>>> QueryPostsMetaAsync<TModel>(IOk r, Action<QueryPostsMeta.Options>? modify = null)
			where TModel : IEntity
		{
			// Get query
			var query = GetPostsMetaQuery<TModel>(modify);

			// Execute query
			return await query.ExecuteQueryAsync(r).ConfigureAwait(false);
		}

		/// <summary>
		/// Get the full path to an attached file
		/// </summary>
		/// <param name="r">Result</param>
		/// <param name="uploadsPath">Full path to wp-uploads directory on server</param>
		public async Task<IR<string>> GetAttachedFilePathAsync(IOkV<long> r, string uploadsPath)
		{
			// Get query
			var query = GetPostsMetaQuery<AttachedFileValue>(opt =>
			{
				opt.PostId = r.Value;
				opt.Key = Constants.AttachedFile;
			});

			// Execute query
			return r
				.Link()
					.Handle().With<GetAttachedFileValueExceptionMsg>()
					.MapAsync(query.ExecuteQueryAsync).Await()
				.Link()
					.Handle().With<MultipleAttachedFilesFoundExceptionMsg>()
					.UnwrapSingle<AttachedFileValue>()
				.Link()
					.Map(addUploadsPath);

			// Add uploads path 
			IR<string> addUploadsPath(IOkV<AttachedFileValue> r)
				=> r.OkV(uploadsPath.EndWith('/') + r.Value.Value);
		}

		/// <summary>
		/// Get query object
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="modify">[Optional] Action to modify the options for this query</param>
		private IQuery<TModel> GetPostsMetaQuery<TModel>(Action<QueryPostsMeta.Options>? modify = null)
			=> StartNewQuery()
				.WithModel<TModel>()
				.WithOptions(modify)
				.WithParts(new QueryPostsMeta.Builder<TModel>(db))
				.GetQuery();

		private record AttachedFileValue(string Value);
	}
}
