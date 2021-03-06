// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System.Threading.Tasks;
using Jm.WordPress.Query.Wrapper.PostsMeta;

namespace Jeebs.WordPress
{
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Get the full path to an attached file
		/// </summary>
		/// <param name="r">Result</param>
		/// <param name="uploadsPath">Full path to wp-uploads directory on server</param>
		public async Task<IR<string>> GetAttachedFilePathAsync(IOkV<long> r, string uploadsPath)
		{
			// Get query
			var query = GetPostsMetaQuery<AttachedFileMetaValue>(opt =>
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
					.UnwrapSingle<AttachedFileMetaValue>()
				.Link()
					.Map(addUploadsPath);

			// Add uploads path 
			IR<string> addUploadsPath(IOkV<AttachedFileMetaValue> r) =>
				r.OkV(uploadsPath.EndWith('/') + r.Value.Value);
		}

		private record AttachedFileMetaValue(string Value);
	}
}
