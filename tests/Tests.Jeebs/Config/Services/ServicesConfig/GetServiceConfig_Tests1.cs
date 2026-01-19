// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.Services.Seq;
using Jeebs.Config.Services.Slack;

namespace Jeebs.Config.Services.ServicesConfig_Tests;

public partial class GetServiceConfig_Tests
{
	[Fact]
	public void Splits_Definition_Unknown_Service_Returns_Fail()
	{
		// Arrange
		var config = new ServicesConfig();
		var name = Rnd.Str;

		// Act
		var result = config.GetServiceConfig(c => c.Slack, name);

		// Assert
		result.AssertFail("No {Type} service named '{Name}' is configured.", nameof(SlackConfig), name);
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
		var result = config.GetServiceConfig(c => c.Seq, name);

		// Assert
		Assert.Equal(service, result);
	}
}
