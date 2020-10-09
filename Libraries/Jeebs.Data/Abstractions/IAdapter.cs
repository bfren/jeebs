using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Enums;

namespace Jeebs.Data
{
	/// <summary>
	/// Enables database-agnostic escaping and queries
	/// </summary>
	public interface IAdapter
	{
		/// <summary>
		/// Schema separator character
		/// </summary>
		char SchemaSeparator { get; }

		/// <summary>
		/// Select column separator character
		/// </summary>
		char ColumnSeparator { get; }

		/// <summary>
		/// List separator character
		/// </summary>
		char ListSeparator { get; }

		/// <summary>
		/// Escape character
		/// </summary>
		char EscapeOpen { get; }

		/// <summary>
		/// Escape character
		/// </summary>
		char EscapeClose { get; }

		/// <summary>
		/// Alias keyword
		/// </summary>
		string Alias { get; }

		/// <summary>
		/// Alias open character
		/// </summary>
		char AliasOpen { get; }

		/// <summary>
		/// Alias open character
		/// </summary>
		char AliasClose { get; }

		/// <summary>
		/// Sort ascending text
		/// </summary>
		string SortAsc { get; }

		/// <summary>
		/// Sort descending text
		/// </summary>
		string SortDesc { get; }

		/// <summary>
		/// Returns true if the specified <paramref name="name"/> is an invalid table / column name / alias
		/// </summary>
		/// <param name="name">Table / column name or alias</param>
		bool IsInvalidIdentifier(string name);

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
		string EscapeAndJoin(params object?[] elements);

		/// <summary>
		/// Escape a column using its table and alias
		/// </summary>
		/// <param name="name">Column name</param>
		/// <param name="alias">Column alias</param>
		/// <param name="table">Table name</param>
		string EscapeColumn(string name, string alias, string? table = null);

		#endregion

		#region Querying

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

		/// <summary>
		/// SELECT columns to return a COUNT query
		/// </summary>
		string GetSelectCount();

		/// <summary>
		/// Query to insert a single row and return the new ID
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="columns">Columns (actual column names in database)</param>
		/// <param name="aliases">Aliases (parameter names / POCO property names)</param>
		string CreateSingleAndReturnId(string table, List<string> columns, List<string> aliases);

		/// <summary>
		/// Build a SELECT query
		/// </summary>
		/// <param name="parts">IQueryParts</param>
		/// <returns>SELECT query</returns>
		string Retrieve(IQueryParts parts);

		/// <summary>
		/// Query to retrieve a single row by ID
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="columns">The columns to SELECT</param>
		/// <param name="idColumn">ID column</param>
		/// <param name="idAlias">[Optional] ID Alias (parameter name / POCO property name) - should normally be <see cref="IEntity.Id"/></param>
		string RetrieveSingleById(string table, List<string> columns, string idColumn, string idAlias = nameof(IEntity.Id));

		/// <summary>
		/// Query to update a single row
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="columns">Columns (actual column names in database)</param>
		/// <param name="aliases">Aliases (parameter names / POCO property names)</param>
		/// <param name="idColumn">ID column (actual column name in database)</param>
		/// <param name="idAlias">ID alias (parameter name / POCO property name)</param>
		/// <param name="versionColumn">[Optional] Version column (actual column name in database)</param>
		/// <param name="versionAlias">[Optional] Version alias (parameter name / POCO property name)</param>
		string UpdateSingle(string table, List<string> columns, List<string> aliases, string idColumn, string idAlias, string? versionColumn = null, string? versionAlias = null);

		/// <summary>
		/// Query to delete a single row
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="idColumn">ID column (actual column name in database)</param>
		/// <param name="idAlias">ID alias (parameter name / POCO property name)</param>
		/// <param name="versionColumn">[Optional] Version column (actual column name in database)</param>
		/// <param name="versionAlias">[Optional] Version alias (parameter name / POCO property name)</param>
		string DeleteSingle(string table, string idColumn, string idAlias, string? versionColumn = null, string? versionAlias = null);

		#endregion
	}
}
