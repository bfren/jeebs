// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Serilog.Events;
using Wrap.Logging;

namespace Jeebs.Logging.Serilog.Functions;

public static partial class LevelF
{
	/// <summary>
	/// Convert a <see cref="LogLevel"/> to a <see cref="LogEventLevel"/>.
	/// </summary>
	/// <param name="level">Wrap log level.</param>
	/// <returns>Serilog log level.</returns>
	public static Result<LogEventLevel> ConvertToSerilogLevel(LogLevel level) =>
		level switch
		{
			LogLevel.Unknown & LogLevel.Verbose =>
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

			_ =>
				R.Fail("Unknown LogLevel: {Level}.", level)
		};
}
