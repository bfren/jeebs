﻿// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq.Expressions;
using Jeebs.Data;
using Jeebs.Data.Clients.MySql;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying;
using static F.DataF.QueryF;

namespace Jeebs.WordPress.Data
{
	public static partial class Query
	{
		/// <summary>
		/// Common functions for building QueryParts
		/// </summary>
		/// <typeparam name="TId">Entity ID type</typeparam>
		public abstract class PartsBuilder<TId> : QueryPartsBuilder<TId>
			where TId : StrongId
		{
			/// <summary>
			/// IDbClient
			/// </summary>
			protected IDbClient Client { get; private init; }

			internal IDbClient ClientTest =>
				Client;

			/// <summary>
			/// IWpDbSchema
			/// </summary>
			protected IWpDbSchema T { get; private init; }

			internal IWpDbSchema TTest =>
				T;

			/// <summary>
			/// Create object
			/// </summary>
			/// <param name="schema">IWpDbSchema</param>
			protected PartsBuilder(IWpDbSchema schema) : this(new MySqlDbClient(), schema) { }

			/// <summary>
			/// Create object
			/// </summary>
			/// <param name="client">IDbClient</param>
			/// <param name="schema">IWpDbSchema</param>
			internal PartsBuilder(IDbClient client, IWpDbSchema schema) =>
				(Client, T) = (client, schema);

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

			#region Testing

			internal string EscapeTest<TTable>(TTable table)
				where TTable : ITable =>
				__(table);

			internal string EscapeTest<TTable>(TTable table, Expression<Func<TTable, string>> selector)
				where TTable : ITable =>
				__(table, selector);

			#endregion
		}
	}
}