// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
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
			void action() => adapter.DeleteSingle(input, JeebsF.Rnd.Str, JeebsF.Rnd.Str);

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
			void action() => adapter.DeleteSingle(JeebsF.Rnd.Str, input, JeebsF.Rnd.Str);

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
			void action() => adapter.DeleteSingle(JeebsF.Rnd.Str, JeebsF.Rnd.Str, input);

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal($"ID Alias is invalid: '{input}'.", ex.Message);
		}

		[Fact]
		public void Returns_Delete_Query()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var table = JeebsF.Rnd.Str;
			var idColumn = JeebsF.Rnd.Str;
			var idAlias = JeebsF.Rnd.Str;

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
			var table = JeebsF.Rnd.Str;
			var idColumn = JeebsF.Rnd.Str;
			var idAlias = JeebsF.Rnd.Str;
			var versionColumn = JeebsF.Rnd.Str;
			var versionAlias = JeebsF.Rnd.Str;

			// Act
			var result = adapter.DeleteSingle(table, idColumn, idAlias, versionColumn, versionAlias);

			// Assert
			Assert.Equal($"DELETE FROM {table} WHERE {idColumn} = @{idAlias} AND {versionColumn} = @{versionAlias} - 1;", result);
		}
	}
}
