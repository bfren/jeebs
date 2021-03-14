// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Data;
using System.Linq;
using Jeebs.Data.Enums;

namespace Jeebs.Data
{
	/// <inheritdoc cref="IAdapter"/>
	public abstract partial class Adapter : IAdapter
	{
		/// <inheritdoc/>
		public char SchemaSeparator { get; }

		/// <inheritdoc/>
		public char ColumnSeparator { get; }

		/// <inheritdoc/>
		public char ListSeparator { get; }

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
		/// <param name="listSeparator">List separator string</param>
		/// <param name="escapeOpen">Open escape character</param>
		/// <param name="escapeClose">Close escape character</param>
		/// <param name="alias">Alias keyword</param>
		/// <param name="aliasOpen">Alias open character</param>
		/// <param name="aliasClose">Alias close character</param>
		/// <param name="sortAsc">Sort Ascending string</param>
		/// <param name="sortDesc">Sort Descending string</param>
		protected Adapter(char schemaSeparator, char columnSeparator, char listSeparator, char escapeOpen, char escapeClose, string alias, char aliasOpen, char aliasClose, string sortAsc, string sortDesc)
		{
			SchemaSeparator = schemaSeparator;
			ColumnSeparator = columnSeparator;
			ListSeparator = listSeparator;
			EscapeOpen = escapeOpen;
			EscapeClose = escapeClose;
			Alias = alias;
			AliasOpen = aliasOpen;
			AliasClose = aliasClose;
			SortAsc = sortAsc;
			SortDesc = sortDesc;
		}

		/// <inheritdoc/>
		public string Join(params object[] columns) =>
			Join(from c in columns select c.ToString());

		/// <inheritdoc/>
		public string Join(IEnumerable<string> columns) =>
			string.Join($"{ColumnSeparator} ", from c in columns where !IsInvalidIdentifier(c) select c);

		/// <inheritdoc/>
		public virtual bool IsInvalidIdentifier(string? name)
		{
			return isNullOrEmpty();

			bool isNullOrEmpty() =>
				string.IsNullOrWhiteSpace(name);
		}

		#region Escaping

		/// <inheritdoc/>
		public string Escape(string name)
		{
			// Handle invalid names
			if (IsInvalidIdentifier(name))
			{
				return string.Empty;
			}

			// If the name contains the separator character, use SplitAndEscape() instead
			if (name.Contains(SchemaSeparator))
			{
				return SplitAndEscape(name);
			}

			// Return escaped name
			return $"{EscapeOpen}{name}{EscapeClose}";
		}

		/// <inheritdoc/>
		public string Escape(string name, string alias, string? table = null)
		{
			// Handle invalid names
			if (IsInvalidIdentifier(name))
			{
				return string.Empty;
			}

			// Handle invalid aliases
			if (IsInvalidIdentifier(alias))
			{
				return Escape(name);
			}

			// Escape with alias
			return $"{EscapeAndJoin(table, name)} {Alias} {AliasOpen}{alias}{AliasClose}";
		}

		/// <inheritdoc/>
		public string EscapeTable<TTable>(TTable table)
			where TTable : notnull =>
			Escape(table.ToString() ?? string.Empty);

		/// <inheritdoc/>
		public string SplitAndEscape(string element)
		{
			// Handle empty elements
			if (string.IsNullOrWhiteSpace(element))
			{
				return string.Empty;
			}

			// Split element by the separator
			var elements = element.Split(SchemaSeparator);

			// Now escape the elements and re-join them
			return EscapeAndJoin(elements);
		}

		/// <inheritdoc/>
		public string EscapeAndJoin(params object?[] elements)
		{
			// Handle no elements
			if (elements is null || elements.Length == 0)
			{
				return string.Empty;
			}

			// Escape each element the array
			var list = new List<string>();
			var escaped = elements
				.Filter(x => x?.ToString())
				.Where(x => !IsInvalidIdentifier(x))
				.Filter(x => Escape(x));

			foreach (var element in escaped)
			{
				list.Add(element);
			}

			// Return escaped elements joined with the separator
			return string.Join(SchemaSeparator, list);
		}

		#endregion

		#region Querying

		/// <inheritdoc/>
		public string GetSortOrder(string column, SortOrder order)
		{
			// Handle no column
			if (string.IsNullOrWhiteSpace(column))
			{
				return string.Empty;
			}

			return $"{column} {(order == SortOrder.Ascending ? SortAsc : SortDesc)}";
		}

		/// <inheritdoc/>
		public abstract string GetRandomSortOrder();

		/// <inheritdoc/>
		public virtual string GetSelectCount() =>
			"COUNT(*)";

		/// <inheritdoc/>
		public abstract string CreateSingleAndReturnId(string table, List<string> columns, List<string> aliases);

		/// <inheritdoc/>
		public abstract string Retrieve(IQueryParts parts);

		/// <inheritdoc/>
		public abstract string RetrieveSingleById(string table, List<string> columns, string idColumn, string idAlias = nameof(IEntity.Id));

		/// <inheritdoc/>
		public abstract string UpdateSingle(string table, List<string> columns, List<string> aliases, string idColumn, string idAlias, string? versionColumn = null, string? versionAlias = null);

		/// <inheritdoc/>
		public abstract string DeleteSingle(string table, string idColumn, string idAlias, string? versionColumn = null, string? versionAlias = null);

		#endregion
	}
}
