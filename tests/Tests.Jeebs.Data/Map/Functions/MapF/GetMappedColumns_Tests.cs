// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Map.Functions.MapF_Tests;

public class GetColumns_Tests
{
	[Fact]
	public void Ignores_Properties_With_Ignore_Attribute()
	{
		// Arrange

		// Act
		var result = MapF.GetColumns<FooWithIgnored>(new FooTable());

		// Assert
		var some = result.AssertSome();
		Assert.DoesNotContain(some, x => x.ColAlias == nameof(FooWithIgnored.Id));
		Assert.DoesNotContain(some, x => x.ColAlias == nameof(FooWithIgnored.Bar0));
	}

	[Fact]
	public void Returns_Some_With_Column_List()
	{
		// Arrange

		// Act
		var result = MapF.GetColumns<FooWithIgnored>(new FooTable());

		// Assert
		var some = result.AssertSome();
		Assert.Collection(some,
			x => Assert.Equal(nameof(FooWithIgnored.FooId), x.ColAlias),
			x => Assert.Equal(nameof(FooWithIgnored.Bar1), x.ColAlias)
		);
	}
}
