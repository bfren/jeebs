// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.DataF_Tests;

public class GetColumnFromExpression_Tests
{
	[Fact]
	public void Unable_To_Get_Column_Returns_PropertyDoesNotExistOnTypeMsg()
	{
		// Arrange

		// Act
		var r0 = DataF.GetColumnFromExpression<BrokenTable>(t => t.Bar);
		var r1 = DataF.GetColumnFromExpression(new BrokenTable(), t => t.Bar);

		// Assert
		r0.AssertFailure("Unable to get column from expression for table '{Table}'.", nameof(BrokenTable));
		r1.AssertFailure("Unable to get column from expression for table '{Table}'.", nameof(BrokenTable));
	}

	[Fact]
	public void Returns_Column_With_Property_Value_As_Name_And_Property_Name_As_Alias()
	{
		// Arrange
		var tableName = Rnd.Str;
		var table = new TestTable(tableName);

		// Act
		var r0 = DataF.GetColumnFromExpression(table, t => t.Foo);
		var r1 = DataF.GetColumnFromExpression<TestTable>(t => t.Foo);

		// Assert
		var ok0 = r0.AssertOk();
		Assert.Equal(tableName, ok0.TblName.Name);
		Assert.Equal(table.Foo, ok0.ColName);
		Assert.Equal(nameof(table.Foo), ok0.ColAlias);
		var ok1 = r1.AssertOk();
		Assert.Equal("TestTable", ok1.TblName.Name);
		Assert.Equal(table.Foo, ok1.ColName);
		Assert.Equal(nameof(table.Foo), ok1.ColAlias);
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
