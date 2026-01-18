// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Messages;
using MySqlConnector.Logging;
using Serilog.Events;

namespace Jeebs.Logging.Serilog.MySql.Functions;

public static partial class LevelF
{
	/// <summary>
	/// Convert a <see cref="MySqlConnectorLogLevel"/> to a <see cref="LogEventLevel"/>.
	/// </summary>
	/// <param name="level"></param>
	public static Maybe<LogEventLevel> ConvertToSerilogLevel(MySqlConnectorLogLevel level) =>
		level switch
		{
			MySqlConnectorLogLevel.Trace =>
				LogEventLevel.Verbose,

			MySqlConnectorLogLevel.Debug =>
				LogEventLevel.Debug,

			MySqlConnectorLogLevel.Info =>
				LogEventLevel.Information,

			MySqlConnectorLogLevel.Warn =>
				LogEventLevel.Warning,

			MySqlConnectorLogLevel.Error =>
				LogEventLevel.Error,

			MySqlConnectorLogLevel.Fatal =>
				LogEventLevel.Fatal,

			_ =>
				F.None<LogEventLevel>(new M.UnknownMySqlConnectorLogLevelMsg(level))
		};

	public static partial class M
	{
		/// <summary>Unknown MySqlConnectorLogLevel value</summary>
		/// <param name="Value"></param>
		public sealed record class UnknownMySqlConnectorLogLevelMsg(MySqlConnectorLogLevel Value) : WithValueMsg<MySqlConnectorLogLevel>;
	}
}
