// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Functions.DataF_Tests;

public class GetColumns_Tests
{
	[Fact]
	public void Ignores_Properties_With_Ignore_Attribute()
	{
		// Arrange

		// Act
		var result = DataF.GetColumns<FooTableWithIgnored, Foo>(new());

		// Assert
		var ok = result.AssertOk();
		Assert.DoesNotContain(ok, x => x.ColAlias == nameof(FooTableWithIgnored.Id));
		Assert.DoesNotContain(ok, x => x.ColAlias == nameof(FooTableWithIgnored.Bar0));
	}

	[Fact]
	public void Returns_Some_With_Column_List()
	{
		// Arrange

		// Act
		var result = DataF.GetColumns<FooTableWithIgnored, Foo>(new());

		// Assert
		var ok = result.AssertOk();
		Assert.Collection(ok,
			x => Assert.Equal(nameof(FooTableWithIgnored.FooId), x.ColAlias),
			x => Assert.Equal(nameof(FooTableWithIgnored.Bar1), x.ColAlias)
		);
	}
}
