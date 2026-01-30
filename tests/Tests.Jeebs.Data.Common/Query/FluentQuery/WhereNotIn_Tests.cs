// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;

namespace Jeebs.Data.Common.FluentQuery.FluentQuery_Tests;

public class WhereNotIn_Tests : FluentQuery_Tests
{
	[Fact]
	public void No_Values__Does_Not_Add_Predicate__Returns_Original_Query()
	{
		// Arrange
		var (query, v) = Setup();

		// Act
		var r0 = query.WhereNotIn(nameof(TestEntity.Foo), Array.Empty<string?>());
		var r1 = query.WhereNotIn(x => x.Foo, []);

		// Assert
		var f0 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r0);
		Assert.Empty(f0.Parts.Where);
		var f1 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r1);
		Assert.Empty(f1.Parts.Where);
		Assert.Same(query, f0);
		Assert.Same(query, f1);
	}

	[Fact]
	public void Adds_Predicate()
	{
		// Arrange
		var (query, v) = Setup();
		var v0 = Rnd.Str;
		var v1 = Rnd.Str;

		// Act
		var r0 = query.WhereNotIn(nameof(TestEntity.Foo), [v0, v1]);
		var r1 = query.WhereNotIn(x => x.Foo, [v0, v1]);

		// Assert
		var f0 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r0);
		var (column0, compare0, value0) = Assert.Single(f0.Parts.Where);
		Assert.Equal(v.Table.GetName(), column0.TblName);
		Assert.Equal(v.Table.Foo, column0.ColName);
		Assert.Equal(nameof(TestEntity.Foo), column0.ColAlias);
		Assert.Equal(Compare.NotIn, compare0);
		Assert.Collection((IEnumerable<string>)value0!,
			y => Assert.Equal(v0, y),
			y => Assert.Equal(v1, y)
		);
		var f1 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r1);
		var (column1, compare1, value1) = Assert.Single(f1.Parts.Where);
		Assert.Equal(v.Table.GetName(), column1.TblName);
		Assert.Equal(v.Table.Foo, column1.ColName);
		Assert.Equal(nameof(TestEntity.Foo), column1.ColAlias);
		Assert.Equal(Compare.NotIn, compare1);
		Assert.Collection((IEnumerable<string>)value1!,
			y => Assert.Equal(v0, y),
			y => Assert.Equal(v1, y)
		);
	}
}
