// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using Xunit;

namespace Jeebs.Data.Clients.MySql.MySqlAdapter_Tests
{
	public class CreateSingleAndReturnId_Tests
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
			void action() => adapter.CreateSingleAndReturnId(input, new List<string>(), new List<string>());

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal($"Table is invalid: '{input}'.", ex.Message);
		}

		[Fact]
		public void Empty_Columns_Throws_InvalidOperationException()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var table = JeebsF.Rnd.Str;
			var columns = new List<string>();

			// Act
			void action() => adapter.CreateSingleAndReturnId(table, columns, new List<string>());

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal("The list of columns cannot be empty.", ex.Message);
		}

		[Fact]
		public void Empty_Aliases_Throws_InvalidOperationException()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var table = JeebsF.Rnd.Str;
			var columns = new List<string> { JeebsF.Rnd.Str };
			var aliases = new List<string>();

			// Act
			void action() => adapter.CreateSingleAndReturnId(table, columns, aliases);

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal("The list of aliases cannot be empty.", ex.Message);
		}

		[Fact]
		public void Columns_And_Aliases_Different_Lengths_Throws_InvalidOperationException()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var table = JeebsF.Rnd.Str;
			var columns = new List<string> { JeebsF.Rnd.Str };
			var aliases = new List<string> { JeebsF.Rnd.Str, JeebsF.Rnd.Str };

			// Act
			void action() => adapter.CreateSingleAndReturnId(table, columns, aliases);

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal("The number of columns (1) and aliases (2) must be the same.", ex.Message);
		}

		[Fact]
		public void Returns_Insert_Query()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var table = JeebsF.Rnd.Str;

			var c0 = JeebsF.Rnd.Str;
			var c1 = JeebsF.Rnd.Str;
			var columns = new List<string> { c0, c1 };

			var a0 = JeebsF.Rnd.Str;
			var a1 = JeebsF.Rnd.Str;
			var aliases = new List<string> { a0, a1 };

			// Act
			var result = adapter.CreateSingleAndReturnId(table, columns, aliases);

			// Assert
			Assert.Equal($"INSERT INTO {table} ({c0}, {c1}) VALUES (@{a0}, @{a1}); SELECT LAST_INSERT_ID();", result);
		}
	}
}
