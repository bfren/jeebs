// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
			where TId : IStrongId
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
			protected PartsBuilder(IWpDbSchema schema) : this(new Extract(), schema) { }

			/// <summary>
			/// Create object
			/// </summary>
			/// <param name="extract">IExtract</param>
			/// <param name="schema">IWpDbSchema</param>
			internal PartsBuilder(IExtract extract, IWpDbSchema schema) : this(extract, new MySqlDbClient(), schema) { }

			/// <summary>
			/// Create object
			/// </summary>
			/// <param name="extract">IExtract</param>
			/// <param name="client">IDbClient</param>
			/// <param name="schema">IWpDbSchema</param>
			internal PartsBuilder(IExtract extract, IDbClient client, IWpDbSchema schema) : base(extract) =>
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
