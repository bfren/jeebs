// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;
using NSubstitute.Core;
using NSubstitute.Extensions;
using Xunit.Sdk;

namespace Jeebs.Data.Testing.Query.FluentQueryHelper_Tests;

public class AssertAssertGenericArgument_Tests
{
	[Fact]
	public void Calls_GetMethodInfo__Asserts_Collection()
	{
		// Arrange
		var method = Substitute.ForPartsOf<MethodInfo>();
		method.Configure().GetGenericArguments()
			.Returns(new[] { typeof(string) });
		var call = Substitute.For<ICall>();
		call.GetMethodInfo()
			.Returns(method);

		// Act
		var action = () => FluentQueryHelper.AssertGenericArgument<string>(call);

		// Assert
		action();
		call.Received().GetMethodInfo();
	}

	[Fact]
	public void Incorrect_Type__Throws_CollectionException_With_EqualException()
	{
		// Arrange
		var method = Substitute.ForPartsOf<MethodInfo>();
		method.Configure().GetGenericArguments()
			.Returns(new[] { typeof(string) });
		var call = Substitute.For<ICall>();
		call.GetMethodInfo()
			.Returns(method);

		// Act
		var action = () => FluentQueryHelper.AssertGenericArgument<Guid>(call);

		// Assert
		var ex = Assert.Throws<CollectionException>(action);
		Assert.Contains("Assert.Equal() Failure", ex.Message);
	}

	[Fact]
	public void No_Generic_Arguments__Throws_CollectionException()
	{
		// Arrange
		var method = Substitute.ForPartsOf<MethodInfo>();
		method.Configure().GetGenericArguments()
			.Returns(Array.Empty<Type>());
		var call = Substitute.For<ICall>();
		call.GetMethodInfo()
			.Returns(method);

		// Act
		var action = () => FluentQueryHelper.AssertGenericArgument<string>(call);

		// Assert
		Assert.Throws<CollectionException>(action);
	}

	[Fact]
	public void Too_Many_Generic_Arguments__Throws_CollectionException()
	{
		// Arrange
		var method = Substitute.ForPartsOf<MethodInfo>();
		method.Configure().GetGenericArguments()
			.Returns(Enumerable.Repeat(typeof(string), Rnd.NumberF.GetInt32(2, 10)).ToArray());
		var call = Substitute.For<ICall>();
		call.GetMethodInfo()
			.Returns(method);

		// Act
		var action = () => FluentQueryHelper.AssertGenericArgument<string>(call);

		// Assert
		Assert.Throws<CollectionException>(action);
	}
}
