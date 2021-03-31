// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Data.Mapping.Mapper_Tests
{
	public class GetMappedColumns_Tests
	{
		[Fact]
		public void Ignores_Properties_With_Ignore_Attribute()
		{
			// Arrange

			// Act
			var result = Mapper.GetMappedColumns<FooWithIgnored>(new FooTable());

			// Assert
			var some = result.AssertSome();
			Assert.DoesNotContain(some, x => x.Alias == nameof(FooWithIgnored.Id));
			Assert.DoesNotContain(some, x => x.Alias == nameof(FooWithIgnored.Bar0));
		}

		[Fact]
		public void Returns_Some_With_MappedColumn_List()
		{
			// Arrange

			// Act
			var result = Mapper.GetMappedColumns<FooWithIgnored>(new FooTable());

			// Assert
			var some = result.AssertSome();
			Assert.Collection(some,
				x => Assert.Equal(nameof(FooWithIgnored.FooId), x.Alias),
				x => Assert.Equal(nameof(FooWithIgnored.Bar1), x.Alias)
			);
		}
	}
}
