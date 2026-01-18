// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Reflection.PropertyInfo_Tests;

public class Set_Tests
{
	[Theory]
	[InlineData(null)]
	public void Set_WithNullObject_ThrowsArgumentNullException(Foo? obj)
	{
		// Arrange
		var info = new PropertyInfo<Foo, string>(nameof(Foo.Bar));

		// Act
		var result = void () => info.Set(obj!, Rnd.Str);

		// Assert
		Assert.Throws<ArgumentNullException>(result);
	}

	[Theory]
	[InlineData(null)]
	public void Set_WithNullValue_ThrowsArgumentNullException(string? value)
	{
		// Arrange
		var foo = new Foo();
		var info = new PropertyInfo<Foo, string>(nameof(Foo.Bar));

		// Act
		var result = void () => info.Set(foo, value!);

		// Assert
		Assert.Throws<ArgumentNullException>(result);
	}

	[Fact]
	public void Set_WithEverythingValid_ChangesValue()
	{
		// Arrange
		var foo = new Foo { Bar = Rnd.Str };
		var newValue = Rnd.Str;
		var info = new PropertyInfo<Foo, string>(nameof(Foo.Bar));

		// Act
		info.Set(foo, newValue);

		// Assert
		Assert.Equal(newValue, foo.Bar);
	}

	public sealed class Foo
	{
		public string Bar { get; set; } = string.Empty;
	}
}
