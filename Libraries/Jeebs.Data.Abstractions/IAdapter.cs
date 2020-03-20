using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Enums;

namespace Jeebs.Data
{
	/// <summary>
	/// Contains Escaping and Queries for a database
	/// </summary>
	public interface IAdapter
	{
		#region Escaping

		/// <summary>
		/// Escape a table or column name
		/// </summary>
		/// <param name="name">Table or column name</param>
		/// <returns>Escaped name</returns>
		string Escape(string name);

		/// <summary>
		/// Escape a table name
		/// </summary>
		/// <typeparam name="TTable">Table type</typeparam>
		/// <param name="table">Mapped Table</param>
		/// <returns>Escaped name</returns>
		string Escape<TTable>(TTable table) where TTable : notnull;

		/// <summary>
		/// Split a string by '.', escape the elements, and rejothem
		/// </summary>
		/// <param name="element">Elemnts (table or column names)</param>
		/// <returns>Escaped and joined elements</returns>
		string SplitAndEscape(string element);

		/// <summary>
		/// Escape and then join array of elements
		/// </summary>
		/// <param name="elements">Elements (table or column names)</param>
		/// <returns>Escaped and joined elements</returns>
		string EscapeAndJoin(params string?[] elements);

		/// <summary>
		/// Joa list of ExtractedColumn objects
		/// </summary>
		/// <param name="columns">ExtractedColumns</param>
		string Join(ExtractedColumns columns);

		/// <summary>
		/// Get an ExtractedColumn
		/// </summary>
		/// <param name="col">ExtractedColumn</param>
		string GetColumn(ExtractedColumn col);

		/// <summary>
		/// Get a MappedColumn
		/// </summary>
		/// <param name="col">MappedColumn</param>
		string GetColumn(MappedColumn col);

		#endregion

		#region Sorting

		/// <summary>
		/// Return a sort order string
		/// </summary>
		/// <param name="column">Column name (unescaped)</param>
		/// <param name="order">QuerySortOrder</param>
		/// <returns>Sort order</returns>
		string GetSortOrder(string column, SortOrder order);

		/// <summary>
		/// Return random sort string
		/// </summary>
		string GetRandomSortOrder();

		#endregion

		#region Queries - Create

		/// <summary>
		/// Query to insert a single row and return the new ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		string CreateSingleAndReturnId<T>();

		#endregion

		#region Queries - Retrieve

		/// <summary>
		/// SELECT columns to return a COUNT query
		/// </summary>
		string GetSelectCount();

		/// <summary>
		/// Query to retrieve a single row by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		string RetrieveSingleById<T>();

		/// <summary>
		/// Build a SELECT query
		/// </summary>
		/// <param name="args">IQuery</param>
		string Retrieve(QueryArgs args);

		#endregion

		#region Queries - Update

		/// <summary>
		/// Query to update a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		string UpdateSingle<T>();

		#endregion

		#region Queries - Delete

		/// <summary>
		/// Query to delete a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		string DeleteSingle<T>();

		#endregion
	}
}
