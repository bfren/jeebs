using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs.WordPress.Entities.Records;
using Jm.WordPress.Query.Wrapper.PostsMeta;

namespace Jeebs.WordPress
{
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Get attached files
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="r">Result - value is list of attached file IDs</param>
		/// <param name="virtualUploadsUrl">Virtual Uploads URL for building URLs</param>
		public async Task<IR<List<T>>> GetAttachedFiles<T>(IOkV<List<long>> r, string virtualUploadsUrl)
			where T : WpAttachedFile
		{
			// Check for empty list
			if (r.Value.Count == 0)
			{
				return r.OkV(new List<T>());
			}

			// Run query
			return r
				.Link()
					.Map(getQuery)
				.Link()
					.Handle().With<GetAttachedFilesExceptionMsg>()
					.MapAsync(getAttachedFiles).Await();

			// Build custom query
			IR<string> getQuery(IOkV<List<long>> r)
				=> r.OkV("SELECT " +
						$"`p`.`{db.Post.Title}` AS '{nameof(WpAttachedFile.Title)}', " +
						$"`p`.`{db.Post.Excerpt}` AS '{nameof(WpAttachedFile.Description)}', " +
						$"CONCAT('{virtualUploadsUrl.EndWith('/')}', `pm`.`{db.PostMeta.Value}`) AS '{nameof(WpAttachedFile.Url)}' " +
					$"FROM `{db.Post}` AS `p` " +
						$"LEFT JOIN `{db.PostMeta}` AS `pm` ON `p`.`{db.Post.PostId}` = `pm`.`{db.PostMeta.PostId}` " +
					$"WHERE `p`.`{db.Post.PostId}` IN ({string.Join(',', r.Value)}) " +
						$"AND `pm`.`{db.PostMeta.Key}` = '{Constants.AttachedFile}';"
				);

			// Get the files
			async Task<IR<List<T>>> getAttachedFiles(IOkV<string> r)
			{
				// Start unit of work
				using var w = db.UnitOfWork;

				// Execute query
				return await w.QueryAsync<T>(r, r.Value).ConfigureAwait(false) switch
				{
					IOkV<IEnumerable<T>> x => r.OkV(x.Value.ToList()),
					{ } e => e.Error<List<T>>()
				};
			}
		}
	}
}