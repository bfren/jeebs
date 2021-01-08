using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Query Post Meta
	/// </summary>
	public partial class QueryPostsMeta
	{
		/// <inheritdoc/>
		public sealed class Builder<T> : QueryPartsBuilderExtended<T, Options>
		{
			/// <summary>
			/// IWpDb
			/// </summary>
			private readonly IWpDb db;

			/// <summary>
			/// Create object
			/// </summary>
			/// <param name="db">IWpDb</param>
			internal Builder(IWpDb db) : base(db.Adapter, db.PostMeta)
				=> this.db = db;

			/// <inheritdoc/>
			public override IQueryParts Build(Options opt)
			{
				// SELECT
				AddSelect(db.PostMeta);

				// WHERE Post ID
				if (opt.PostId is long postId && postId > 0)
				{
					AddWhere($"{__(db.PostMeta, pm => pm.PostId)} = @{nameof(postId)}", new { postId });
				}
				// WHERE Post IDs
				else if (opt.PostIds is List<long> postIds && postIds.Count > 0)
				{
					AddWhere($"{__(db.PostMeta, pm => pm.PostId)} IN ({string.Join(Adapter.ListSeparator, postIds)})");
				}

				// WHERE Meta Key
				if (opt.Key is string metaKey)
				{
					AddWhere($"{__(db.PostMeta, pm => pm.Key)} = @{nameof(metaKey)}", new { metaKey });
				}

				// Finish and return
				return FinishBuild(opt);
			}
		}
	}
}
