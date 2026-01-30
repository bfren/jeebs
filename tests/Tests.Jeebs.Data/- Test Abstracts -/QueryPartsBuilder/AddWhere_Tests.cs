// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Map;

namespace Jeebs.Data.QueryBuilder.QueryPartsBuilder_Tests;

public abstract class AddWhere_Tests<TBuilder, TId> : QueryPartsBuilder_Tests<TBuilder, TId>
	where TBuilder : QueryPartsBuilder<TId>
	where TId : class, IUnion, new()
{
	public abstract void Test00_Column_Exists_Adds_Where();

	protected void Test00()
	{
		// Arrange
		var (builder, v) = Setup();
		var bar = Rnd.Str;
		var cmp = Compare.LessThan;
		var val = Rnd.Int;

		// Act
		var result = builder.AddWhere<TestTable>(v.Parts, new(Rnd.Str, bar), c => c.Bar, cmp, val);

		// Assert
		var ok = result.AssertOk();
		var (column, compare, value) = Assert.Single(ok.Where);
		Assert.Equal(bar, column.ColName);
		Assert.Equal(cmp, compare);
		Assert.Equal(val, value);
	}

	public abstract void Test01_Column_Does_Not_Exist_Returns_None_With_PropertyDoesNotExistOnTypeMsg();

	protected void Test01()
	{
		// Arrange
		var (builder, v) = Setup();
		var cmp = Compare.LessThan;
		var val = Rnd.Int;

		// Act
		var result = builder.AddWhere<TestTable>(v.Parts, new(Rnd.Str, Rnd.Str), _ => Rnd.Str, cmp, val);

		// Assert
		_ = result.AssertFailure("Unable to get column from expression for table '{Table}'.", nameof(TestTable));
	}

	public sealed record class TestTable(string Foo, string Bar) : Table(Rnd.Str);
}
