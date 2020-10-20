using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.ServiceCollectionExtensions_Tests.FluentData_Tests
{
	public class Using_Tests
	{
		[Fact]
		public void Binds_Data_Configuration_Values_To_DbConfig()
		{
			// Arrange		
			var key = F.Rnd.Str;
			var v0 = F.Rnd.Str;
			var v1 = F.Rnd.Str;

			var services = new ServiceCollection();
			var fluent = new ServiceCollectionExtensions.FluentData(services, key);

			var builder = new ConfigurationBuilder();
			builder.AddInMemoryCollection(new Dictionary<string, string>
			{
				{ $"{key}:{nameof(DbConfig.Default)}", v0 },
				{ $"{key}:{nameof(DbConfig.Authentication)}", v1 }
			});

			var config = builder.Build();

			// Act
			fluent.Using(config);
			var result = config.GetSection(key).Get<DbConfig>();

			// Assert
			Assert.Equal(v0, result.Default);
			Assert.Equal(v1, result.Authentication);
		}
	}
}
