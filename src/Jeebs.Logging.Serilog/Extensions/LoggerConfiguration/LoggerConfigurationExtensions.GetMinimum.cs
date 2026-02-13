// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging.Serilog.Exceptions;
using Jeebs.Logging.Serilog.Functions;
using Serilog.Events;
using Wrap.Logging;

namespace Jeebs.Logging.Serilog;

public static partial class LoggerConfigurationExtensions
{
	/// <summary>
	/// Returns <paramref name="testMinimum"/> if it is not null and greater than <paramref name="overallMinimum"/> -
	/// otherwise returns <paramref name="overallMinimum"/>
	/// </summary>
	/// <param name="testMinimum">Potential minimum log level.</param>
	/// <param name="overallMinimum">System minimum log level.</param>
	/// <exception cref="LoadFromJeebsConfigException"></exception>
	/// <returns>The greater of two minimum log levels.</returns>
	internal static LogEventLevel GetMinimum(LogLevel? testMinimum, LogLevel overallMinimum)
	{
		var min = testMinimum switch
		{
			LogLevel individualMinimum when individualMinimum > overallMinimum =>
				individualMinimum,

			_ =>
				overallMinimum
		};

		return LevelF
			.ConvertToSerilogLevel(min)
			.Unwrap(v => throw new LoadFromJeebsConfigException(v));
	}
}
