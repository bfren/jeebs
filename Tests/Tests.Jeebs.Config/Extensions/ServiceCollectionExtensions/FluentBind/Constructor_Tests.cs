// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace Jeebs.Config.ServiceCollectionExtensions_Tests.FluentBind_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_Properties()
		{
			// Arrange
			var services = Substitute.For<IServiceCollection>();

			// Act
			var result = new ServiceCollectionExtensions.FluentBind<Foo>(services);

			// Assert
			Assert.Equal(services, result.Services);
		}
	}
}
