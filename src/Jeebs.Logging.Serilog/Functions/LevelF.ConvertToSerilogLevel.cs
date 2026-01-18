// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Messages;
using Serilog.Events;

namespace Jeebs.Logging.Serilog.Functions;

public static partial class LevelF
{
	/// <summary>
	/// Convert a <see cref="LogLevel"/> to a <see cref="LogEventLevel"/>.
	/// </summary>
	/// <param name="level"></param>
	public static Maybe<LogEventLevel> ConvertToSerilogLevel(LogLevel level) =>
		level switch
		{
			LogLevel.Verbose =>
				LogEventLevel.Verbose,

			LogLevel.Debug =>
				LogEventLevel.Debug,

			LogLevel.Information =>
				LogEventLevel.Information,

			LogLevel.Warning =>
				LogEventLevel.Warning,

			LogLevel.Error =>
				LogEventLevel.Error,

			LogLevel.Fatal =>
				LogEventLevel.Fatal,

			LogLevel.Unknown =>
				LogEventLevel.Information,

			_ =>
				F.None<LogEventLevel>(new M.UnknownLogLevelMsg(level))
		};

	public static partial class M
	{
		/// <summary>Unknown LogLevel value</summary>
		/// <param name="Value"></param>
		public sealed record class UnknownLogLevelMsg(LogLevel Value) : WithValueMsg<LogLevel>;
	}
}
