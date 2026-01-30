// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs.Data.Map;

namespace Jeebs.Data.Common.FluentQuery;

public static partial class QueryF
{
	/// <summary>
	/// Get the list of columns for a query, escaped with the alias.
	/// </summary>
	/// <param name="client">IDbClient.</param>
	/// <param name="columns">IColumnList.</param>
	public static List<string> GetColumnsFromList(IDbClient client, IColumnList columns)
	{
		// Create empty list
		var col = new List<string>();

		// Escape and Add each column
		foreach (var column in columns)
		{
			col.Add(client.Escape(column, true));
		}

		// Return list
		return col;
	}
}
