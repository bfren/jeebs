using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Database adapter
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
		/// Setup object
		/// </summary>
		/// <param name="separator">Separator character</param>
		/// <param name="escapeOpen">Open escape character</param>
		/// <param name="escapeClose">Close escape character</param>
		protected Adapter(char separator, char escapeOpen, char escapeClose)
		{
			this.separator = separator;
			this.escapeOpen = escapeOpen;
			this.escapeClose = escapeClose;
		}

		#region Escaping

		/// <summary>
		/// Format and escape a table name
		/// Examples:
		///		prefix_table_name			prefix: prefix_
		///		dbo.prefix_table_name		prefix: dbo.prefix_
		/// </summary>
		/// <param name="name">Table name</param>
		/// <returns>Formatted and escaped table name</returns>
		public string SplitAndEscape(in string name)
		{
			var elements = name.Split(separator);
			return EscapeAndJoin(elements);
		}

		/// <summary>
		/// Escape and then join an array of elements
		/// </summary>
		/// <param name="elements">Elements (table or column names)</param>
		/// <returns>Escaped and joined elements</returns>
		public string EscapeAndJoin(string?[] elements)
		{
			var escaped = new List<string>();
			foreach (var element in elements)
			{
				if (string.IsNullOrEmpty(element))
				{
					continue;
				}

				escaped.Add(Escape(element));
			}

			return string.Join(separator.ToString(), escaped);
		}

		/// <summary>
		/// Escape a table or column name
		/// </summary>
		/// <param name="name">Table or column name</param>
		/// <returns>Escaped name</returns>
		public string Escape(in string name) => $"{escapeOpen}{name}{escapeClose}";

		#endregion

		#region Queries

		/// <summary>
		/// Query to insert a single row and return the new ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <returns>SQL query</returns>
		public abstract string CreateSingleAndReturnId<T>();

		/// <summary>
		/// Query to retrieve a single row by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="id">Entity ID</param>
		/// <returns>SQL query</returns>
		public abstract string RetrieveSingleById<T>(int id);

		/// <summary>
		/// Query to update a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="id">Id value</param>
		/// <param name="version">[Optional] Version</param>
		/// <returns>SQL query</returns>
		public abstract string UpdateSingle<T>(int id, long? version = null);

		#endregion
	}
}
