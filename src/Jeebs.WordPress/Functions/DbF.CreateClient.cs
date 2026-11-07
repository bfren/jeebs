// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Adapters.Dapper;
using Jeebs.Data.Clients.MySql;
using Microsoft.Extensions.Logging.Abstractions;

namespace Jeebs.WordPress.Functions;

public static partial class DbF
{
	/// <summary>
	/// Create a default WordPress database client, used to build queries (it does not log, as it never opens a connection).
	/// </summary>
	/// <returns>MySqlDbClient.</returns>
	public static MySqlDbClient CreateClient() =>
		new(DapperAdapter.DefaultInstance, NullLoggerFactory.Instance);
}
