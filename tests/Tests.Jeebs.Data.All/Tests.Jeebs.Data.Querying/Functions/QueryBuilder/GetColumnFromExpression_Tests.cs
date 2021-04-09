// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Data;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying.Exceptions;
using Xunit;
using static F.DataF.QueryBuilderF;

namespace F.DataF.QueryBuilderF_Tests
{
	public class GetColumnFromExpression_Tests
	{
		[Fact]
		public void Unable_To_Get_Column_Throws_UnableToGetColumnFromExpressionException()
		{
			// Arrange

			// Act
			static void a0() => GetColumnFromExpression<BrokenTable>(t => t.Foo);
			static void a1() => GetColumnFromExpression(new BrokenTable(), t => t.Foo);

			// Assert
			Assert.Throws<UnableToGetColumnFromExpressionException<BrokenTable>>(a0);
			Assert.Throws<UnableToGetColumnFromExpressionException<BrokenTable>>(a1);
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
			Assert.Equal(tableName, r0.Table);
			Assert.Equal(table.Foo, r0.Name);
			Assert.Equal(nameof(table.Foo), r0.Alias);
			Assert.Equal("TestTable", r1.Table);
			Assert.Equal(table.Foo, r1.Name);
			Assert.Equal(nameof(table.Foo), r1.Alias);
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
