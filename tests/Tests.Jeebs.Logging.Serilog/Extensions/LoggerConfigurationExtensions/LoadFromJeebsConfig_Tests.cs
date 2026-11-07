// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config;
using Serilog;
using Serilog.Events;
using Wrap.Logging;

namespace Jeebs.Logging.Serilog.LoggerConfigurationExtensions_Tests;

public class LoadFromJeebsConfig_Tests
{
	private static ILogger GetLogger(string source, Dictionary<string, LogLevel> overrides)
	{
		var jeebs = new JeebsConfig { Logging = new() { Minimum = LogLevel.Verbose, Overrides = overrides } };
		var serilog = new LoggerConfiguration();
		serilog.LoadFromJeebsConfig(jeebs);
		return serilog.CreateLogger().ForContext("SourceContext", source);
	}

	[Theory]
	[InlineData("MySqlConnector.ConnectionPool")]
	[InlineData("System.Data.SQLite")]
	public void No_Override__Database_Driver_Logs_Warnings_Only(string source)
	{
		// Arrange

		// Act
		var logger = GetLogger(source, []);

		// Assert
		Assert.False(logger.IsEnabled(LogEventLevel.Information));
		Assert.True(logger.IsEnabled(LogEventLevel.Warning));
	}

	[Theory]
	[InlineData("MySqlConnector", "MySqlConnector.ConnectionPool")]
	[InlineData("System.Data.SQLite", "System.Data.SQLite")]
	public void Override__Database_Driver_Uses_Configured_Level(string key, string source)
	{
		// Arrange
		var overrides = new Dictionary<string, LogLevel> { [key] = LogLevel.Debug };

		// Act
		var logger = GetLogger(source, overrides);

		// Assert
		Assert.True(logger.IsEnabled(LogEventLevel.Debug));
		Assert.False(logger.IsEnabled(LogEventLevel.Verbose));
	}
}
