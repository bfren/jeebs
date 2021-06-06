// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using Xunit;

namespace Jeebs.WordPress.Data.Clients.MySql.MySqlAdapter_Tests
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
			var table = F.Rnd.Str;
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
			var table = F.Rnd.Str;
			var columns = new List<string> { F.Rnd.Str };
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
			var table = F.Rnd.Str;
			var columns = new List<string> { F.Rnd.Str };
			var aliases = new List<string> { F.Rnd.Str, F.Rnd.Str };

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
			var table = F.Rnd.Str;

			var c0 = F.Rnd.Str;
			var c1 = F.Rnd.Str;
			var columns = new List<string> { c0, c1 };

			var a0 = F.Rnd.Str;
			var a1 = F.Rnd.Str;
			var aliases = new List<string> { a0, a1 };

			// Act
			var result = adapter.CreateSingleAndReturnId(table, columns, aliases);

			// Assert
			Assert.Equal($"INSERT INTO {table} ({c0}, {c1}) VALUES (@{a0}, @{a1}); SELECT LAST_INSERT_ID();", result);
		}
	}
}
