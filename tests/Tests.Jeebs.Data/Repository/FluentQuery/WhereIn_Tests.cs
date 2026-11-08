// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;

namespace Jeebs.Data.Repository.FluentQuery_Tests;

public class WhereIn_Tests : FluentQuery_Tests
{
	[Fact]
	public void No_Values__Adds_Predicate_With_Empty_List()
	{
		// Arrange
		var (query, _) = Setup();

		// Act
		var r0 = query.WhereIn(nameof(TestEntity.Foo), Array.Empty<string?>());
		var r1 = query.WhereIn(x => x.Foo, []);

		// Assert
		var f0 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r0);
		var (_, compare0, value0) = Assert.Single(f0.Parts.Where);
		Assert.Equal(Compare.In, compare0);
		Assert.Empty((IEnumerable<string?>)value0);
		var f1 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r1);
		var (_, compare1, value1) = Assert.Single(f1.Parts.Where);
		Assert.Equal(Compare.In, compare1);
		Assert.Empty((IEnumerable<string?>)value1);
	}

	[Fact]
	public void Adds_Predicate()
	{
		// Arrange
		var (query, v) = Setup();
		var v0 = Rnd.Str;
		var v1 = Rnd.Str;

		// Act
		var r0 = query.WhereIn(nameof(TestEntity.Foo), [v0, v1]);
		var r1 = query.WhereIn(x => x.Foo, [v0, v1]);

		// Assert
		var f0 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r0);
		var (column0, compare0, value0) = Assert.Single(f0.Parts.Where);
		Assert.Equal(v.Table.GetName(), column0.TblName);
		Assert.Equal(v.Table.Foo, column0.ColName);
		Assert.Equal(nameof(TestEntity.Foo), column0.ColAlias);
		Assert.Equal(Compare.In, compare0);
		Assert.Collection((IEnumerable<string>)value0,
			y => Assert.Equal(v0, y),
			y => Assert.Equal(v1, y)
		);
		var f1 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r1);
		var (column1, compare1, value1) = Assert.Single(f1.Parts.Where);
		Assert.Equal(v.Table.GetName(), column1.TblName);
		Assert.Equal(v.Table.Foo, column1.ColName);
		Assert.Equal(nameof(TestEntity.Foo), column1.ColAlias);
		Assert.Equal(Compare.In, compare1);
		Assert.Collection((IEnumerable<string>)value1,
			y => Assert.Equal(v0, y),
			y => Assert.Equal(v1, y)
		);
	}
}
