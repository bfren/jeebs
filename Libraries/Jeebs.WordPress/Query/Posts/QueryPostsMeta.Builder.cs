using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Query Post Meta
	/// </summary>
	internal partial class QueryPostsMeta
	{
		/// <inheritdoc/>
		internal sealed class Builder<T> : QueryPartsBuilderExtended<T, Options>
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

				// WHERE Post IDs
				if (opt.PostIds is List<long> postIds && postIds.Count > 0)
				{
					AddWhere($"{__(db.PostMeta, pm => pm.PostId)} IN ({string.Join(Adapter.ListSeparator, postIds)})");
				}

				// Finish and return
				return FinishBuild(opt);
			}
		}
	}
}
