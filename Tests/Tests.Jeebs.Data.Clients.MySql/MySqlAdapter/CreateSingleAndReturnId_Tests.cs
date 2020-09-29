using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
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
			Action action = () => adapter.CreateSingleAndReturnId(input, Arg.Any<List<string>>(), Arg.Any<List<string>>());

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal($"Table is invalid: '{input}'.", ex.Message);
		}

		[Fact]
		public void Empty_Columns_Throws_InvalidOperationException()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var table = "one";
			var columns = new List<string>();

			// Act
			Action action = () => adapter.CreateSingleAndReturnId(table, columns, Arg.Any<List<string>>());

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal("The list of columns cannot be empty.", ex.Message);
		}

		[Fact]
		public void Empty_Aliases_Throws_InvalidOperationException()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var table = "one";
			var columns = new List<string> { "two" };
			var aliases = new List<string>();

			// Act
			Action action = () => adapter.CreateSingleAndReturnId(table, columns, aliases);

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal("The list of aliases cannot be empty.", ex.Message);
		}

		[Fact]
		public void Columns_And_Aliases_Different_Lengths_Throws_InvalidOperationException()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var table = "one";
			var columns = new List<string> { "two" };
			var aliases = new List<string> { "three", "four" };

			// Act
			Action action = () => adapter.CreateSingleAndReturnId(table, columns, aliases);

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal("The number of columns (1) and aliases (2) must be the same.", ex.Message);
		}

		[Fact]
		public void	Returns_Insert_Query()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var table = "one";
			var columns = new List<string> { "two", "three" };
			var aliases = new List<string> { "four", "five" };

			// Act
			var result = adapter.CreateSingleAndReturnId(table, columns, aliases);

			// Assert
			Assert.Equal("INSERT INTO one (two, three) VALUES (@four, @five); SELECT LAST_INSERT_ID();", result);
		}
	}
}
