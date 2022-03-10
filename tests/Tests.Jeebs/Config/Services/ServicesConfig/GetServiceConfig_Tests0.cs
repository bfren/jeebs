﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.Services.Seq;

namespace Jeebs.Config.Services.ServicesConfig_Tests;

public partial class GetServiceConfig_Tests
{
	[Fact]
	public void Service_Does_Not_Exist_Throws_UnknownServiceException()
	{
		// Arrange
		var config = new ServicesConfig();
		var name = Rnd.Str;

		// Act
		var action = void () => config.GetServiceConfig(x => x.Seq, name);

		// Assert
		var ex = Assert.Throws<UnknownServiceException>(action);
		Assert.Equal(string.Format(UnknownServiceException.Format, name, typeof(SeqConfig)), ex.Message);
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
		Assert.Equal(string.Format(InvalidServiceConfigurationException.Format, name, typeof(SeqConfig)), ex.Message);
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