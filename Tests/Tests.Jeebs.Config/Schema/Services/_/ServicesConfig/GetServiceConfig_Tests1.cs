// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jx.Config;
using Xunit;

namespace Jeebs.Config.ServicesConfig_Tests
{
	public partial class GetServiceConfig_Tests
	{
		[Fact]
		public void Splits_Definition_Unknown_Service_Type_Throws_UnsupportedServiceException()
		{
			// Arrange
			var config = new ServicesConfig();
			var type = JeebsF.Rnd.Str;

			// Act
			void action() => config.GetServiceConfig($"{type}.{JeebsF.Rnd.Str}");

			// Assert
			var ex = Assert.Throws<UnsupportedServiceException>(action);
			Assert.Equal(string.Format(UnsupportedServiceException.Format, type), ex.Message);
		}

		[Fact]
		public void Splits_Definition_And_Returns_ServiceConfig()
		{
			// Arrange
			var config = new ServicesConfig();
			var name = JeebsF.Rnd.Str;
			var service = new SeqConfig
			{
				Server = "https://www.contoso.com",
				ApiKey = JeebsF.Rnd.Str
			};

			config.Seq.Add(name, service);

			// Act
			var result = config.GetServiceConfig($"seq.{name}");

			// Assert
			Assert.Equal(service, result);
		}
	}
}
