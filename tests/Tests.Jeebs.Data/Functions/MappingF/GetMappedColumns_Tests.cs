// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;
using static F.DataF.MappingF;

namespace Jeebs.Data.Mapping.Mapper_Tests
{
	public class GetMappedColumns_Tests
	{
		[Fact]
		public void Ignores_Properties_With_Ignore_Attribute()
		{
			// Arrange

			// Act
			var result = GetMappedColumns<FooWithIgnored>(new FooTable());

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
			var result = GetMappedColumns<FooWithIgnored>(new FooTable());

			// Assert
			var some = result.AssertSome();
			Assert.Collection(some,
				x => Assert.Equal(nameof(FooWithIgnored.FooId), x.Alias),
				x => Assert.Equal(nameof(FooWithIgnored.Bar1), x.Alias)
			);
		}
	}
}
