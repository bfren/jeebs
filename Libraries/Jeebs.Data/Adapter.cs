using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Jeebs.Data.Enums;

namespace Jeebs.Data
{
	/// <inheritdoc cref="IAdapter"/>
	public abstract class Adapter : IAdapter
	{
		/// <inheritdoc/>
		public char SchemaSeparator { get; }

		/// <inheritdoc/>
		public string ColumnSeparator { get; }

		/// <inheritdoc/>
		public char EscapeOpen { get; }

		/// <inheritdoc/>
		public char EscapeClose { get; }

		/// <inheritdoc/>
		public string Alias { get; }

		/// <inheritdoc/>
		public char AliasOpen { get; }

		/// <inheritdoc/>
		public char AliasClose { get; }

		/// <inheritdoc/>
		public string SortAsc { get; }

		/// <inheritdoc/>
		public string SortDesc { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="schemaSeparator">Schema separator character</param>
		/// <param name="columnSeparator">Column separator string</param>
		/// <param name="escapeOpen">Open escape character</param>
		/// <param name="escapeClose">Close escape character</param>
		/// <param name="alias">Alias keyword</param>
		/// <param name="aliasOpen">Alias open character</param>
		/// <param name="aliasClose">Alias close character</param>
		/// <param name="sortAsc">Sort Ascending string</param>
		/// <param name="sortDesc">Sort Descending string</param>
		protected Adapter(char schemaSeparator, string columnSeparator, char escapeOpen, char escapeClose, string alias, char aliasOpen, char aliasClose, string sortAsc, string sortDesc)
		{
			SchemaSeparator = schemaSeparator;
			ColumnSeparator = columnSeparator;
			EscapeOpen = escapeOpen;
			EscapeClose = escapeClose;
			Alias = alias;
			AliasOpen = aliasOpen;
			AliasClose = aliasClose;
			SortAsc = sortAsc;
			SortDesc = sortDesc;
		}

		#region Escaping

		/// <inheritdoc/>
		public string Escape(string name)
		{
			// Don't allow blank names
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentNullException(nameof(name));
			}

			// If the name contains the separator character, use SplitAndJoin() instead
			if (name.Contains(SchemaSeparator))
			{
				return SplitAndEscape(name);
			}

			// Trim escape characters
			var trimmed = name.Trim(EscapeOpen, EscapeClose);

			// Return escaped name
			return $"{EscapeOpen}{trimmed}{EscapeClose}";
		}

		/// <inheritdoc/>
		public string Escape<TTable>(TTable table) where TTable : notnull => Escape(table.ToString());

		/// <inheritdoc/>
		public string SplitAndEscape(string element)
		{
			// Split an element by the default separator
			var elements = element.Split(SchemaSeparator);

			// Now escape the elements and re-jothem
			return EscapeAndJoin(elements);
		}

		/// <inheritdoc/>
		public string EscapeAndJoin(params object?[] elements)
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
				var str = element?.ToString();

				if (string.IsNullOrWhiteSpace(str))
				{
					continue;
				}

				escaped.Add(Escape(str));
			}

			// Return escaped elements joined with the separator
			return string.Join(SchemaSeparator.ToString(), escaped);
		}

		/// <inheritdoc/>
		public string EscapeColumn(string name, string alias) => $"{Escape(name)} {this.Alias} {AliasOpen}{alias}{AliasClose}";

		#endregion

		#region Querying

		/// <inheritdoc/>
		public string GetSortOrder(string column, SortOrder order) => string.Concat(Escape(column), " ", order == SortOrder.Ascending ? SortAsc : SortDesc);

		/// <inheritdoc/>
		public abstract string GetRandomSortOrder();

		/// <inheritdoc/>
		public virtual string GetSelectCount() => "COUNT(*)";

		/// <inheritdoc/>
		public abstract string CreateSingleAndReturnId(string table, List<string> columns, List<string> aliases);

		/// <inheritdoc/>
		public abstract string Retrieve(IQueryParts parts);

		/// <inheritdoc/>
		public abstract string RetrieveSingleById(List<string> columns, string table, string idColumn);

		/// <inheritdoc/>
		public abstract string UpdateSingle(string table, List<string> columns, List<string> aliases, string idColumn, string idAlias, string? versionColumn = null, string? versionAlias = null);

		/// <inheritdoc/>
		public abstract string DeleteSingle(string table, string idColumn, string idAlias, string? versionColumn = null, string? versionAlias = null);

		#endregion
	}
}
