// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using Jeebs.Data;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
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
			var result = GetColumnFromExpression(table, t => t.Foo);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<UnableToGetColumnFromExpressionExceptionMsg>(none);
		}

		[Fact]
		public void Returns_Column_With_Property_Value_As_Name_And_Property_Name_As_Alias()
		{
			// Arrange
			var tableName = Rnd.Str;
			var table = new TestTable(tableName);

			// Act
			var result = GetColumnFromExpression(table, t => t.Foo);

			// Assert
			var some = result.AssertSome();
			Assert.Equal(tableName, some.Table);
			Assert.Equal(table.Foo, some.Name);
			Assert.Equal(nameof(table.Foo), some.Alias);
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

			public TestTable(string name) : base(name) { }
		}
	}
}
