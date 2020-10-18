using System;
using System.Collections.Generic;
using System.Text;
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
			var type = F.Rnd.Str;

			// Act
			void action() => config.GetServiceConfig($"{type}.{F.Rnd.Str}");

			// Assert
			var ex = Assert.Throws<UnsupportedServiceException>(action);
			Assert.Equal(string.Format(UnsupportedServiceException.Format, type), ex.Message);
		}

		[Fact]
		public void Splits_Definition_And_Returns_ServiceConfig()
		{
			// Arrange
			var config = new ServicesConfig();
			var name = F.Rnd.Str;
			var service = new SeqConfig
			{
				Server = "https://www.contoso.com",
				ApiKey = F.Rnd.Str
			};

			config.Seq.Add(name, service);

			// Act
			var result = config.GetServiceConfig($"seq.{name}");

			// Assert
			Assert.Equal(service, result);
		}
	}
}
