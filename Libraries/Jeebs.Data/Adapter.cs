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
		/// Create object
		/// </summary>
		/// <param name="separator">Separator character</param>
		/// <param name="escapeOpen">Open escape character</param>
		/// <param name="escapeClose">Close escape character</param>
		protected Adapter(in char separator, in char escapeOpen, in char escapeClose)
		{
			this.separator = separator;
			this.escapeOpen = escapeOpen;
			this.escapeClose = escapeClose;
		}

		#region Escaping

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
		public string EscapeAndJoin(string?[] elements)
		{
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
