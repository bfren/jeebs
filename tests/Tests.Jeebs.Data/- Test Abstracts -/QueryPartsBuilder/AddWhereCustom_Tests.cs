// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Id;
using static Jeebs.Data.Query.QueryPartsBuilder.M;

namespace Jeebs.Data.Query.QueryPartsBuilder_Tests;

public abstract class AddWhereCustom_Tests<TBuilder, TId> : QueryPartsBuilder_Tests<TBuilder, TId>
	where TBuilder : QueryPartsBuilder<TId>
	where TId : IStrongId
{
	public abstract void Test00_Clause_Null_Or_Empty_Returns_None_With_TryingToAddEmptyClauseToWhereCustomMsg(string input);

	public static IEnumerable<string?[]> Test00_Data()
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
		var none = result.AssertNone();
		Assert.IsType<TryingToAddEmptyClauseToWhereCustomMsg>(none);
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
		var none = result.AssertNone();
		Assert.IsType<UnableToAddParametersToWhereCustomMsg>(none);
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
		Assert.Collection(some.WhereCustom,
			x =>
			{
				Assert.Equal(clause, x.clause);
				Assert.Collection(x.parameters,
					y =>
					{
						Assert.Equal(nameof(value), y.Key);
						Assert.Equal(value, y.Value);
					}
				);
			}
		);
	}
}
