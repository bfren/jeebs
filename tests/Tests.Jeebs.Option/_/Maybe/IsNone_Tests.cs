// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;
using static F.MaybeF;

namespace Jeebs.Maybe_Tests;

public class IsNone_Tests
{
	[Fact]
	public void Is_Some_Returns_False()
	{
		// Arrange
		var some = Some(F.Rnd.Str);

		// Act
		var result = some.IsNone;

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Is_None_Returns_True()
	{
		// Arrange
		var none = Create.None<string>();

		// Act
		var result = none.IsNone;

		// Assert
		Assert.True(result);
	}
}
