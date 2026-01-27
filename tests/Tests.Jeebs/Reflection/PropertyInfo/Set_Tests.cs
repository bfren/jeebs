// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Reflection.PropertyInfo_Tests;

public class Set_Tests
{
	[Fact]
	public void Set_NullReferenceType_SetsToNull()
	{
		// Arrange
		var foo = new Foo { Bar = Rnd.Str };
		var info = new PropertyInfo<Foo, string>(nameof(Foo.Bar));

		// Act
		info.Set(foo, null!);

		// Assert
		Assert.Null(foo.Bar);
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

		public int Baz { get; set; }
	}
}
