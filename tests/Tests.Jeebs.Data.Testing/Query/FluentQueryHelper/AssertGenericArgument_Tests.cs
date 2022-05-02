// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;
using Jeebs.Data.Testing.Exceptions;
using NSubstitute.Core;
using NSubstitute.Extensions;

namespace Jeebs.Data.Testing.Query.FluentQueryHelper_Tests;

public class AssertGenericArgument_Tests
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
	public void Incorrect_Type__Throws_GenericArgumentException()
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
		var ex = Assert.Throws<GenericArgumentException>(action);
		Assert.Contains($"Expected type '{typeof(Guid)}' but found '{typeof(string)}'.", ex.Message);
	}

	[Fact]
	public void No_Generic_Arguments__Throws_GenericArgumentException()
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
		var ex = Assert.Throws<GenericArgumentException>(action);
		Assert.Equal("Expected one generic argument but found none.", ex.Message);
	}

	[Fact]
	public void Too_Many_Generic_Arguments__Throws_GenericArgumentException()
	{
		// Arrange
		var method = Substitute.ForPartsOf<MethodInfo>();
		var args = Rnd.NumberF.GetInt32(2, 10);
		method.Configure().GetGenericArguments()
			.Returns(Enumerable.Repeat(typeof(string), args).ToArray());
		var call = Substitute.For<ICall>();
		call.GetMethodInfo()
			.Returns(method);

		// Act
		var action = () => FluentQueryHelper.AssertGenericArgument<string>(call);

		// Assert
		var ex = Assert.Throws<GenericArgumentException>(action);
		Assert.Equal($"Expected one generic argument but found {args}.", ex.Message);
	}
}
