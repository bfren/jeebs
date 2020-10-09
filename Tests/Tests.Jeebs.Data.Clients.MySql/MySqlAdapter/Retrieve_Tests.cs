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
			void action() => adapter.Retrieve(parts);

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal($"Table is invalid: '{input}'.", ex.Message);
		}

		[Fact]
		public void No_Select_Selects_All()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var parts = Substitute.For<IQueryParts>();
			var from = F.Rand.String;
			parts.From.Returns(from);

			// Act
			var result = adapter.Retrieve(parts);

			// Assert
			Assert.Equal($"SELECT * FROM {from};", result);
		}

		[Theory]
		[InlineData("two, three")]
		[InlineData("`two`,`three`")]
		public void Selects_Columns(string input)
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var parts = Substitute.For<IQueryParts>();
			var from = F.Rand.String;
			parts.From.Returns(from);
			parts.Select.Returns(input);

			// Act
			var result = adapter.Retrieve(parts);

			// Assert
			Assert.Equal($"SELECT {input} FROM {from};", result);
		}
	}
}
