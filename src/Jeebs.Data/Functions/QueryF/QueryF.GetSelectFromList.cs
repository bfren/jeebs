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
		/// Escape and join a list of columns for a select query
		/// </summary>
		/// <param name="client">IDbClient</param>
		/// <param name="columns">IColumnList</param>
		public static string GetSelectFromList(IDbClient client, IColumnList columns)
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
