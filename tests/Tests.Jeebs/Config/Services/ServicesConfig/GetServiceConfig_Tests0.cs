// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Globalization;
using Jeebs.Config.Services.Console;
using Jeebs.Config.Services.Seq;

namespace Jeebs.Config.Services.ServicesConfig_Tests;

public partial class GetServiceConfig_Tests
{
	[Fact]
	public void Service_Does_Not_Exist_Returns_Default_Instance()
	{
		// Arrange
		var config = new ServicesConfig();
		var name = Rnd.Str;
		var defaultConfig = new ConsoleConfig();

		// Act
		var result = config.GetServiceConfig(x => x.Console, name);

		// Assert
		Assert.Equal(defaultConfig.AddPrefix, result.AddPrefix);
		Assert.Equal(defaultConfig.Template, result.Template);
	}

	[Fact]
	public void Invalid_Config_Throws_InvalidServiceConfigurationException()
	{
		// Arrange
		var config = new ServicesConfig();
		var name = Rnd.Str;
		config.Seq.Add(name, new SeqConfig());

		// Act
		var action = void () => config.GetServiceConfig(x => x.Seq, name);

		// Assert
		var ex = Assert.Throws<InvalidServiceConfigurationException>(action);
		Assert.Equal(string.Format(CultureInfo.InvariantCulture, InvalidServiceConfigurationException.Format, name, typeof(SeqConfig)), ex.Message);
	}

	[Fact]
	public void Returns_ServiceConfig()
	{
		// Arrange
		var config = new ServicesConfig();
		var name = Rnd.Str;
		var service = new SeqConfig
		{
			Server = "https://www.contoso.com",
			ApiKey = Rnd.Str
		};

		config.Seq.Add(name, service);

		// Act
		var result = config.GetServiceConfig(x => x.Seq, name);

		// Assert
		Assert.Equal(service, result);
	}
}
