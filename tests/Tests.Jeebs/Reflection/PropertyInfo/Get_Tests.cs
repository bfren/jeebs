﻿// Jeebs Unit Tests
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
	public void WithPropertyNotSet_ThrowsInvalidOperationException()
	{
		// Arrange
		var foo = new Foo();
		var info = new PropertyInfo<Foo, string>(nameof(Foo.Bar));

		// Act
		var result = void () => info.Get(foo);

		// Assert
		Assert.Throws<InvalidOperationException>(result);
	}

	[Theory]
	[InlineData(null)]
	public void FromNullObject_ThrowsArgumentNullException(Foo obj)
	{
		// Arrange
		var info = new PropertyInfo<Foo, string>(nameof(Foo.Bar));

		// Act
		var result = void () => info.Get(obj);

		// Assert
		Assert.Throws<ArgumentNullException>(result);
	}

	public sealed class Foo
	{
		public string? Bar { get; set; }
	}
}
