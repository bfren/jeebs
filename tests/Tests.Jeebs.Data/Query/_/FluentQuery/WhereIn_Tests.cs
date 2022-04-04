// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;

namespace Jeebs.Data.Query.FluentQuery_Tests;

public class WhereIn_Tests : FluentQuery_Tests
{
	[Fact]
	public void No_Values__Does_Not_Add_Predicate__Returns_Original_Query()
	{
		// Arrange
		var (query, v) = Setup();

		// Act
		var r0 = query.WhereIn(nameof(TestEntity.Foo), Array.Empty<string?>());
		var r1 = query.WhereIn(x => x.Foo, Array.Empty<string?>());

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
		var r0 = query.WhereIn(nameof(TestEntity.Foo), new[] { v0, v1 });
		var r1 = query.WhereIn(x => x.Foo, new[] { v0, v1 });

		// Assert
		var f0 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r0);
		Assert.Collection(f0.Parts.Where, x =>
		{
			Assert.Equal(v.Table.GetName(), x.column.TblName);
			Assert.Equal(v.Table.Foo, x.column.ColName);
			Assert.Equal(nameof(TestEntity.Foo), x.column.ColAlias);
			Assert.Equal(Compare.In, x.compare);
			Assert.Collection((IEnumerable<string>)x.value!,
				y => Assert.Equal(v0, y),
				y => Assert.Equal(v1, y)
			);
		});
		var f1 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r1);
		Assert.Collection(f1.Parts.Where, x =>
		{
			Assert.Equal(v.Table.GetName(), x.column.TblName);
			Assert.Equal(v.Table.Foo, x.column.ColName);
			Assert.Equal(nameof(TestEntity.Foo), x.column.ColAlias);
			Assert.Equal(Compare.In, x.compare);
			Assert.Collection((IEnumerable<string>)x.value!,
				y => Assert.Equal(v0, y),
				y => Assert.Equal(v1, y)
			);
		});
	}
}
