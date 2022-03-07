// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;
using static F.MaybeF;

namespace Jeebs.MaybeExtensions_Tests;

public class UnsafeUnwrap_Tests
{
	[Fact]
	public void Some_Returns_Value()
	{
		// Arrange
		var value = F.Rnd.Int;
		var maybe = Some(value);

		// Act
		var result = maybe.UnsafeUnwrap();

		// Assert
		Assert.Equal(value, result);
	}

	[Fact]
	public void None_Throws_UnsafeUnwrapException()
	{
		// Arrange
		var maybe = Create.None<int>();

		// Act
		var action = void () => maybe.UnsafeUnwrap();

		// Assert
		_ = Assert.Throws<UnsafeUnwrapException>(action);
	}
}
