// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.Services.Seq;
using Jeebs.Config.Services.Slack;

namespace Jeebs.Config.Services.ServicesConfig_Tests;

public partial class GetServiceConfig_Tests
{
	[Fact]
	public void Splits_Definition_Unknown_Service_Type_Returns_Fail()
	{
		// Arrange
		var config = new ServicesConfig();
		var type = Rnd.Str;

		// Act
		var result = config.GetServiceConfig($"{type}.{Rnd.Str}");

		// Assert
		_ = result.AssertFail("Service type '{Type}' is not recognised.", new { type });
	}

	[Fact]
	public void Splits_Definition_Unknown_Service_Name_Returns_Default_Config()
	{
		// Arrange
		var config = new ServicesConfig();
		var name = Rnd.Str;

		// Act
		var result = config.GetServiceConfig($"slack.{name}");

		// Assert
		result.AssertOk(new SlackConfig());
	}

	[Fact]
	public void Splits_Definition_And_Returns_ServiceConfig()
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
		var result = config.GetServiceConfig($"seq.{name}");

		// Assert
		Assert.Equal(service, result);
	}
}
