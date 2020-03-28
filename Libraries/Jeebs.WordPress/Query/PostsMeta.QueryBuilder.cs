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
		/// <summary>
		/// Query Builder
		/// </summary>
		internal sealed class Builder<T> : QueryBuilder<T, Options>
		{
			/// <summary>
			/// IWpDb
			/// </summary>
			private readonly IWpDb db;

			/// <summary>
			/// Create object
			/// </summary>
			/// <param name="db">IWpDb</param>
			internal Builder(IWpDb db) : base(db.Adapter) => this.db = db;

			/// <summary>
			/// Build query
			/// </summary>
			/// <param name="opt">QueryOptions</param>
			public override QueryArgs<T> Build(Options opt)
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
