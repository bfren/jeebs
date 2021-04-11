// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq.Expressions;
using Jeebs.Data.Clients.MySql;
using Jeebs.Data.Mapping;
using static F.DataF.QueryF;

namespace Jeebs.WordPress.Data.Querying
{
	/// <summary>
	/// WordPress Query Options
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TId">Entity ID type</typeparam>
	public abstract record QueryOptions<TEntity, TId> : Jeebs.Data.Querying.QueryOptions<TEntity, TId>
		where TEntity : IWithId<TId>
		where TId : StrongId
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
		/// Inject dependencies
		/// </summary>
		/// <param name="db">WordPress Database instance</param>
		protected QueryOptions(IWpDb db) =>
			(Client, Db) = (new MySqlDbClient(), db);

		/// <summary>
		/// Escape a table
		/// </summary>
		/// <typeparam name="TTable">Table type</typeparam>
		/// <param name="table">Table object</param>
#pragma warning disable IDE1006 // Naming Styles
		protected string __<TTable>(TTable table)
#pragma warning restore IDE1006 // Naming Styles
			where TTable : ITable =>
			Client.Escape(table);

		/// <summary>
		/// Get and escape a column using a Linq Expression selector
		/// </summary>
		/// <typeparam name="TTable">Table type</typeparam>
		/// <param name="table">Table object</param>
		/// <param name="selector">Column selector</param>
#pragma warning disable IDE1006 // Naming Styles
		protected string __<TTable>(TTable table, Expression<Func<TTable, string>> selector)
#pragma warning restore IDE1006 // Naming Styles
			where TTable : ITable
		{
			if (GetColumnFromExpression(table, selector) is Some<IColumn> column)
			{
				return Client.EscapeWithTable(column.Value, true);
			}

			throw new Exception("Unable to get column.");
		}
	}
}
