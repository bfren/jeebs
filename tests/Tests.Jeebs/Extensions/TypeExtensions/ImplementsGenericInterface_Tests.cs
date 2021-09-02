// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.TypeExtensions_Tests;

public class ImplementsGenericInterface_Tests
{
	[Theory]
	[InlineData(typeof(Foo), typeof(IDoesImplement<>))]
	[InlineData(typeof(Foo), typeof(IDoesImplement<string>))]
	[InlineData(typeof(Foo<>), typeof(IDoesImplement<>))]
	[InlineData(typeof(Foo<string>), typeof(IDoesImplement<>))]
	[InlineData(typeof(Foo<string>), typeof(IDoesImplement<string>))]
	public void Does_Implement_Returns_True(Type @base, Type @interface)
	{
		// Arrange

		// Act
		var result = @base.ImplementsGenericInterface(@interface);

		// Assert
		Assert.True(result);
	}

	[Theory]
	[InlineData(typeof(Foo), typeof(IDoesNotImplement<>))]
	[InlineData(typeof(Foo<>), typeof(IDoesNotImplement<>))]
	[InlineData(typeof(Foo<string>), typeof(IDoesImplement<int>))]
	public void Does_Not_Implement_Returns_False(Type @base, Type @interface)
	{
		// Arrange

		// Act
		var result = @base.ImplementsGenericInterface(@interface);

		// Assert
		Assert.False(result);
	}

	public interface IDoesImplement<T> { }
	public interface IDoesNotImplement<T> { }
	public class Foo : IDoesImplement<string> { }
	public class Foo<T> : IDoesImplement<T> { }
}
