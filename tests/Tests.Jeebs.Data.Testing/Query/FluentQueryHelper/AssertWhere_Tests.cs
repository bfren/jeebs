// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Testing.Exceptions;
using NSubstitute.Core;

namespace Jeebs.Data.Testing.Query.FluentQueryHelper_Tests;

public class AssertWhere_Tests : Setup
{
	[Fact]
	public void With_Property_Expression__Asserts_Where()
	{
		// Arrange
		var fluent = Create();
		var compare = Compare.Like;
		var value = Rnd.Str;
		fluent.Where(x => x.Foo, compare, value);

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertWhere<TestEntity, string>(c, x => x.Foo, compare, value);

		// Assert
		fluent.AssertCalls(action);
	}

	[Fact]
	public void With_Clause__Asserts_Where()
	{
		// Arrange
		var fluent = Create();
		var clause = Rnd.Str;
		var parameters = new { value = Rnd.Lng };
		fluent.Where(clause, parameters);

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertWhere(c, clause, parameters);

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
		var a0 = (ICall c) => FluentQueryHelper.AssertWhere<TestEntity, string>(c, x => x.Foo, Compare.Equal, Rnd.Str);
		var a1 = (ICall c) => FluentQueryHelper.AssertWhere(c, Rnd.Str, Rnd.Str);

		// Assert
		Assert.Throws<MethodNameException>(() => fluent.AssertCalls(a0));
		Assert.Throws<MethodNameException>(() => fluent.AssertCalls(a1));
	}

	[Fact]
	public void Incorrect_Generic_Argument__Throws_GenericArgumentException()
	{
		// Arrange
		var fluent = Create();
		var compare = Compare.Like;
		var value = Rnd.Str;
		fluent.Where(x => x.Foo, compare, value);

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertWhere<TestEntity, long>(c, nameof(TestEntity.Foo), compare, Rnd.Lng);

		// Assert
		Assert.Throws<GenericArgumentException>(() => fluent.AssertCalls(action));
	}

	[Fact]
	public void No_Generic_Argument__Throws_GenericArgumentException()
	{
		// Arrange
		var fluent = Create();
		var compare = Compare.Like;
		var value = Rnd.Str;
		fluent.Where(nameof(TestEntity.Foo), compare, value);

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertWhere<TestEntity, string>(c, x => x.Foo, compare, value);

		// Assert
		Assert.Throws<GenericArgumentException>(() => fluent.AssertCalls(action));
	}

	[Fact]
	public void Compare_Not_Equal__Throws_EqualTypeException()
	{
		// Arrange
		var fluent = Create();
		var c0 = Compare.Is;
		var c1 = Compare.IsNot;
		fluent.Where(x => x.Foo, c0, Rnd.Str);

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertWhere<TestEntity, string>(c, x => x.Foo, c1, Rnd.Str);

		// Assert
		Assert.Throws<EqualTypeException>(() => fluent.AssertCalls(action));
	}

	[Fact]
	public void Value_Not_Equal__Throws_EqualTypeException()
	{
		// Arrange
		var fluent = Create();
		var v0 = Rnd.Lng;
		var v1 = Rnd.Lng;
		var compare = Compare.LessThanOrEqual;
		fluent.Where(x => x.Bar, compare, v0);

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertWhere<TestEntity, long>(c, x => x.Bar, compare, v1);

		// Assert
		Assert.Throws<EqualTypeException>(() => fluent.AssertCalls(action));
	}

	[Fact]
	public void Clause_Not_Equal_Throws_EqualTypeException()
	{
		// Arrange
		var fluent = Create();
		var parameters = new { value = Rnd.Guid };
		fluent.Where(Rnd.Str, parameters);

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertWhere(c, Rnd.Str, parameters);

		// Assert
		Assert.Throws<EqualTypeException>(() => fluent.AssertCalls(action));
	}

	[Fact]
	public void Parameters_Not_Equal__Throws_EqualJsonException()
	{
		// Arrange
		var fluent = Create();
		var clause = Rnd.Str;
		fluent.Where(clause, new { value = Rnd.Lng });

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertWhere(c, clause, new { value = Rnd.Lng });

		// Assert
		Assert.Throws<EqualJsonException>(() => fluent.AssertCalls(action));
	}
}
