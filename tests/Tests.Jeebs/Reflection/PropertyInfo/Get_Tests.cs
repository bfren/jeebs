// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Reflection.PropertyInfo_Tests;

public class Get_Tests
{
	[Fact]
	public void WithPropertySet_ReturnsValue()
	{
		// Arrange
		var foo = new Foo { Bar = Rnd.Str };
		var info = new PropertyInfo<Foo, string>(nameof(Foo.Bar));

		// Act
		var result = info.Get(foo);

		// Assert
		Assert.Equal(foo.Bar, result);
	}

	[Fact]
	public void WithPropertyNotSet_ReturnsNone()
	{
		// Arrange
		var foo = new Foo();
		var info = new PropertyInfo<Foo, string>(nameof(Foo.Bar));

		// Act
		var result = info.Get(foo);

		// Assert
		result.AssertNone();
	}

	[Fact]
	public void FromNullObject_ReturnsNone()
	{
		// Arrange
		var info = new PropertyInfo<Foo, string>(nameof(Foo.Bar));

		// Act
		var result = info.Get(null!);

		// Assert
		result.AssertNone();
	}

	public sealed class Foo
	{
		public string? Bar { get; set; }
	}
}
