// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using static StrongId.Testing.Generator;

namespace Jeebs.Data.Query.FluentQuery_Tests;

public class WhereId_Tests : FluentQuery_Tests
{
	[Fact]
	public void No_Values__Does_Not_Add_Predicate__Returns_Original_Query()
	{
		// Arrange
		var (query, _) = Setup();

		// Act
		var r0 = query.WhereId();
		var r1 = query.WhereId(Array.Empty<TestId>());

		// Assert
		var f0 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r0);
		Assert.Empty(f0.Parts.Where);
		var f1 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r1);
		Assert.Empty(f1.Parts.Where);
		Assert.Same(query, f0);
		Assert.Same(query, f1);
	}

	[Fact]
	public void Single_Id_Value__Adds_Predicate__Using_Compare_Equal()
	{
		// Arrange
		var (query, v) = Setup();
		var id = LongId<TestId>();

		// Act
		var result = query.WhereId(id);

		// Assert
		var fluent = Assert.IsType<FluentQuery<TestEntity, TestId>>(result);
		Assert.Collection(fluent.Parts.Where, x =>
		{
			Assert.Equal(v.Table.GetName(), x.column.TblName);
			Assert.Equal(v.Table.Id, x.column.ColName);
			Assert.Equal(nameof(TestEntity.Id), x.column.ColAlias);
			Assert.Equal(Enums.Compare.Equal, x.compare);
			Assert.Equal(id.Value, x.value);
		});
	}

	[Fact]
	public void Multiple_Ids__Adds_Predicate__Using_Compare_In()
	{
		// Arrange
		var (query, v) = Setup();
		var i0 = LongId<TestId>();
		var i1 = LongId<TestId>();

		// Act
		var result = query.WhereId(i0, i1);

		// Assert
		var fluent = Assert.IsType<FluentQuery<TestEntity, TestId>>(result);
		Assert.Collection(fluent.Parts.Where, x =>
		{
			Assert.Equal(v.Table.GetName(), x.column.TblName);
			Assert.Equal(v.Table.Id, x.column.ColName);
			Assert.Equal(nameof(TestEntity.Id), x.column.ColAlias);
			Assert.Equal(Enums.Compare.In, x.compare);
			Assert.Collection((IEnumerable<object>)x.value,
				x => Assert.Equal(i0.Value, x),
				x => Assert.Equal(i1.Value, x)
			);
		});
	}
}
