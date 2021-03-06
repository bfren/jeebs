using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.ServiceCollectionExtensions_Tests.FluentData_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_Properties()
		{
			// Arrange
			var services = Substitute.For<IServiceCollection>();
			var section = F.Rnd.Str;

			// Act
			var result = new ServiceCollectionExtensions.FluentData(services, section);

			// Assert
			Assert.Equal(services, result.Services);
			Assert.Equal(section, result.Section);
		}
	}
}
