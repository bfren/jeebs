// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Reflection.ObjectExtensions_Tests;

public class GetProperties_Tests
{
	[Fact]
	public void Is_Type_Gets_Properties()
	{
		// Arrange
		var type = typeof(Test);

		// Act
		var result = ObjectExtensions.GetProperties(type);

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(nameof(Test.Foo), x.Name),
			x => Assert.Equal(nameof(Test.Bar), x.Name)
		);
	}

	[Fact]
	public void Is_Not_Type_Gets_Properties()
	{
		// Arrange
		var test = new Test(F.Rnd.Str, F.Rnd.Int);

		// Act
		var result = test.GetProperties();

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(nameof(Test.Foo), x.Name),
			x => Assert.Equal(nameof(Test.Bar), x.Name)
		);
	}

	public sealed record class Test(string Foo, int Bar);
}
