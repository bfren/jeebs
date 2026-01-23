// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.Services.Seq;

namespace Jeebs.Config.Services.ServicesConfig_Tests;

public partial class GetServiceConfig_Tests
{
	[Fact]
	public void Service_Does_Not_Exist_Returns_Default_Config()
	{
		// Arrange
		var config = new ServicesConfig();
		var name = Rnd.Str;

		// Act
		var result = config.GetServiceConfig(x => x.Slack, name);

		// Assert
		result.AssertOk(new());
	}

	[Fact]
	public void Invalid_Config_Returns_Fail()
	{
		// Arrange
		var config = new ServicesConfig();
		var name = Rnd.Str;
		config.Seq.Add(name, new SeqConfig());

		// Act
		var result = config.GetServiceConfig(x => x.Seq, name);

		// Assert
		result.AssertFail(
			"Definition of {Type} service named '{Name}' is invalid.",
			nameof(SeqConfig), name
		);
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
