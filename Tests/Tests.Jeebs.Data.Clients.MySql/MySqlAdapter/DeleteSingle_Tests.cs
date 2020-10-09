using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Clients.MySql.MySqlAdapter_Tests
{
	public class DeleteSingle_Tests
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
			void action() => adapter.DeleteSingle(input, F.Rnd.String, F.Rnd.String);

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal($"Table is invalid: '{input}'.", ex.Message);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void Invalid_IdColumn_Throws_InvalidOperationException(string input)
		{
			// Arrange
			var adapter = new MySqlAdapter();

			// Act
			void action() => adapter.DeleteSingle(F.Rnd.String, input, F.Rnd.String);

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

			// Act
			void action() => adapter.DeleteSingle(F.Rnd.String, F.Rnd.String, input);

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal($"ID Alias is invalid: '{input}'.", ex.Message);
		}

		[Fact]
		public void Returns_Delete_Query()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var table = F.Rnd.String;
			var idColumn = F.Rnd.String;
			var idAlias = F.Rnd.String;

			// Act
			var result = adapter.DeleteSingle(table, idColumn, idAlias);

			// Assert
			Assert.Equal($"DELETE FROM {table} WHERE {idColumn} = @{idAlias};", result);
		}

		[Fact]
		public void Returns_Delete_Query_With_Version()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var table = F.Rnd.String;
			var idColumn = F.Rnd.String;
			var idAlias = F.Rnd.String;
			var versionColumn = F.Rnd.String;
			var versionAlias = F.Rnd.String;

			// Act
			var result = adapter.DeleteSingle(table, idColumn, idAlias, versionColumn, versionAlias);

			// Assert
			Assert.Equal($"DELETE FROM {table} WHERE {idColumn} = @{idAlias} AND {versionColumn} = @{versionAlias} - 1;", result);
		}
	}
}
