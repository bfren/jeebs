// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using Jeebs.Data;
using Jeebs.Data.Mapping;

namespace F.DataF
{
	public static partial class QueryF
	{
		/// <summary>
		/// Escape and join a list of columns for a select query
		/// </summary>
		/// <param name="client">IDbClient</param>
		/// <param name="columns">IColumnList</param>
		public static string GetSelectFromList(IDbClient client, IColumnList columns)
		{
			// Do nothing if there are no columns
			if (columns.Count == 0)
			{
				return string.Empty;
			}

			// Escape and add each column to the select list
			var select = new List<string>();
			foreach (var column in columns)
			{
				select.Add(client.EscapeWithTable(column, true));
			}

			// Join without wrapping
			return client.JoinList(select, false);
		}
	}
}
