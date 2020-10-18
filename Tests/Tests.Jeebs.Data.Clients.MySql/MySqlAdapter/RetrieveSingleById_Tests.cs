using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Data.Clients.MySql.MySqlAdapter_Tests
{
	public class RetrieveSingleById_Tests
	{

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void Invalid_Table_Throws_InvalidOperationException(string input)
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var columns = new List<string> { F.Rnd.Str, F.Rnd.Str };

			// Act
			void action() => adapter.RetrieveSingleById(input, columns, F.Rnd.Str);

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal($"Table is invalid: '{input}'.", ex.Message);
		}

		[Fact]
		public void Empty_Columns_Throws_InvalidOperationException()
		{
			// Arrange
			var adapter = new MySqlAdapter();

			// Act
			void action() => adapter.RetrieveSingleById(F.Rnd.Str, new List<string>(), F.Rnd.Str);

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal("The list of columns cannot be empty.", ex.Message);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void Invalid_IdColumn_Throws_InvalidOperationException(string input)
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var columns = new List<string> { F.Rnd.Str, F.Rnd.Str };

			// Act
			void action() => adapter.RetrieveSingleById(F.Rnd.Str, columns, input);

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal($"ID Column is invalid: '{input}'.", ex.Message);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("  ")]
		public void Removes_Invalid_Columns_From_Query(string input)
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var columns = new List<string> { "one", "two", input, "`fo our`" };
			var table = F.Rnd.Str;
			var idColumn = F.Rnd.Str;

			// Act
			var result = adapter.RetrieveSingleById(table, columns, idColumn);

			// Assert
			Assert.Equal($"SELECT one, two, `fo our` FROM {table} WHERE {idColumn} = @Id;", result);
		}

		[Theory]
		[InlineData(null, "Id")]
		[InlineData("otherAlias", "otherAlias")]
		public void Returns_RetrieveSingleById_Query(string input, string expected)
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var c0 = F.Rnd.Str;
			var c1 = F.Rnd.Str;
			var columns = new List<string> { c0, c1 };
			var table = F.Rnd.Str;
			var idColumn = F.Rnd.Str;

			// Act
			var result = adapter.RetrieveSingleById(table, columns, idColumn, input);

			// Assert
			Assert.Equal($"SELECT {c0}, {c1} FROM {table} WHERE {idColumn} = @{expected};", result);
		}
	}
}
