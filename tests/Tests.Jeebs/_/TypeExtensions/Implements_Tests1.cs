// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;

namespace Jeebs.TypeExtensions_Tests;

public partial class Implements_Tests
{
	[Fact]
	public void Base_Is_Object_Type_Returns_False()
	{
		// Arrange
		var @base = typeof(object);
		var @class = Substitute.For<Type>();

		// Act
		var result = @base.Implements(@class);

		// Assert
		Assert.False(result);
	}

	[Theory]
	[InlineData(typeof(bool))]
	[InlineData(typeof(int))]
	[InlineData(typeof(long))]
	[InlineData(typeof(float))]
	[InlineData(typeof(double))]
	[InlineData(typeof(char))]
	[InlineData(typeof(LogLevel))]
	public void Base_Is_Value_Type_Returns_False(Type @base)
	{
		// Arrange
		var @class = Substitute.For<Type>();

		// Act
		var result = @base.Implements(@class);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Base_Is_SubclassOf_Type_Returns_True()
	{
		// Arrange
		var @base = typeof(Foo);
		var @class = typeof(DoesImplement);

		// Act
		var result = @base.Implements(@class);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void Interface_Is_AssignableFrom_Base_Returns_True()
	{
		// Arrange
		var @base = typeof(Foo);
		var @class = typeof(IDoesImplement);

		// Act
		var result = @base.Implements(@class);

		// Assert
		Assert.True(result);
	}

	[Theory]
	[InlineData(typeof(Foo), typeof(IDoesImplement<>))]
	[InlineData(typeof(Foo), typeof(IDoesImplement<string>))]
	[InlineData(typeof(Foo<>), typeof(IDoesImplement<>))]
	[InlineData(typeof(Foo<string>), typeof(IDoesImplement<>))]
	[InlineData(typeof(Foo<string>), typeof(IDoesImplement<string>))]
	[InlineData(typeof(Foo), typeof(DoesImplement<>))]
	[InlineData(typeof(Foo), typeof(DoesImplement<string>))]
	[InlineData(typeof(Foo<>), typeof(DoesImplement<>))]
	[InlineData(typeof(Foo<string>), typeof(DoesImplement<string>))]
	public void Base_Does_Implement_Generic_Returns_True(Type @base, Type @class)
	{
		// Arrange

		// Act
		var result = @base.Implements(@class);

		// Assert
		Assert.True(result);
	}

	[Theory]
	[InlineData(typeof(Foo), typeof(IDoesNotImplement<>))]
	[InlineData(typeof(Foo<>), typeof(IDoesNotImplement<>))]
	[InlineData(typeof(Foo<string>), typeof(IDoesImplement<int>))]
	[InlineData(typeof(Foo), typeof(DoesNotImplement<>))]
	[InlineData(typeof(Foo<>), typeof(DoesNotImplement<>))]
	[InlineData(typeof(Foo<string>), typeof(DoesNotImplement<int>))]
	public void Base_Does_Not_Implement_Generic_Returns_False(Type @base, Type @class)
	{
		// Arrange

		// Act
		var result = @base.Implements(@class);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Base_Does_Not_Implement_Type_Returns_False()
	{
		// Arrange
		var @base = typeof(Foo);
		var @class = typeof(DoesNotImplement);

		// Act
		var result = @base.Implements(@class);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Interface_Is_Not_AssignableFrom_Base_Returns_False()
	{
		// Arrange
		var @base = typeof(Foo);
		var @class = typeof(IDoesNotImplement);

		// Act
		var result = @base.Implements(@class);

		// Assert
		Assert.False(result);
	}
}
