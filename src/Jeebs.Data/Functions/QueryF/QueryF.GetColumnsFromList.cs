// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using Jeebs.Data;
using Jeebs.Data.Mapping;

namespace F.DataF
{
	public static partial class QueryF
	{
		/// <summary>
		/// Get the list of columns for a query, escaped with the alias
		/// </summary>
		/// <param name="client">IDbClient</param>
		/// <param name="columns">IColumnList</param>
		public static List<string> GetColumnsFromList(IDbClient client, IColumnList columns)
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
