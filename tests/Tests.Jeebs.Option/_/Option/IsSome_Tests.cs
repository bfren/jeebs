// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;
using static F.OptionF;

namespace Jeebs.Option_Tests;

public class IsSome_Tests
{
	[Fact]
	public void Is_Some_Returns_True()
	{
		// Arrange
		var some = Some(F.Rnd.Str);

		// Act
		var result = some.IsSome;

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void Is_None_Returns_False()
	{
		// Arrange
		var none = Create.None<string>();

		// Act
		var result = none.IsSome;

		// Assert
		Assert.False(result);
	}
}
