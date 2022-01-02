// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Enumerated_Tests;

public partial class Operator_Tests
{
	[Fact]
	public void Equals_When_Equal_Returns_True()
	{
		// Arrange
		var value = F.Rnd.Str;
		var foo = new Foo(value);

		// Act
		var result = foo == value;

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void Equals_When_Not_Equal_Returns_False()
	{
		// Arrange
		var value = F.Rnd.Str;
		var foo = new Foo(value);
		var bar = new Bar(value);

		// Act
		var r0 = foo == string.Empty;
		var r1 = foo == bar;

		// Assert
		Assert.False(r0);
		Assert.False(r1);
	}

	public record class Bar : Enumerated
	{
		public Bar(string name) : base(name) { }
	}
}
