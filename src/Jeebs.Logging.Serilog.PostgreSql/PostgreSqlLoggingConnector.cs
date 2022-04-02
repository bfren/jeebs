// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config;
#if NET6_0
using Npgsql.Logging;
#elif NET7_0_OR_GREATER
using Npgsql;
using Serilog.Extensions.Logging;
#endif

namespace Jeebs.Logging.Serilog.PostgreSql;

/// <summary>
/// Connects Npgsql log to Serilog
/// </summary>
public sealed class PostgreSqlLoggingConnector : ILoggingConnector
{
	/// <inheritdoc/>
	public void Enable(global::Serilog.LoggerConfiguration serilog, JeebsConfig jeebs) =>
#if NET6_0
		NpgsqlLogManager.Provider = new PostgreSqlLoggingProvider();
#elif NET7_0_OR_GREATER
		NpgsqlLoggingConfiguration.InitializeLogging(
			new SerilogLoggerFactory(global::Serilog.Log.Logger.ForContext<NpgsqlConnection>(), true), true
		);
#endif
}
