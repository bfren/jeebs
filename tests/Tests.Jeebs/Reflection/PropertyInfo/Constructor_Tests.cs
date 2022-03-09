// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Reflection.PropertyInfo_Tests;

public class Constructor_Tests
{
	[Fact]
	public void WithInvalidPropertyName_ThrowsInvalidOperationException()
	{
		// Arrange

		// Act
		var result = PropertyInfo<Foo, object> () => new(Rnd.Str);

		// Assert
		_ = Assert.Throws<InvalidOperationException>(result);
	}

	[Fact]
	public void WithInvalidPropertyType_ThrowsInvalidOperationException()
	{
		// Arrange

		// Act
		var result = PropertyInfo<Foo, int> () => new(nameof(Foo.Bar));

		// Assert
		_ = Assert.Throws<InvalidOperationException>(result);
	}

	public sealed class Foo
	{
		public string Bar { get; set; } = string.Empty;
	}
}
