// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using Jeebs;
using Jeebs.Data.Mapping;
using Xunit;
using static F.DataF.QueryF;
using static F.DataF.QueryF.Msg;

namespace F.DataF.QueryF_Tests
{
	public class GetColumnFromExpression_Tests
	{
		[Fact]
		public void Exception_While_Making_Column_Returns_None_With_UnableToGetColumnFromExpressionExceptionMsg()
		{
			// Arrange
			var table = new BrokenTable();

			// Act
			var r0 = GetColumnFromExpression(table, t => t.Foo);
			var r1 = GetColumnFromExpression<BrokenTable>(t => t.Foo);

			// Assert
			var n0 = r0.AssertNone();
			Assert.IsType<UnableToGetColumnFromExpressionExceptionMsg>(n0);
			var n1 = r1.AssertNone();
			Assert.IsType<UnableToGetColumnFromExpressionExceptionMsg>(n1);
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
			Assert.Equal(tableName, s0.Table);
			Assert.Equal(table.Foo, s0.Name);
			Assert.Equal(nameof(table.Foo), s0.Alias);
			var s1 = r1.AssertSome();
			Assert.Equal("TestTable", s1.Table);
			Assert.Equal(table.Foo, s1.Name);
			Assert.Equal(nameof(table.Foo), s1.Alias);
		}

		public record BrokenTable : TestTable
		{
			public BrokenTable() : base(F.Rnd.Str) { }

			public override string GetName() =>
				throw new Exception();
		}

		public record TestTable : Table
		{
			public const string Prefix =
				"Bar_";

			public string Foo =>
				Prefix + nameof(Foo);

			public TestTable() : base(nameof(TestTable)) { }

			public TestTable(string name) : base(name) { }
		}
	}
}
