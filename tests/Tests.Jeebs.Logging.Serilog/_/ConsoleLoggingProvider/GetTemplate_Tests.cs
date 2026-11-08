// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.Services.Console;

namespace Jeebs.Logging.Serilog.ConsoleLoggingProvider_Tests;

public class GetTemplate_Tests
{
	[Fact]
	public void AddPrefix_With_Name__Adds_Name_Before_Message()
	{
		// Arrange
		var config = new ConsoleConfig { Template = "[{Level:u3}] {Message:lj}{NewLine}", AddPrefix = true };

		// Act
		var result = ConsoleLoggingProvider.GetTemplate(config, "My{App}");

		// Assert
		Assert.Equal("[{Level:u3}] My{{App}} | {Message:lj}{NewLine}", result);
	}

	[Theory]
	[InlineData(false, "app")]
	[InlineData(true, null)]
	[InlineData(true, " ")]
	public void No_Prefix_Or_No_Name__Returns_Template(bool addPrefix, string? name)
	{
		// Arrange
		var config = new ConsoleConfig { AddPrefix = addPrefix };

		// Act
		var result = ConsoleLoggingProvider.GetTemplate(config, name);

		// Assert
		Assert.Equal(config.Template, result);
	}
}
