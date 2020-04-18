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
		/// <summary>
		/// Query Builder
		/// </summary>
		internal sealed class Builder<T> : QueryPartsBuilder<T, Options>
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
				// Use db shorthands
				var _ = db;
				var tm = _.Term.ToString();
				var tx = _.TermTaxonomy.ToString();

				// SELECT columns
				AddSelect($"{Adapter.Extract<T>(_.Term)}");

				// FROM table
				AddFrom($"{Escape(tm)}");

				// INNER JOIN table
				AddInnerJoin(tx, _.TermTaxonomy.TermId, (tm, _.Term.TermId));

				// WHERE taxonomy
				if (opt.Taxonomy is Enums.Taxonomy taxonomy)
				{
					AddWhere($"{Escape(tx, _.TermTaxonomy.Taxonomy)} = @{nameof(taxonomy)}", new { taxonomy });
				}

				// WHERE count
				if (opt.CountAtLeast is long count && count > 0)
				{
					AddWhere($"{Escape(tx, _.TermTaxonomy.Count)} >= @{nameof(count)}", new { count });
				}

				// WHERE id
				if (opt.Id is long id)
				{
					AddWhere($"{Escape(tm, _.Term.TermId)} = @{nameof(id)}", new { id });
				}

				// WHERE slug
				if (opt.Term is string slug)
				{
					AddWhere($"{Escape(tm, _.Term.Slug)} = @{nameof(slug)}", new { slug });
				}

				// Finish and return
				return FinishBuild(opt,
					(Escape(tm, _.Term.Title), SortOrder.Ascending),
					(Escape(tx, _.TermTaxonomy.Count), SortOrder.Ascending)
				);
			}
		}
	}
}
