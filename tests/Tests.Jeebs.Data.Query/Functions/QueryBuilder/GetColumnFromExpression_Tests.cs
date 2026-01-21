// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;
using Wrap.Exceptions;

namespace Jeebs.Data.Query.Functions.QueryBuilderF_Tests;

public class GetColumnFromExpression_Tests
{
	[Fact]
	public void Unable_To_Get_Column_Throws_UnableToGetColumnFromExpressionException()
	{
		// Arrange

		// Act
		var a0 = void () => QueryBuilderF.GetColumnFromExpression<BrokenTable>(t => t.Bar);
		var a1 = void () => QueryBuilderF.GetColumnFromExpression(new BrokenTable(), t => t.Bar);

		// Assert
		var ex0 = Assert.Throws<FailureException>(a0);
		Assert.Equal("Unable to get column from expression for table '{Table}'.", ex0.Message);
		var ex1 = Assert.Throws<FailureException>(a1);
		Assert.Equal("Unable to get column from expression for table '{Table}'.", ex1.Message);
	}

	[Fact]
	public void Returns_Column_With_Property_Value_As_Name_And_Property_Name_As_Alias()
	{
		// Arrange
		var tableName = Rnd.Str;
		var table = new TestTable(tableName);

		// Act
		var r0 = QueryBuilderF.GetColumnFromExpression(table, t => t.Foo);
		var r1 = QueryBuilderF.GetColumnFromExpression<TestTable>(t => t.Foo);

		// Assert
		Assert.Equal(tableName, r0.TblName.Name);
		Assert.Equal(table.Foo, r0.ColName);
		Assert.Equal(nameof(table.Foo), r0.ColAlias);
		Assert.Equal(nameof(TestTable), r1.TblName.Name);
		Assert.Equal(table.Foo, r1.ColName);
		Assert.Equal(nameof(table.Foo), r1.ColAlias);
	}

	public record class BrokenTable : TestTable
	{
		internal string Bar =>
			Prefix + nameof(Bar);

		public BrokenTable() : base(Rnd.Str) { }
	}

	public record class TestTable : Table
	{
		public static readonly string Prefix =
			"Bar_";

		public string Foo =>
			Prefix + nameof(Foo);

		public TestTable() : base(nameof(TestTable)) { }

		public TestTable(string name) : base(name) { }
	}
}
