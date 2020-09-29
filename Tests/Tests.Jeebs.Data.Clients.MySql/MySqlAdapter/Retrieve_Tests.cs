using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Clients.MySql.MySqlAdapter_Tests
{
	public class Retrieve_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void Invalid_Table_Throws_InvalidOperationException(string input)
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var parts = Substitute.For<IQueryParts>();
			parts.From.Returns(input);

			// Act
			Action action = () => adapter.Retrieve(parts);

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal($"FROM table is invalid: '{input}'.", ex.Message);
		}

		[Fact]
		public void No_Select_Selects_All()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var parts = Substitute.For<IQueryParts>();
			parts.From.Returns("one");
			
			// Act
			var result = adapter.Retrieve(parts);

			// Assert
			Assert.Equal("SELECT * FROM one;", result);
		}

		[Theory]
		[InlineData("two, three")]
		[InlineData("`two`,`three`")]
		public void Selects_Columns(string input)
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var parts = Substitute.For<IQueryParts>();
			parts.From.Returns("one");
			parts.Select.Returns(input);

			// Act
			var result = adapter.Retrieve(parts);

			// Assert
			Assert.Equal($"SELECT {input} FROM one;", result);
		}
	}
}
