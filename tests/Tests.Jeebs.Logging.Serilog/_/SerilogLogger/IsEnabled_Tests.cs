// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Serilog;
using Wrap.Logging;
using MS = Microsoft.Extensions.Logging;

namespace Jeebs.Logging.Serilog.SerilogLogger_Tests;

public class IsEnabled_Tests
{
	private static SerilogLogger GetLogger() =>
		new(new LoggerConfiguration().MinimumLevel.Information().CreateLogger());

	[Theory]
	[InlineData(LogLevel.Verbose, false)]
	[InlineData(LogLevel.Debug, false)]
	[InlineData(LogLevel.Information, true)]
	[InlineData(LogLevel.Warning, true)]
	[InlineData(LogLevel.Error, true)]
	[InlineData(LogLevel.Fatal, true)]
	public void Wrap_Level__Compares_With_Minimum(LogLevel level, bool expected)
	{
		// Arrange
		var log = GetLogger();

		// Act
		var result = log.IsEnabled(level);

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData(MS.LogLevel.Trace, false)]
	[InlineData(MS.LogLevel.Debug, false)]
	[InlineData(MS.LogLevel.Information, true)]
	[InlineData(MS.LogLevel.Warning, true)]
	[InlineData(MS.LogLevel.Error, true)]
	[InlineData(MS.LogLevel.Critical, true)]
	[InlineData(MS.LogLevel.None, false)]
	public void Microsoft_Level__Compares_With_Minimum(MS.LogLevel level, bool expected)
	{
		// Arrange
		var log = GetLogger();

		// Act
		var result = log.IsEnabled(level);

		// Assert
		Assert.Equal(expected, result);
	}
}
