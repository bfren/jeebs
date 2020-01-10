using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

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
		/// Create object
		/// </summary>
		/// <param name="separator">Separator character</param>
		/// <param name="escapeOpen">Open escape character</param>
		/// <param name="escapeClose">Close escape character</param>
		/// <param name="alias">Alias keyword</param>
		/// <param name="aliasOpen">Alias open character</param>
		/// <param name="aliasClose">Alias close character</param>
		protected Adapter(in char separator, in char escapeOpen, in char escapeClose, in string alias, in char aliasOpen, in char aliasClose)
		{
			this.separator = separator;
			this.escapeOpen = escapeOpen;
			this.escapeClose = escapeClose;
			this.alias = alias;
			this.aliasOpen = aliasOpen;
			this.aliasClose = aliasClose;
		}

		#region Escaping

		/// <summary>
		/// Escape a table or column name
		/// If <paramref name="name"/> contains the separator character, <see cref="SplitAndEscape(in string)"/> will be used instead
		/// </summary>
		/// <exception cref="ArgumentNullException">If <paramref name="name"/> is null, empty, or whitespace</exception>
		/// <param name="name">Table or column name</param>
		/// <returns>Escaped name</returns>
		public string Escape(in string name)
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
		public string Escape<TTable>(in TTable table) where TTable : notnull => Escape(table.ToString());

		/// <summary>
		/// Split a string by '.', escape the elements, and rejoin them
		/// </summary>
		/// <param name="element">Elemnts (table or column names)</param>
		/// <returns>Escaped and joined elements</returns>
		public string SplitAndEscape(in string element)
		{
			// Split an element by the default separator
			var elements = element.Split(separator);

			// Now escape the elements and re-join them
			return EscapeAndJoin(elements);
		}

		/// <summary>
		/// Escape and then join an array of elements
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

			// Escape each element in the array, skipping elements that are null / empty / whitespace
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
		public string Join(in ExtractedColumns columns)
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
		public string GetColumn(in ExtractedColumn col) => Column(string.Concat(col.Table, separator, col.Column), col.Alias);

		/// <summary>
		/// Get a MappedColumn
		/// </summary>
		/// <param name="col">MappedColumn</param>
		public string GetColumn(in MappedColumn col) => Column(col.Column, col.Property.Name);

		/// <summary>
		/// Escape a column using its table and alias
		/// </summary>
		/// <param name="name">Column name</param>
		/// <param name="alias">Column alias</param>
		private string Column(string name, string alias) => $"{name} {this.alias} {aliasOpen}{alias}{aliasClose}";

		#endregion

		#region Queries

		/// <summary>
		/// Query to insert a single row and return the new ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		public abstract string CreateSingleAndReturnId<T>();

		/// <summary>
		/// Query to retrieve a single row by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		public abstract string RetrieveSingleById<T>();

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
