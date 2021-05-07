// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq.Expressions;
using Jeebs.Data.Clients.MySql;
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
			/// MySQL Client
			/// </summary>
			protected MySqlDbClient Client { get; private init; }

			/// <summary>
			/// WordPress Database instance
			/// </summary>
			protected IWpDb Db { get; init; }

			/// <summary>
			/// Shorthand for Database Schema
			/// </summary>
			protected IWpDbSchema T =>
				Db.Schema;

			/// <summary>
			/// Primary Table
			/// </summary>
			protected TPrimaryTable Table { get; init; }

			/// <summary>
			/// ID Column selector
			/// </summary>
			protected abstract Expression<Func<TPrimaryTable, string>> IdColumn { get; }

			/// <summary>
			/// Inject dependencies
			/// </summary>
			/// <param name="db">WordPress Database instance</param>
			/// <param name="table">Primary table</param>
			protected Options(IWpDb db, TPrimaryTable table) =>
				(Client, Db, Table) = (new MySqlDbClient(), db, table);

			/// <inheritdoc/>
			protected override Option<(ITable table, IColumn idColumn)> GetMap() =>
				from idColumn in GetColumnFromExpression(Table, IdColumn)
				select ((ITable)Table, idColumn);

			/// <summary>
			/// Escape a table
			/// </summary>
			/// <typeparam name="TTable">Table type</typeparam>
			/// <param name="table">Table object</param>
#pragma warning disable IDE1006 // Naming Styles
			protected string __<TTable>(TTable table)
				where TTable : ITable
#pragma warning restore IDE1006 // Naming Styles
			{
				return Client.Escape(table);
			}

			/// <summary>
			/// Get and escape a column using a Linq Expression selector
			/// </summary>
			/// <typeparam name="TTable">Table type</typeparam>
			/// <param name="table">Table object</param>
			/// <param name="selector">Column selector</param>
#pragma warning disable IDE1006 // Naming Styles
			protected string __<TTable>(TTable table, Expression<Func<TTable, string>> selector)
				where TTable : ITable
#pragma warning restore IDE1006 // Naming Styles
			{
				if (GetColumnFromExpression(table, selector) is Some<IColumn> column)
				{
					return Client.EscapeWithTable(column.Value);
				}

				throw new Exception("Unable to get column.");
			}
		}
	}
}
