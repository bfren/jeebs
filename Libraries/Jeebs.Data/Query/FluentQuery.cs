using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Fluent Query
	/// </summary>
	/// <typeparam name="T">Model type</typeparam>
	public sealed class FluentQuery<T> : IFluentQuery<T>
	{
		/// <summary>
		/// IAdapter
		/// </summary>
		private readonly IAdapter adapter;

		/// <summary>
		/// FluentQueryParameters
		/// </summary>
		public FluentQueryParameters Parameters { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="adapter">IAdapter</param>
		internal FluentQuery(IAdapter adapter)
		{
			this.adapter = adapter;
			Parameters = new FluentQueryParameters();
		}

		/// <summary>
		/// SQL builder objects
		/// </summary>
		private readonly List<string> select = new List<string>();
		private string from = string.Empty;
		private readonly List<string> innerJoin = new List<string>();
		private readonly List<string> leftJoin = new List<string>();
		private readonly List<string> rightJoin = new List<string>();
		private string where = string.Empty;
		private string? orderBy;
		private double? limit;
		private double? offset;

		/// <summary>
		/// Select extracted columns
		/// </summary>
		/// <param name="columns">ExtractedColumns</param>
		public IFluentQuery<T> Select(ExtractedColumns columns)
		{
			// Add columns
			foreach (var item in columns)
			{
				select.Add(adapter.GetColumn(item));
			}

			// Return
			return this;
		}

		/// <summary>
		/// Select columns from an entity table
		/// </summary>
		/// <typeparam name="TTable">Table type</typeparam>
		/// <param name="table">Entity table</param>
		public IFluentQuery<T> SelectFromTable<TTable>(TTable table)
			where TTable : ITable
		{
			// Extract and Select table columns
			Select(table.Extract<T>());

			// Escape table name and add to from
			from = adapter.Escape(table);

			// Return
			return this;
		}

		/// <summary>
		/// Select columns
		/// </summary>
		/// <param name="tables">Array of tables to select columns from</param>
		public IFluentQuery<T> SelectFromTables(params ITable[] tables)
		{
			// Extract columns from each table
			foreach (var item in tables)
			{
				Select(item.Extract<T>());
			}

			// Return
			return this;
		}

		public IFluentQuery<T> From(string name)
		{
			// Escape table name
			from = adapter.Escape(name);

			// Return
			return this;
		}
	}
}
