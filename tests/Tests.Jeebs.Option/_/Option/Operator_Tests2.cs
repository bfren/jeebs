// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;
using static F.OptionF;

namespace Jeebs.Option_Tests;

public partial class Operator_Tests
{
	[Fact]
	public void DoesNotEqual_When_Equal_Returns_False()
	{
		// Arrange
		var value = F.Rnd.Int;
		var some = Some(value);

		// Act
		var r0 = some != value;
		var r1 = value != some;

		// Assert
		Assert.False(r0);
		Assert.False(r1);
	}

	[Fact]
	public void DoesNotEqual_When_Not_Equal_Returns_True()
	{
		// Arrange
		var v0 = F.Rnd.Int;
		var v1 = F.Rnd.Int;
		var some = Some(v0);

		// Act
		var r0 = some != v1;
		var r1 = v1 != some;

		// Assert
		Assert.True(r0);
		Assert.True(r1);
	}
}
