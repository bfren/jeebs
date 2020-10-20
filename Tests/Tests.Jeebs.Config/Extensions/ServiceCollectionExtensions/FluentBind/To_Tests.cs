using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace Jeebs.Config.ServiceCollectionExtensions_Tests.FluentBind_Tests
{
	public class To_Tests
	{
		[Fact]
		public void Sets_SectionKey()
		{
			// Arrange
			var services = Substitute.For<IServiceCollection>();
			var fluent = new ServiceCollectionExtensions.FluentBind<Foo>(services);
			var key = F.Rnd.Str;

			// Act
			var result = fluent.To(key);

			// Assert
			Assert.Equal(result.SectionKey, key);
		}

		[Fact]
		public void Sets_JeebsSectionKey()
		{
			// Arrange
			var services = Substitute.For<IServiceCollection>();
			var fluent = new ServiceCollectionExtensions.FluentBind<Foo>(services);
			var key = $":{F.Rnd.Str}";

			// Act
			var result = fluent.To(key);

			// Assert
			Assert.Equal(result.SectionKey, JeebsConfig.GetKey(key));
		}

		[Fact]
		public void Binds_When_Using_Previously_Called()
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
			bind.Using(config).To(key);
			var result = config.GetSection(key).Get<Foo>();

			// Assert
			Assert.Equal(b0, result.Bar0);
			Assert.Equal(b1, result.Bar1);
		}
	}
}
