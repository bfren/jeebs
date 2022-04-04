// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using MaybeF;
using static Jeebs.Data.Query.FluentQuery.M;

namespace Jeebs.Data.Query.FluentQuery_Tests;

public class Where_Tests0 : FluentQuery_Tests
{
	[Fact]
	public void Query_Errors__Does_Not_Add_Clause__Returns_Original_Query()
	{
		// Arrange
		var (query, _) = Setup();
		query.Errors.Add(Substitute.For<IMsg>());

		// Act
		var result = query.Where(Rnd.Str, Rnd.Guid);

		// Assert
		var fluent = Assert.IsType<FluentQuery<TestEntity, TestId>>(result);
		Assert.Empty(fluent.Parts.Where);
		Assert.Same(query, fluent);
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData("  ")]
	public void Clause_Null_Or_Whitespace__Adds_Error__Returns_Original_Query(string clause)
	{
		// Arrange
		var (query, _) = Setup();

		// Act
		var result = query.Where(clause, Rnd.Lng);

		// Assert
		var fluent = Assert.IsType<FluentQuery<TestEntity, TestId>>(result);
		Assert.Empty(fluent.Parts.Where);
		Assert.Same(query, fluent);
		Assert.Collection(fluent.Errors,
			x => x.AssertType<TryingToAddEmptyClauseToWhereMsg>()
		);
	}

	[Theory]
	[InlineData(null)]
	[InlineData(true)]
	[InlineData(42)]
	[InlineData(42L)]
	[InlineData('n')]
	[InlineData("invalid")]
	public void Unable_To_Add_Parameters__Adds_Error__Returns_Original_Query(object param)
	{
		// Arrange
		var (query, _) = Setup();

		// Act
		var result = query.Where(Rnd.Str, param);

		// Assert
		var fluent = Assert.IsType<FluentQuery<TestEntity, TestId>>(result);
		Assert.Empty(fluent.Parts.Where);
		Assert.Same(query, fluent);
		Assert.Collection(fluent.Errors,
			x => x.AssertType<UnableToAddParametersToWhereMsg>()
		);
	}

	[Fact]
	public void Adds_Clause_And_Parameters()
	{
		// Arrange
		var (query, _) = Setup();
		var clause = Rnd.Str;
		var v0 = Rnd.Lng;
		var v1 = Rnd.Guid;
		var v2 = Rnd.Str;
		var param = new { A = v0, B = v1, C = v2 };

		// Act
		var result = query.Where(clause, param);

		// Assert
		var fluent = Assert.IsType<FluentQuery<TestEntity, TestId>>(result);
		Assert.Collection(fluent.Parts.WhereCustom,
			x =>
			{
				Assert.Equal(clause, x.clause);
				Assert.Collection(x.parameters,
					y =>
					{
						Assert.Equal(nameof(param.A), y.Key);
						Assert.Equal(v0, y.Value);
					},
					y =>
					{
						Assert.Equal(nameof(param.B), y.Key);
						Assert.Equal(v1, y.Value);
					},
					y =>
					{
						Assert.Equal(nameof(param.C), y.Key);
						Assert.Equal(v2, y.Value);
					}
				);
			}
		);
	}
}
