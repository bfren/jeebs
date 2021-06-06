// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using Jeebs.Data;

namespace F.DataF
{
	public static partial class QueryF
	{
		/// <summary>
		/// Get the list of columns for a query, escaped with the alias
		/// </summary>
		/// <param name="client">IDbClient</param>
		/// <param name="columns">ColumnList</param>
		public static List<string> GetColumnsFromList(IDbClient client, List<IColumn> columns)
		{
			var col = new List<string>();
			foreach (var column in columns)
			{
				col.Add(client.Escape(column, true));
			}

			return col;
		}
	}
}
