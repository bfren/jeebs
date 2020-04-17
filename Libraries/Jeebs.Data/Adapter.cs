using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Jeebs.Data.Enums;

namespace Jeebs.Data
{
	/// <summary>
	/// Contains Escaping and Queries for a database
	/// </summary>
	public abstract class Adapter : IAdapter
	{
		/// <summary>
		/// Separator character
		/// </summary>
		public char Separator { get; }

		/// <summary>
		/// Escape character
		/// </summary>
		public char EscapeOpen { get; }

		/// <summary>
		/// Escape character
		/// </summary>
		public char EscapeClose { get; }

		/// <summary>
		/// Alias keyword
		/// </summary>
		public string Alias { get; }

		/// <summary>
		/// Alias open character
		/// </summary>
		public char AliasOpen { get; }

		/// <summary>
		/// Alias open character
		/// </summary>
		public char AliasClose { get; }

		/// <summary>
		/// Sort ascending text
		/// </summary>
		public string SortAsc { get; }

		/// <summary>
		/// Sort descending text
		/// </summary>
		public string SortDesc { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="separator">Separator character</param>
		/// <param name="escapeOpen">Open escape character</param>
		/// <param name="escapeClose">Close escape character</param>
		/// <param name="alias">Alias keyword</param>
		/// <param name="aliasOpen">Alias open character</param>
		/// <param name="aliasClose">Alias close character</param>
		/// <param name="sortAsc">Sort Ascending string</param>
		/// <param name="sortDesc">Sort Descending string</param>
		protected Adapter(char separator, char escapeOpen, char escapeClose, string alias, char aliasOpen, char aliasClose, string sortAsc, string sortDesc)
		{
			Separator = separator;
			EscapeOpen = escapeOpen;
			EscapeClose = escapeClose;
			Alias = alias;
			AliasOpen = aliasOpen;
			AliasClose = aliasClose;
			SortAsc = sortAsc;
			SortDesc = sortDesc;
		}

		#region Escaping

		/// <summary>
		/// Escape a table or column name
		/// If <paramref name="name"/> contains the separator character, <see cref="SplitAndEscape(string)"/> will be used instead
		/// </summary>
		/// <exception cref="ArgumentNullException">If <paramref name="name"/> is null, empty, or whitespace</exception>
		/// <param name="name">Table or column name</param>
		/// <returns>Escaped name</returns>
		public string Escape(string name)
		{
			// Don't allow blank names
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentNullException(nameof(name));
			}

			// If the name contains the separator character, use SplitAndJoin() instead
			if (name.Contains(Separator))
			{
				return SplitAndEscape(name);
			}

			// Trim escape characters
			var trimmed = name.Trim(EscapeOpen, EscapeClose);

			// Return escaped name
			return $"{EscapeOpen}{trimmed}{EscapeClose}";
		}

		/// <summary>
		/// Escape a table name
		/// </summary>
		/// <typeparam name="TTable">Table type</typeparam>
		/// <param name="table">Mapped Table</param>
		/// <returns>Escaped name</returns>
		public string Escape<TTable>(TTable table) where TTable : notnull => Escape(table.ToString());

		/// <summary>
		/// Split a string by '.', escape the elements, and rejothem
		/// </summary>
		/// <param name="element">Elemnts (table or column names)</param>
		/// <returns>Escaped and joined elements</returns>
		public string SplitAndEscape(string element)
		{
			// Split an element by the default separator
			var elements = element.Split(Separator);

			// Now escape the elements and re-jothem
			return EscapeAndJoin(elements);
		}

		/// <summary>
		/// Escape and then joan array of elements
		/// </summary>
		/// <param name="elements">Elements (table or column names)</param>
		/// <returns>Escaped and joined elements</returns>
		public string EscapeAndJoin(params string?[] elements)
		{
			// Check for no elements
			if (elements.Length == 0)
			{
				throw new ArgumentNullException(nameof(elements));
			}

			// The list of elements to escape
			var escaped = new List<string>();

			// Escape each element the array, skipping elements that are null / empty / whitespace
			foreach (var element in elements)
			{
				if (string.IsNullOrWhiteSpace(element))
				{
					continue;
				}

				escaped.Add(Escape(element));
			}

			// Return escaped elements joined with the separator
			return string.Join(Separator.ToString(), escaped);
		}

		/// <summary>
		/// Escape a column using its table and alias
		/// </summary>
		/// <param name="name">Column name</param>
		/// <param name="alias">Column alias</param>
		public string EscapeColumn(string name, string alias) => $"{Escape(name)} {this.Alias} {AliasOpen}{alias}{AliasClose}";

		#endregion

		/// <summary>
		/// Return a sort order string (ASC or DESC)
		/// </summary>
		/// <param name="column">Column name (unescaped)</param>
		/// <param name="order">QuerySortOrder</param>
		/// <returns>Sort order</returns>
		public string GetSortOrder(string column, SortOrder order) => string.Concat(Escape(column), " ", order == SortOrder.Ascending ? SortAsc : SortDesc);

		/// <summary>
		/// Return random sort string
		/// </summary>
		public abstract string GetRandomSortOrder();

		/// <summary>
		/// SELECT columns to return a COUNT query
		/// </summary>
		public virtual string GetSelectCount() => "COUNT(*)";

		/// <summary>
		/// Query to insert a single row and return the new ID
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="columns">Columns (actual column names in database)</param>
		/// <param name="aliases">Aliases (parameter names / POCO property names)</param>
		public abstract string CreateSingleAndReturnId(string table, List<string> columns, List<string> aliases);

		/// <summary>
		/// Build a SELECT query
		/// </summary>
		/// <param name="parts">IQueryParts</param>
		/// <returns>SELECT query</returns>
		public abstract string Retrieve(IQueryParts parts);

		/// <summary>
		/// Query to retrieve a single row by ID
		/// </summary>
		/// <param name="columns">The columns to SELECT</param>
		/// <param name="table">Table name</param>
		/// <param name="idColumn">ID column</param>
		public abstract string RetrieveSingleById(List<string> columns, string table, string idColumn);

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
		public abstract string UpdateSingle(string table, List<string> columns, List<string> aliases, string idColumn, string idAlias, string? versionColumn = null, string? versionAlias = null);

		/// <summary>
		/// Query to delete a single row
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="idColumn">ID column (actual column name in database)</param>
		/// <param name="idAlias">ID alias (parameter name / POCO property name)</param>
		/// <param name="versionColumn">[Optional] Version column (actual column name in database)</param>
		/// <param name="versionAlias">[Optional] Version alias (parameter name / POCO property name)</param>
		public abstract string DeleteSingle(string table, string idColumn, string idAlias, string? versionColumn = null, string? versionAlias = null);
	}
}
