// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;
using Jeebs.Data.Mapping;
using Xunit;
using static F.DataF.QueryF;
using static Jeebs.Linq.LinqExpressionExtensions.M;

namespace F.DataF.QueryF_Tests;

public class GetColumnFromExpression_Tests
{
	[Fact]
	public void Unable_To_Get_Column_Returns_PropertyDoesNotExistOnTypeMsg()
	{
		// Arrange

		// Act
		var r0 = GetColumnFromExpression<BrokenTable>(t => t.Bar);
		var r1 = GetColumnFromExpression(new BrokenTable(), t => t.Bar);

		// Assert
		var n0 = r0.AssertNone();
		Assert.IsType<PropertyDoesNotExistOnTypeMsg<BrokenTable>>(n0);
		var n1 = r1.AssertNone();
		Assert.IsType<PropertyDoesNotExistOnTypeMsg<BrokenTable>>(n1);
	}

	[Fact]
	public void Returns_Column_With_Property_Value_As_Name_And_Property_Name_As_Alias()
	{
		// Arrange
		var tableName = Rnd.Str;
		var table = new TestTable(tableName);

		// Act
		var r0 = GetColumnFromExpression(table, t => t.Foo);
		var r1 = GetColumnFromExpression<TestTable>(t => t.Foo);

		// Assert
		var s0 = r0.AssertSome();
		Assert.Equal(tableName, s0.Table.Name);
		Assert.Equal(table.Foo, s0.Name);
		Assert.Equal(nameof(table.Foo), s0.Alias);
		var s1 = r1.AssertSome();
		Assert.Equal("TestTable", s1.Table.Name);
		Assert.Equal(table.Foo, s1.Name);
		Assert.Equal(nameof(table.Foo), s1.Alias);
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
