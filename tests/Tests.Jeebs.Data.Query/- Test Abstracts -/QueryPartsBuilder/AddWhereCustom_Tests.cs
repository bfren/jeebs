// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using StrongId;
using static Jeebs.Data.Query.QueryPartsBuilder.M;

namespace Jeebs.Data.Query.QueryPartsBuilder_Tests;

public abstract class AddWhereCustom_Tests<TBuilder, TId> : QueryPartsBuilder_Tests<TBuilder, TId>
	where TBuilder : QueryPartsBuilder<TId>
	where TId : class, IStrongId, new()
{
	public abstract void Test00_Clause_Null_Or_Empty_Returns_None_With_TryingToAddEmptyClauseToWhereCustomMsg(string input);

	public static IEnumerable<object?[]> Test00_Data()
	{
		yield return new string?[] { null };
		yield return new[] { "" };
		yield return new[] { " " };
	}

	protected void Test00(string input)
	{
		// Arrange
		var (builder, v) = Setup();

		// Act
		var result = builder.AddWhereCustom(v.Parts, input, new());

		// Assert
		result.AssertNone().AssertType<TryingToAddEmptyClauseToWhereCustomMsg>();
	}

	public abstract void Test01_Invalid_Parameters_Returns_None_With_UnableToAddParametersToWhereCustomMsg(object input);

	public static IEnumerable<object?[]> Test01_Data()
	{
		yield return new object?[] { null };
		yield return new object[] { 42 };
		yield return new object[] { true };
		yield return new object[] { 'c' };
	}

	protected void Test01(object input)
	{
		// Arrange
		var (builder, v) = Setup();
		var clause = Rnd.Str;

		// Act
		var result = builder.AddWhereCustom(v.Parts, clause, input);

		// Assert
		result.AssertNone().AssertType<UnableToAddParametersToWhereCustomMsg>();
	}

	public abstract void Test02_Returns_New_Parts_With_Clause_And_Parameters();

	protected void Test02()
	{
		// Arrange
		var (builder, v) = Setup();
		var clause = Rnd.Str;
		var value = Rnd.Lng;
		var parameters = new { value };

		// Act
		var result = builder.AddWhereCustom(v.Parts, clause, parameters);

		// Assert
		var some = result.AssertSome();
		Assert.NotSame(v.Parts, some);
		var s0 = Assert.Single(some.WhereCustom);
		Assert.Equal(clause, s0.clause);
		var s1 = Assert.Single(s0.parameters);
		Assert.Equal(nameof(value), s1.Key);
		Assert.Equal(value, s1.Value);
	}
}
