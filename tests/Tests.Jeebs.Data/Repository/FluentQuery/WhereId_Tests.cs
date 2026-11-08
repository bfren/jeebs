// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Repository.FluentQuery_Tests;

public class WhereId_Tests : FluentQuery_Tests
{
	[Fact]
	public void No_Values__Adds_Predicate__Using_Compare_In_With_Empty_List()
	{
		// Arrange
		var (query, _) = Setup();

		// Act
		var r0 = query.WhereId();
		var r1 = query.WhereId([]);

		// Assert
		var f0 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r0);
		var (_, compare0, value0) = Assert.Single(f0.Parts.Where);
		Assert.Equal(Enums.Compare.In, compare0);
		Assert.Empty((IEnumerable<object>)value0);
		var f1 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r1);
		var (_, compare1, value1) = Assert.Single(f1.Parts.Where);
		Assert.Equal(Enums.Compare.In, compare1);
		Assert.Empty((IEnumerable<object>)value1);
	}

	[Fact]
	public void Single_Id_Value__Adds_Predicate__Using_Compare_Equal()
	{
		// Arrange
		var (query, v) = Setup();
		var id = IdGen.LongId<TestId>();

		// Act
		var result = query.WhereId(id);

		// Assert
		var fluent = Assert.IsType<FluentQuery<TestEntity, TestId>>(result);
		var (column, compare, value) = Assert.Single(fluent.Parts.Where);
		Assert.Equal(v.Table.GetName(), column.TblName);
		Assert.Equal(v.Table.Id, column.ColName);
		Assert.Equal(nameof(TestEntity.Id), column.ColAlias);
		Assert.Equal(Enums.Compare.Equal, compare);
		Assert.Equal(id.Value, value);
	}

	[Fact]
	public void Multiple_Ids__Adds_Predicate__Using_Compare_In()
	{
		// Arrange
		var (query, v) = Setup();
		var i0 = IdGen.LongId<TestId>();
		var i1 = IdGen.LongId<TestId>();

		// Act
		var result = query.WhereId(i0, i1);

		// Assert
		var fluent = Assert.IsType<FluentQuery<TestEntity, TestId>>(result);
		var (column, compare, value) = Assert.Single(fluent.Parts.Where);
		Assert.Equal(v.Table.GetName(), column.TblName);
		Assert.Equal(v.Table.Id, column.ColName);
		Assert.Equal(nameof(TestEntity.Id), column.ColAlias);
		Assert.Equal(Enums.Compare.In, compare);
		Assert.Collection((IEnumerable<object>)value,
			x => Assert.Equal(i0.Value, x),
			x => Assert.Equal(i1.Value, x)
		);
	}
}
