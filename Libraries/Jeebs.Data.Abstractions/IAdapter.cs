using System;
using System.Collections.Generic;
using System.Text;

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
		string Escape(in string name);

		/// <summary>
		/// Escape a table name
		/// </summary>
		/// <typeparam name="TTable">Table type</typeparam>
		/// <param name="table">Mapped Table</param>
		/// <returns>Escaped name</returns>
		string Escape<TTable>(in TTable table) where TTable : notnull;

		/// <summary>
		/// Split a string by '.', escape the elements, and rejoin them
		/// </summary>
		/// <param name="element">Elemnts (table or column names)</param>
		/// <returns>Escaped and joined elements</returns>
		string SplitAndEscape(in string element);

		/// <summary>
		/// Escape and then join an array of elements
		/// </summary>
		/// <param name="elements">Elements (table or column names)</param>
		/// <returns>Escaped and joined elements</returns>
		string EscapeAndJoin(params string?[] elements);

		/// <summary>
		/// Join a list of ExtractedColumn objects
		/// </summary>
		/// <param name="columns">ExtractedColumns</param>
		string Join(in ExtractedColumns columns);

		/// <summary>
		/// Get an ExtractedColumn
		/// </summary>
		/// <param name="col">ExtractedColumn</param>
		string GetColumn(in ExtractedColumn col);

		/// <summary>
		/// Get a MappedColumn
		/// </summary>
		/// <param name="col">MappedColumn</param>
		string GetColumn(in MappedColumn col);

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
		/// Query to retrieve a single row by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		string RetrieveSingleById<T>();

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
