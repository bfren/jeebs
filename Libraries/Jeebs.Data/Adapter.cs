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
		private readonly char separator;

		/// <summary>
		/// Escape character
		/// </summary>
		private readonly char escapeOpen;

		/// <summary>
		/// Escape character
		/// </summary>
		private readonly char escapeClose;

		/// <summary>
		/// Alias keyword
		/// </summary>
		private readonly string alias;

		/// <summary>
		/// Alias open character
		/// </summary>
		private readonly char aliasOpen;

		/// <summary>
		/// Alias open character
		/// </summary>
		private readonly char aliasClose;

		/// <summary>
		/// Sort ascending text
		/// </summary>
		private readonly string sortAsc;

		/// <summary>
		/// Sort descending text
		/// </summary>
		private readonly string sortDesc;

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
			this.separator = separator;
			this.escapeOpen = escapeOpen;
			this.escapeClose = escapeClose;
			this.alias = alias;
			this.aliasOpen = aliasOpen;
			this.aliasClose = aliasClose;
			this.sortAsc = sortAsc;
			this.sortDesc = sortDesc;
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
			if (name.Contains(separator))
			{
				return SplitAndEscape(name);
			}

			// Trim escape characters
			var trimmed = name.Trim(escapeOpen, escapeClose);

			// Return escaped name
			return $"{escapeOpen}{trimmed}{escapeClose}";
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
			var elements = element.Split(separator);

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
			return string.Join(separator.ToString(), escaped);
		}

		/// <summary>
		/// Join a list of ExtractedColumn objects
		/// </summary>
		/// <param name="columns">ExtractedColumns</param>
		public string Join(ExtractedColumns columns)
		{
			// Get each column
			var select = new List<string>();
			foreach (var c in columns)
			{
				select.Add(GetColumn(c));
			}

			// Return joined with a comma
			return string.Join(", ", select);
		}

		/// <summary>
		/// Get an ExtractedColumn
		/// </summary>
		/// <param name="col">ExtractedColumn</param>
		public string GetColumn(ExtractedColumn col) => Column(string.Concat(col.Table, separator, col.Column), col.Alias);

		/// <summary>
		/// Get a MappedColumn
		/// </summary>
		/// <param name="col">MappedColumn</param>
		public string GetColumn(MappedColumn col) => Column(col.Column, col.Property.Name);

		/// <summary>
		/// Escape a column using its table and alias
		/// </summary>
		/// <param name="name">Column name</param>
		/// <param name="alias">Column alias</param>
		private string Column(string name, string alias) => $"{Escape(name)} {this.alias} {aliasOpen}{alias}{aliasClose}";

		#endregion

		#region Sorting

		/// <summary>
		/// Return a sort order string (ASC or DESC)
		/// </summary>
		/// <param name="column">Column name (unescaped)</param>
		/// <param name="order">QuerySortOrder</param>
		/// <returns>Sort order</returns>
		public string GetSortOrder(string column, SortOrder order) => string.Concat(Escape(column), " ", order == SortOrder.Ascending ? sortAsc : sortDesc);

		/// <summary>
		/// Return random sort string
		/// </summary>
		public abstract string GetRandomSortOrder();

		#endregion

		#region Queries

		/// <summary>
		/// Query to insert a single row and return the new ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		public abstract string CreateSingleAndReturnId<T>();

		/// <summary>
		/// SELECT columns to return a COUNT query
		/// </summary>
		public virtual string GetSelectCount() => "COUNT(*)";

		/// <summary>
		/// Query to retrieve a single row by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		public abstract string RetrieveSingleById<T>();

		/// <summary>
		/// Build a SELECT query
		/// </summary>
		/// <param name="parts">QueryParts</param>
		public abstract string Retrieve<T>(QueryParts<T> parts);

		/// <summary>
		/// Query to update a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		public abstract string UpdateSingle<T>();

		/// <summary>
		/// Query to delete a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		public abstract string DeleteSingle<T>();

		#endregion
	}
}
