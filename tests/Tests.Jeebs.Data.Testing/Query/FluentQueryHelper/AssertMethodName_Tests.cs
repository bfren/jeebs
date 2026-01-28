// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;
using Jeebs.Data.Testing.Exceptions;
using NSubstitute.Core;

namespace Jeebs.Data.Testing.Query.FluentQueryHelper_Tests;

public class AssertMethodName_Tests
{
	[Fact]
	public void Calls_GetMethodInfo__Asserts_Equal()
	{
		// Arrange
		var value = Rnd.Str;
		var method = Substitute.ForPartsOf<MethodInfo>();
		method.Name
			.Returns(value);
		var call = Substitute.For<ICall>();
		call.GetMethodInfo()
			.Returns(method);

		// Act
		var action = () => FluentQueryHelper.AssertMethodName(call, value);

		// Assert
		action();
		call.Received().GetMethodInfo();
	}

	[Fact]
	public void Not_Equal__Throws_MethodNameException()
	{
		// Arrange
		var method = Substitute.ForPartsOf<MethodInfo>();
		method.Name
			.Returns(Rnd.Str);
		var call = Substitute.For<ICall>();
		call.GetMethodInfo()
			.Returns(method);

		// Act
		var result = Record.Exception(() => FluentQueryHelper.AssertMethodName(call, Rnd.Str));

		// Assert
		Assert.IsType<MethodNameException>(result);
	}
}
