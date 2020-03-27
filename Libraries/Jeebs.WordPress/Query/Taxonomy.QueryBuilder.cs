using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Query Taxonomy
	/// </summary>
	public partial class Taxonomy
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

			/// <summary>
			/// Build query
			/// </summary>
			/// <param name="opt">QueryOptions</param>
			public override QueryArgs<T> Build(QueryOptions opt)
			{
				// Use db shorthands
				var _ = db;
				var tm = _.Term.ToString();
				var tx = _.TermTaxonomy.ToString();

				// SELECT columns
				AddSelect($"{Extract<T>(_.Term)}");

				// FROM table
				AddFrom($"{Escape(_.Term)} AS {Escape(tm)}");

				// TODO : add join support to query
			}
		}
	}
}
