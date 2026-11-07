// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config;
using Jeebs.Config.Logging;
using Serilog;
using Serilog.Events;
using Wrap.Logging;

namespace Jeebs.Logging.Serilog.LoggerConfigurationExtensions_Tests;

public class LoadFromJeebsConfig_Tests
{
	private static ILogger GetMySqlConnectorLogger(Dictionary<string, LogLevel> overrides)
	{
		var jeebs = new JeebsConfig { Logging = new() { Minimum = LogLevel.Verbose, Overrides = overrides } };
		var serilog = new LoggerConfiguration();
		serilog.LoadFromJeebsConfig(jeebs);
		return serilog.CreateLogger().ForContext("SourceContext", "MySqlConnector.ConnectionPool");
	}

	[Fact]
	public void No_Override__MySqlConnector_Logs_Warnings_Only()
	{
		// Arrange

		// Act
		var logger = GetMySqlConnectorLogger([]);

		// Assert
		Assert.False(logger.IsEnabled(LogEventLevel.Information));
		Assert.True(logger.IsEnabled(LogEventLevel.Warning));
	}

	[Fact]
	public void Override__MySqlConnector_Uses_Configured_Level()
	{
		// Arrange
		var overrides = new Dictionary<string, LogLevel> { ["MySqlConnector"] = LogLevel.Debug };

		// Act
		var logger = GetMySqlConnectorLogger(overrides);

		// Assert
		Assert.True(logger.IsEnabled(LogEventLevel.Debug));
		Assert.False(logger.IsEnabled(LogEventLevel.Verbose));
	}
}
