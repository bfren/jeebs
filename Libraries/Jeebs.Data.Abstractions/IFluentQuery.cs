using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	public interface IFluentQuery<T>
	{
		/// <summary>
		/// FluentQueryParameters
		/// </summary>
		FluentQueryParameters Parameters { get; }

		/// <summary>
		/// Select extracted columns
		/// </summary>
		/// <param name="columns">ExtractedColumns</param>
		IFluentQuery<T> Select(ExtractedColumns columns);

		/// <summary>
		/// Select columns from an entity table
		/// </summary>
		/// <typeparam name="TTable">Table type</typeparam>
		/// <param name="table">Entity table</param>
		public IFluentQuery<T> SelectFromTable<TTable>(TTable table)
			where TTable : ITable;

		/// <summary>
		/// Select columns
		/// </summary>
		/// <param name="tables">Array of tables to select columns from</param>
		IFluentQuery<T> SelectFromTables(params ITable[] tables);
	}
}
