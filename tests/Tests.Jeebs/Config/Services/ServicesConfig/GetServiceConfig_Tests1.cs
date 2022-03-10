// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.Services.Seq;

namespace Jeebs.Config.Services.ServicesConfig_Tests;

public partial class GetServiceConfig_Tests
{
	[Fact]
	public void Splits_Definition_Unknown_Service_Type_Throws_UnsupportedServiceException()
	{
		// Arrange
		var config = new ServicesConfig();
		var type = Rnd.Str;

		// Act
		var action = void () => config.GetServiceConfig($"{type}.{Rnd.Str}");

		// Assert
		var ex = Assert.Throws<UnsupportedServiceException>(action);
		Assert.Equal(string.Format(UnsupportedServiceException.Format, type), ex.Message);
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
