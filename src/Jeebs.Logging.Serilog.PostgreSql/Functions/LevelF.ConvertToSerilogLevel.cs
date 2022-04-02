// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

#if NET6_0
using Jeebs.Messages;
using Npgsql.Logging;
using Serilog.Events;

namespace Jeebs.Logging.Serilog.PostgreSql.Functions;

public static partial class LevelF
{
	/// <summary>
	/// Convert a <see cref="NpgsqlLogLevel"/> to a <see cref="LogEventLevel"/>
	/// </summary>
	/// <param name="level"></param>
	public static Maybe<LogEventLevel> ConvertToSerilogLevel(NpgsqlLogLevel level) =>
		level switch
		{
			NpgsqlLogLevel.Trace =>
				LogEventLevel.Verbose,

			NpgsqlLogLevel.Debug =>
				LogEventLevel.Debug,

			NpgsqlLogLevel.Info =>
				LogEventLevel.Information,

			NpgsqlLogLevel.Warn =>
				LogEventLevel.Warning,

			NpgsqlLogLevel.Error =>
				LogEventLevel.Error,

			NpgsqlLogLevel.Fatal =>
				LogEventLevel.Fatal,

			_ =>
				F.None<LogEventLevel>(new M.UnknownNpgsqlLogLevelMsg(level))
		};

	public static partial class M
	{
		/// <summary>Unknown NpgsqlLogLevel value</summary>
		/// <param name="Value"></param>
		public sealed record class UnknownNpgsqlLogLevelMsg(NpgsqlLogLevel Value) : WithValueMsg<NpgsqlLogLevel>;
	}
}
#endif
