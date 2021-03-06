using System;
using Xunit;

namespace Jeebs.Data.Clients.SqlServer.SqlServerAdapter_Tests
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
			var adapter = new SqlServerAdapter();

			// Act
			void action() => adapter.DeleteSingle(input, F.Rnd.Str, F.Rnd.Str);

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
			var adapter = new SqlServerAdapter();

			// Act
			void action() => adapter.DeleteSingle(F.Rnd.Str, input, F.Rnd.Str);

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
			var adapter = new SqlServerAdapter();

			// Act
			void action() => adapter.DeleteSingle(F.Rnd.Str, F.Rnd.Str, input);

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal($"ID Alias is invalid: '{input}'.", ex.Message);
		}

		[Fact]
		public void Returns_Delete_Query()
		{
			// Arrange
			var adapter = new SqlServerAdapter();
			var table = F.Rnd.Str;
			var idColumn = F.Rnd.Str;
			var idAlias = F.Rnd.Str;

			// Act
			var result = adapter.DeleteSingle(table, idColumn, idAlias);

			// Assert
			Assert.Equal($"DELETE FROM {table} WHERE {idColumn} = @{idAlias};", result);
		}

		[Fact]
		public void Returns_Delete_Query_With_Version()
		{
			// Arrange
			var adapter = new SqlServerAdapter();
			var table = F.Rnd.Str;
			var idColumn = F.Rnd.Str;
			var idAlias = F.Rnd.Str;
			var versionColumn = F.Rnd.Str;
			var versionAlias = F.Rnd.Str;

			// Act
			var result = adapter.DeleteSingle(table, idColumn, idAlias, versionColumn, versionAlias);

			// Assert
			Assert.Equal($"DELETE FROM {table} WHERE {idColumn} = @{idAlias} AND {versionColumn} = @{versionAlias} - 1;", result);
		}
	}
}
