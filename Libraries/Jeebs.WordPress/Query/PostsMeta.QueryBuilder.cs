using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Query Post Meta
	/// </summary>
	public partial class PostsMeta
	{
		/// <summary>
		/// Query Builder
		/// </summary>
		internal sealed class QueryBuilder<T> : QueryBuilder<T, QueryOptions>
		{
			/// <summary>
			/// IWpDb
			/// </summary>
			private readonly IWpDb db;

			/// <summary>
			/// Create object
			/// </summary>
			/// <param name="db">IWpDb</param>
			internal QueryBuilder(IWpDb db) : base(db.Adapter) => this.db = db;

			public override QueryArgs<T> Build(QueryOptions opt)
			{
				// FROM
				AddFrom(db.PostMeta.ToString());

				// SELECT
				AddSelect(Extract<T>(db.PostMeta));

				// WHERE Post IDs
				if (opt.PostIds is List<long> postIds)
				{
					AddWhere($"{Escape(db.PostMeta.PostId)} IN ({string.Join(", ", postIds)})");
				}

				// LIMIT and OFFSET
				AddLimitAndOffset(opt);

				// Return
				return Args;
			}
		}
	}
}
