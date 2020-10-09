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
			void action() => adapter.DeleteSingle(input, F.Rand.String, F.Rand.String);

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
			void action() => adapter.DeleteSingle(F.Rand.String, input, F.Rand.String);

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
			void action() => adapter.DeleteSingle(F.Rand.String, F.Rand.String, input);

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal($"ID Alias is invalid: '{input}'.", ex.Message);
		}

		[Fact]
		public void Returns_Delete_Query()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var table = F.Rand.String;
			var idColumn = F.Rand.String;
			var idAlias = F.Rand.String;

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
			var table = F.Rand.String;
			var idColumn = F.Rand.String;
			var idAlias = F.Rand.String;
			var versionColumn = F.Rand.String;
			var versionAlias = F.Rand.String;

			// Act
			var result = adapter.DeleteSingle(table, idColumn, idAlias, versionColumn, versionAlias);

			// Assert
			Assert.Equal($"DELETE FROM {table} WHERE {idColumn} = @{idAlias} AND {versionColumn} = @{versionAlias} - 1;", result);
		}
	}
}
