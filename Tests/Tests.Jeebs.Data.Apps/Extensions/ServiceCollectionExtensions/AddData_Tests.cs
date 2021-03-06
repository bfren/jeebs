using System.Collections.Generic;
using Jeebs.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Jeebs.Data.ServiceCollectionExtensions_Tests
{
	public class AddData_Tests
	{
		[Fact]
		public void Binds_Data_Configuration_Values_To_DbConfig_Using_Default_Key()
		{
			// Arrange		
			var v0 = F.Rnd.Str;
			var v1 = F.Rnd.Str;

			var services = new ServiceCollection();

			var builder = new ConfigurationBuilder();
			builder.AddInMemoryCollection(new Dictionary<string, string>
			{
				{ $"{DbConfig.Key}:{nameof(DbConfig.Default)}", v0 },
				{ $"{DbConfig.Key}:{nameof(DbConfig.Authentication)}", v1 }
			});

			var config = builder.Build();

			// Act
			services.AddData();
			var result = config.GetSection(DbConfig.Key).Get<DbConfig>();

			// Assert
			Assert.Equal(v0, result.Default);
			Assert.Equal(v1, result.Authentication);
		}
	}
}
