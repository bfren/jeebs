// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;
using static Jeebs.Data.Query.Functions.QueryF.M;
using static MaybeF.F.EnumerableF.M;

namespace Jeebs.Data.Query.Functions.QueryF_Tests;

public class GetColumnFromAlias_Tests
{
	[Fact]
	public void Property_Does_Not_Exist_On_Table__Returns_None_With_NoMatchingItemsMsg()
	{
		// Arrange
		var table = new TestTable();

		// Act
		var r0 = QueryF.GetColumnFromAlias(table, Rnd.Str);
		var r1 = QueryF.GetColumnFromAlias<TestTable>(Rnd.Str);

		// Assert
		r0.AssertNone().AssertType<NoMatchingItemsMsg>();
		r1.AssertNone().AssertType<NoMatchingItemsMsg>();
	}

	[Fact]
	public void Property_Exists__Get_Value_Is_Null__Returns_None_With_UnableToGetColumnFromAliasMsg()
	{
		// Arrange
		var table = new TestTable();
		var alias = nameof(TestTable.Foo);

		// Act
		var r0 = QueryF.GetColumnFromAlias(table, alias);
		var r1 = QueryF.GetColumnFromAlias<TestTable>(alias);

		// Assert
		var n0 = r0.AssertNone().AssertType<UnableToGetColumnFromAliasMsg>();
		Assert.Equal(table, n0.Table);
		Assert.Equal(alias, n0.ColumnAlias);
		var n1 = r1.AssertNone().AssertType<UnableToGetColumnFromAliasMsg>();
		Assert.Equal(table, n1.Table);
		Assert.Equal(alias, n1.ColumnAlias);
	}

	[Fact]
	public void Property_Exists__Get_Value_Is_String__Returns_Column_With_Correct_Values()
	{
		// Arrange
		var table = new TestTable();
		var alias = nameof(TestTable.Bar);
		var value = nameof(TestTable) + nameof(TestTable.Bar);

		// Act
		var r0 = QueryF.GetColumnFromAlias(table, alias);
		var r1 = QueryF.GetColumnFromAlias<TestTable>(alias);

		// Assert
		var s0 = r0.AssertSome();
		Assert.Equal(table.GetName(), s0.TblName);
		Assert.Equal(alias, s0.ColAlias);
		Assert.Equal(value, s0.ColName);
		var s1 = r1.AssertSome();
		Assert.Equal(table.GetName(), s1.TblName);
		Assert.Equal(alias, s1.ColAlias);
		Assert.Equal(value, s1.ColName);
	}

	public sealed record class TestTable : Table
	{
		public string? Foo { get; init; }

		public string Bar =>
			nameof(TestTable) + nameof(Bar);

		public TestTable() : base(nameof(TestTable)) { }
	}
}
