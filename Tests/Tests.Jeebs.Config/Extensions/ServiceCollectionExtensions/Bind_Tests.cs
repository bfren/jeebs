using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace Jeebs.Config.ServiceCollectionExtensions_Tests
{
	public class Bind_Tests
	{
		[Fact]
		public void Returns_FluentBind_With_Services()
		{
			// Arrange
			var services = Substitute.For<IServiceCollection>();

			// Act
			var result = services.Bind<Foo>();

			// Assert
			var bind = Assert.IsType<ServiceCollectionExtensions.FluentBind<Foo>>(result);
			Assert.Equal(services, bind.Services);
		}
	}
}
