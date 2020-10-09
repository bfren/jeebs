using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Data.Clients.MySql.MySqlAdapter_Tests
{
	public class UpdateSingle_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void Invalid_Table_Throws_InvalidOperationException(string input)
		{
			// Arrange
			var adapter = new MySqlAdapter();

			// Act
			void action() => adapter.UpdateSingle(input, new List<string>(), new List<string>(), string.Empty, string.Empty);

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal($"Table is invalid: '{input}'.", ex.Message);
		}

		[Fact]
		public void Empty_Columns_Throws_InvalidOperationException()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var table = F.Rnd.String;
			var columns = new List<string>();

			// Act
			void action() => adapter.UpdateSingle(table, columns, new List<string>(), string.Empty, string.Empty);

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal("The list of columns cannot be empty.", ex.Message);
		}

		[Fact]
		public void Empty_Aliases_Throws_InvalidOperationException()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var table = F.Rnd.String;
			var columns = new List<string> { F.Rnd.String };
			var aliases = new List<string>();

			// Act
			void action() => adapter.UpdateSingle(table, columns, aliases, string.Empty, string.Empty);

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal("The list of aliases cannot be empty.", ex.Message);
		}

		[Fact]
		public void Columns_And_Aliases_Different_Lengths_Throws_InvalidOperationException()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var table = F.Rnd.String;
			var columns = new List<string> { F.Rnd.String };
			var aliases = new List<string> { F.Rnd.String, F.Rnd.String };

			// Act
			void action() => adapter.UpdateSingle(table, columns, aliases, string.Empty, string.Empty);

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal("The number of columns (1) and aliases (2) must be the same.", ex.Message);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void Invalid_IdColumn_Throws_InvalidOperationException(string input)
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var columns = new List<string> { F.Rnd.String, F.Rnd.String };
			var aliases = new List<string> { F.Rnd.String, F.Rnd.String };

			// Act
			void action() => adapter.UpdateSingle(F.Rnd.String, columns, aliases, input, string.Empty);

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal($"ID Column is invalid: '{input}'.", ex.Message);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void Invalid_IdAlias_Throws_InvalidOperationException(string input)
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var columns = new List<string> { F.Rnd.String, F.Rnd.String };
			var aliases = new List<string> { F.Rnd.String, F.Rnd.String };

			// Act
			void action() => adapter.UpdateSingle(F.Rnd.String, columns, aliases, F.Rnd.String, input);

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal($"ID Alias is invalid: '{input}'.", ex.Message);
		}

		[Fact]
		public void Returns_Update_Query()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var table = F.Rnd.String;

			var c0 = F.Rnd.String;
			var c1 = F.Rnd.String;
			var columns = new List<string> { c0, c1 };

			var a0 = F.Rnd.String;
			var a1 = F.Rnd.String;
			var aliases = new List<string> { a0, a1 };

			var idColumn = F.Rnd.String;
			var idAlias = F.Rnd.String;

			// Act
			var result = adapter.UpdateSingle(table, columns, aliases, idColumn, idAlias);

			// Assert
			Assert.Equal($"UPDATE {table} SET {c0} = @{a0}, {c1} = @{a1} WHERE {idColumn} = @{idAlias};", result);
		}

		[Fact]
		public void Returns_Update_Query_With_Version()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var table = F.Rnd.String;

			var c0 = F.Rnd.String;
			var c1 = F.Rnd.String;
			var columns = new List<string> { c0, c1 };

			var a0 = F.Rnd.String;
			var a1 = F.Rnd.String;
			var aliases = new List<string> { a0, a1 };

			var idColumn = F.Rnd.String;
			var idAlias = F.Rnd.String;

			var versionColumn = F.Rnd.String;
			var versionAlias = F.Rnd.String;

			// Act
			var result = adapter.UpdateSingle(table, columns, aliases, idColumn, idAlias, versionColumn, versionAlias);

			// Assert
			Assert.Equal($"UPDATE {table} SET {c0} = @{a0}, {c1} = @{a1} WHERE {idColumn} = @{idAlias} AND {versionColumn} = @{versionAlias} - 1;", result);
		}
	}
}
