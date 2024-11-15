// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Testing.Exceptions;
using NSubstitute.Core;

namespace Jeebs.Data.Testing.Query.FluentQueryHelper_Tests;

public class AssertWhereNotIn_Tests : Setup
{
	[Fact]
	public void Asserts_WhereNotIn()
	{
		// Arrange
		var fluent = Create();
		var values = new[] { Rnd.Str, Rnd.Str };
		fluent.WhereNotIn(x => x.Foo, values);

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertWhereNotIn<TestEntity, string>(c, x => x.Foo, values);

		// Assert
		fluent.AssertCalls(action);
	}

	[Fact]
	public void Incorrect_Method__Throws_MethodNameException()
	{
		// Arrange
		var fluent = Create();
		fluent.Maximum(Rnd.ULng);

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertWhereNotIn<TestEntity, long>(c, x => x.Bar, [Rnd.Lng, Rnd.Lng]);

		// Assert
		Assert.Throws<MethodNameException>(() => fluent.AssertCalls(action));
	}

	[Fact]
	public void Incorrect_Generic_Argument__Throws_GenericArgumentException()
	{
		// Arrange
		var fluent = Create();
		var values = new[] { Rnd.Str, Rnd.Str };
		fluent.WhereNotIn(x => x.Foo, values);

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertWhereNotIn<TestEntity, long>(c, x => x.Bar, [Rnd.Lng, Rnd.Lng]);

		// Assert
		Assert.Throws<GenericArgumentException>(() => fluent.AssertCalls(action));
	}

	[Fact]
	public void Values_Not_Equal__Throws_EqualTypeException()
	{
		// Arrange
		var fluent = Create();
		var values = new[] { Rnd.Str, Rnd.Str };
		fluent.WhereNotIn(x => x.Foo, values);

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertWhereNotIn<TestEntity, string>(c, x => x.Foo, [Rnd.Str, Rnd.Str]);

		// Assert
		Assert.Throws<EqualTypeException>(() => fluent.AssertCalls(action));
	}
}
