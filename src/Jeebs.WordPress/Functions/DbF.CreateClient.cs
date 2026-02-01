// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Adapters.Dapper;
using Jeebs.Data.Clients.MySql;

namespace Jeebs.WordPress.Functions;

public static partial class DbF
{
	/// <summary>
	/// Create a default WordPress database client.
	/// </summary>
	/// <returns>MySqlDbClient.</returns>
	public static MySqlDbClient CreateClient() =>
		new(DapperAdapter.DefaultInstance);
}
