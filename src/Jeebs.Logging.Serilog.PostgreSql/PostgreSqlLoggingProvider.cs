// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

#if NET6_0
using Npgsql.Logging;

namespace Jeebs.Logging.Serilog.PostgreSql;

/// <summary>
/// Create logger instances for Npgsql
/// </summary>
public sealed class PostgreSqlLoggingProvider : INpgsqlLoggingProvider
{
	/// <summary>
	/// Create named logger
	/// </summary>
	/// <param name="name">Logger name</param>
	public NpgsqlLogger CreateLogger(string name) =>
		new PostgreSqlLogger(name);
}
#endif
