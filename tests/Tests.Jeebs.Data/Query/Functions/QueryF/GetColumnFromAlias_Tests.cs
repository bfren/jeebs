// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

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
		_ = r0.AssertFail("Column with alias '{Alias}' not found in table '{Table}'.");
		_ = r1.AssertFail("Column with alias '{Alias}' not found in table '{Table}'.");
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
		var f0 = r0.AssertFail("Column with alias '{Alias}' has null or empty name in table '{Table}'.");
		Assert.Collection(f0.Args!,
			x => Assert.Equal(alias, x),
			x => Assert.Equal(table, x)
		);
		var f1 = r1.AssertFail("Column with alias '{Alias}' has null or empty name in table '{Table}'.");
		Assert.Collection(f1.Args!,
			x => Assert.Equal(alias, x),
			x => Assert.Equal(table, x)
		);
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
		var ok0 = r0.AssertOk();
		Assert.Equal(table.GetName(), ok0.TblName);
		Assert.Equal(alias, ok0.ColAlias);
		Assert.Equal(value, ok0.ColName);
		var ok1 = r1.AssertOk();
		Assert.Equal(table.GetName(), ok1.TblName);
		Assert.Equal(alias, ok1.ColAlias);
		Assert.Equal(value, ok1.ColName);
	}

	public sealed record class TestTable : Table
	{
		public string? Foo { get; init; }

		public string Bar =>
			nameof(TestTable) + nameof(Bar);

		public TestTable() : base(nameof(TestTable)) { }
	}
}
