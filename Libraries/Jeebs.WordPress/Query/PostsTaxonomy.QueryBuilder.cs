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
		/// <summary>
		/// Query Builder
		/// </summary>
		internal sealed class Builder<T> : QueryPartsBuilder<T, Options>
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

			/// <summary>
			/// Build query
			/// </summary>
			/// <param name="opt">QueryOptions</param>
			public override IQueryParts Build(Options opt)
			{
				// FROM
				AddFrom(db.Term.ToString());

				// Add SELECT
				AddSelect(Adapter.Extract<T>(db.Term, db.TermRelationship, db.TermTaxonomy));

				// JOIN
				AddInnerJoin(db.TermTaxonomy, db.TermTaxonomy.TermId, (db.Term, db.Term.TermId));
				AddInnerJoin(db.TermRelationship, db.TermRelationship.TermTaxonomyId, (db.TermTaxonomy, db.TermTaxonomy.TermTaxonomyId));

				// WHERE Taxonomies
				if (opt.Taxonomies.Count > 0)
				{
					var taxonomies = string.Join($"'{db.Adapter.ColumnSeparator}'", opt.Taxonomies);
					AddWhere($"{Escape(db.TermTaxonomy, db.TermTaxonomy.Taxonomy)} IN ('{taxonomies}')");
				}

				// WHERE Post IDs
				if (opt.PostIds.Count > 0)
				{
					var postIds = string.Join(db.Adapter.ColumnSeparator, opt.PostIds);
					AddWhere($"{Escape(db.TermRelationship.PostId)} IN ({postIds})");
				}

				// Finish and return
				return FinishBuild(opt,
					(Escape(db.Term, db.Term.Title), SortOrder.Ascending),
					(Escape(db.TermTaxonomy, db.TermTaxonomy.Count), SortOrder.Descending)
				);
			}
		}
	}
}
