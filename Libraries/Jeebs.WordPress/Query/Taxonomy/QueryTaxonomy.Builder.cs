using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Enums;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Query Taxonomy
	/// </summary>
	public partial class QueryTaxonomy
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
			internal Builder(IWpDb db) : base(db.Adapter, db.Term) =>
				this.db = db;

			/// <inheritdoc/>
			public override IQueryParts Build(Options opt)
			{
				// SELECT columns
				AddSelect(db.Term, db.TermTaxonomy);

				// INNER JOIN table
				AddInnerJoin(db.TermTaxonomy, tx => tx.TermId, (db.Term, tm => tm.TermId));

				// WHERE taxonomy
				if (opt.Taxonomy is Enums.Taxonomy taxonomy)
				{
					AddWhere($"{__(db.TermTaxonomy, tx => tx.Taxonomy)} = @{nameof(taxonomy)}", new { taxonomy });
				}

				// WHERE count
				if (opt.CountAtLeast is long count && count > 0)
				{
					AddWhere($"{__(db.TermTaxonomy, tx => tx.Count)} >= @{nameof(count)}", new { count });
				}

				// WHERE id
				if (opt.Id is long id)
				{
					AddWhere($"{__(db.Term, tm => tm.TermId)} = @{nameof(id)}", new { id });
				}

				// WHERE slug
				if (opt.Term is string slug)
				{
					AddWhere($"{__(db.Term, tm => tm.Slug)} = @{nameof(slug)}", new { slug });
				}

				// Finish and return
				return FinishBuild(opt,
					(__(db.Term, tm => tm.Title), SortOrder.Ascending),
					(__(db.TermTaxonomy, tx => tx.Count), SortOrder.Ascending)
				);
			}
		}
	}
}
