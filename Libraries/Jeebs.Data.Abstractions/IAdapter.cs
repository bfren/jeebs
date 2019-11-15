using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	public interface IAdapter
	{
		#region Escaping

		/// <summary>
		/// Format and escape a table name
		/// </summary>
		/// <example>prefix_table_name			prefix: prefix_</example>
		/// <example>dbo.prefix_table_name		prefix: dbo.prefix_</example>
		/// <param name="name">Table name</param>
		/// <returns>Formatted and escaped table name</returns>
		string SplitAndEscape(in string name);

		/// <summary>
		/// Escape and then join an array of elements
		/// </summary>
		/// <param name="elements">Elements (table or column names)</param>
		/// <returns>Escaped and joined elements</returns>
		string EscapeAndJoin(string?[] elements);

		/// <summary>
		/// Escape a table or column name
		/// </summary>
		/// <param name="name">Table or column name</param>
		/// <returns>Escaped name</returns>
		string Escape(in string name);

		#endregion

		#region Queries - Create

		/// <summary>
		/// Query to insert a single row and return the new ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <returns>SQL query</returns>
		string CreateSingleAndReturnId<T>();

		#endregion

		#region Queries - Retrieve

		/// <summary>
		/// Query to retrieve a single row by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="id">Entity ID</param>
		/// <returns>SQL query</returns>
		string RetrieveSingleById<T>(int id);

		#endregion

		#region Queries - Update

		/// <summary>
		/// Query to update a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="id">Id value</param>
		/// <param name="version">[Optional] Version</param>
		/// <returns>SQL query</returns>
		string UpdateSingle<T>(int id, long? version = null);

		#endregion

		#region Queries - Delete

		/// <summary>
		/// Query to delete a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="id">Id value</param>
		/// <returns>SQL query</returns>
		string DeleteSingle<T>(int id);

		#endregion
	}
}
