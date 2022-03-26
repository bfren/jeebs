// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;

namespace Jeebs.Data.Query.QueryFluent_Tests;

public class WhereNotIn_Tests : QueryFluent_Tests
{
	[Fact]
	public void No_Values_Does_Not_Add_Predicate()
	{
		// Arrange
		var (query, v) = Setup();

		// Act
		var r0 = query.WhereNotIn(nameof(TestEntity.Foo), Array.Empty<string?>());
		var r1 = query.WhereNotIn(x => x.Foo, Array.Empty<string?>());

		// Assert
		var f0 = Assert.IsType<QueryFluent<TestEntity, TestId>>(r0);
		Assert.Empty(f0.Predicates);
		var f1 = Assert.IsType<QueryFluent<TestEntity, TestId>>(r1);
		Assert.Empty(f1.Predicates);
	}

	[Fact]
	public void Adds_Predicate()
	{
		// Arrange
		var (query, v) = Setup();
		var v0 = Rnd.Str;
		var v1 = Rnd.Str;

		// Act
		var r0 = query.WhereNotIn(nameof(TestEntity.Foo), new[] { v0, v1 });
		var r1 = query.WhereNotIn(x => x.Foo, new[] { v0, v1 });

		// Assert
		var f0 = Assert.IsType<QueryFluent<TestEntity, TestId>>(r0);
		Assert.Collection(f0.Predicates, x =>
		{
			Assert.Equal(nameof(TestEntity.Foo), x.col);
			Assert.Equal(Compare.NotIn, x.cmp);
			Assert.Collection((IEnumerable<string>)x.val!,
				y => Assert.Equal(v0, y),
				y => Assert.Equal(v1, y)
			);
		});
		var f1 = Assert.IsType<QueryFluent<TestEntity, TestId>>(r1);
		Assert.Collection(f1.Predicates, x =>
		{
			Assert.Equal(nameof(TestEntity.Foo), x.col);
			Assert.Equal(Compare.NotIn, x.cmp);
			Assert.Collection((IEnumerable<string>)x.val!,
				y => Assert.Equal(v0, y),
				y => Assert.Equal(v1, y)
			);
		});
	}
}
