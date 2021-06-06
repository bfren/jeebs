// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using Jeebs.Data;

namespace F.DataF
{
	public static partial class QueryF
	{
		/// <summary>
		/// Escape and join a list of columns for a select query
		/// </summary>
		/// <param name="client">IDbClient</param>
		/// <param name="columns">Column List</param>
		public static string GetSelectFromList(IDbClient client, List<IColumn> columns)
		{
			var select = new List<string>();
			foreach (var column in columns)
			{
				select.Add(client.EscapeWithTable(column, true));
			}

			return client.JoinList(select, false);
		}
	}
}
