// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq.Expressions;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying;
using Jeebs.Linq;
using static F.DataF.QueryF;

namespace Jeebs.WordPress.Data
{
	public static partial class Query
	{
		/// <summary>
		/// WordPress Query Options
		/// </summary>
		/// <typeparam name="TId">Entity ID type</typeparam>
		/// <typeparam name="TPrimaryTable">Primary Table type</typeparam>
		public abstract record Options<TId, TPrimaryTable> : QueryOptions<TId>
			where TId : StrongId
			where TPrimaryTable : ITable
		{
			/// <summary>
			/// WordPress Database instance
			/// </summary>
			protected IWpDb Db { get; init; }

			internal IWpDb DbTest =>
				Db;

			/// <summary>
			/// Primary Table
			/// </summary>
			protected TPrimaryTable Table { get; init; }

			internal TPrimaryTable TableTest =>
				Table;

			/// <summary>
			/// IWpDbSchema shorthand
			/// </summary>
			protected IWpDbSchema T =>
				Db.Schema;

			internal IWpDbSchema TTest =>
				T;

			/// <summary>
			/// ID Column selector
			/// </summary>
			protected abstract Expression<Func<TPrimaryTable, string>> IdColumn { get; }

			/// <summary>
			/// Inject dependencies
			/// </summary>
			/// <param name="db">WordPress Database instance</param>
			/// <param name="builder">IQueryPartsBuilder</param>
			/// <param name="table">Primary table</param>
			internal Options(IWpDb db, IQueryPartsBuilder<TId> builder, TPrimaryTable table) : base(builder) =>
				(Db, Table) = (db, table);

			/// <inheritdoc/>
			protected override Option<(ITable table, IColumn idColumn)> GetMap() =>
				from idColumn in GetColumnFromExpression(Table, IdColumn)
				select ((ITable)Table, idColumn);

			#region Testing

			internal Option<(ITable table, IColumn idColumn)> GetMapTest() =>
				GetMap();

			#endregion
		}
	}
}
