// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Testing.Exceptions;
using NSubstitute.Core;

namespace Jeebs.Data.Testing.Query.FluentQueryHelper_Tests;

public class AssertWhereIn_Tests : Setup
{
	[Fact]
	public void Asserts_WhereIn()
	{
		// Arrange
		var fluent = Create();
		var values = new[] { Rnd.Str, Rnd.Str };
		fluent.WhereIn(x => x.Foo, values);

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertWhereIn<TestEntity, string>(c, x => x.Foo, values);

		// Assert
		fluent.AssertCalls(action);
	}

	[Fact]
	public void Incorrect_Method__Throws_MethodNameException()
	{
		// Arrange
		var fluent = Create();
		fluent.Maximum(Rnd.ULng);
		var action = (ICall c) => FluentQueryHelper.AssertWhereIn<TestEntity, long>(c, x => x.Bar, [Rnd.Lng, Rnd.Lng]);

		// Act
		var result = Record.Exception(() => fluent.AssertCalls(action));

		// Assert
		Assert.IsType<MethodNameException>(result);
	}

	[Fact]
	public void Incorrect_Generic_Argument__Throws_GenericArgumentException()
	{
		// Arrange
		var fluent = Create();
		var values = new[] { Rnd.Str, Rnd.Str };
		fluent.WhereIn(x => x.Foo, values);
		var action = (ICall c) => FluentQueryHelper.AssertWhereIn<TestEntity, long>(c, x => x.Bar, [Rnd.Lng, Rnd.Lng]);

		// Act
		var result = Record.Exception(() => fluent.AssertCalls(action));

		// Assert
		Assert.IsType<GenericArgumentException>(result);
	}

	[Fact]
	public void Values_Not_Equal__Throws_EqualTypeException()
	{
		// Arrange
		var fluent = Create();
		var values = new[] { Rnd.Str, Rnd.Str };
		fluent.WhereIn(x => x.Foo, values);
		var action = (ICall c) => FluentQueryHelper.AssertWhereIn<TestEntity, string>(c, x => x.Foo, [Rnd.Str, Rnd.Str]);

		// Act
		var result = Record.Exception(() => fluent.AssertCalls(action));

		// Assert
		Assert.IsType<EqualTypeException>(result);
	}
}
