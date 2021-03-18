// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace Jeebs.Config.ServiceCollectionExtensions_Tests.FluentBind_Tests
{
	public class Using_Tests
	{
		[Fact]
		public void Sets_Config()
		{
			// Arrange
			var services = Substitute.For<IServiceCollection>();
			var fluent = new ServiceCollectionExtensions.FluentBind<Foo>(services);
			var config = Substitute.For<IConfiguration>();

			// Act
			var result = fluent.Using(config);

			// Assert
			Assert.Equal(result.Config, config);
		}

		[Fact]
		public void Binds_When_To_Previously_Called()
		{
			// Arrange
			var services = Substitute.For<IServiceCollection>();
			var bind = new ServiceCollectionExtensions.FluentBind<Foo>(services);
			var key = F.Rnd.Str;

			var b0 = F.Rnd.Str;
			var b1 = F.Rnd.Int;
			var builder = new ConfigurationBuilder();
			builder.AddInMemoryCollection(new Dictionary<string, string>
			{
				{ $"{key}:{nameof(Foo.Bar0)}", b0 },
				{ $"{key}:{nameof(Foo.Bar1)}", b1.ToString() }
			});
			var config = builder.Build();

			// Act
			bind.To(key).Using(config);
			var result = config.GetSection(key).Get<Foo>();

			// Assert
			Assert.Equal(b0, result.Bar0);
			Assert.Equal(b1, result.Bar1);
		}
	}
}
