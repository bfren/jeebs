using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Enums;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Query Post Taxonomy
	/// </summary>
	internal partial class QueryPostsTaxonomy
	{
		/// <inheritdoc/>
		internal sealed class Builder<T> : QueryPartsBuilderExtended<T, Options>
			where T : TermList.Term
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

			/// <inheritdoc/>
			public override IQueryParts Build(Options opt)
			{
				// Add SELECT
				AddSelect(db.Term, db.TermRelationship, db.TermTaxonomy);

				// FROM
				AddFrom(db.Term);

				// JOIN
				AddInnerJoin(db.TermTaxonomy, tx => tx.TermId, (db.Term, tm => tm.TermId));
				AddInnerJoin(db.TermRelationship, tr => tr.TermTaxonomyId, (db.TermTaxonomy, tx => tx.TermTaxonomyId));

				// WHERE Taxonomies
				if (opt.Taxonomies.Count > 0)
				{
					var taxonomies = string.Join($"'{Adapter.ColumnSeparator}'", opt.Taxonomies);
					AddWhere($"{__(db.TermTaxonomy, tx => tx.Taxonomy)} IN ('{taxonomies}')");
				}

				// WHERE Post IDs
				if (opt.PostIds.Count > 0)
				{
					var postIds = string.Join(Adapter.ColumnSeparator, opt.PostIds);
					AddWhere($"{__(db.TermRelationship, tr => tr.PostId)} IN ({postIds})");
				}

				// Finish and return
				return FinishBuild(opt,
					(__(db.Term, tm => tm.Title), SortOrder.Ascending),
					(__(db.TermTaxonomy, tx => tx.Count), SortOrder.Descending)
				);
			}
		}
	}
}
