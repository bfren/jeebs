// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.WordPress.Data.Clients.MySql.MySqlAdapter_Tests
{
	public class Escape_Tests
	{
		[Theory]
		[InlineData("foo", "`foo`")]
		[InlineData("foo.bar", "`foo`.`bar`")]
		public void Returns_Escaped(string input, string expected)
		{
			// Arrange
			var adapter = new MySqlAdapter();

			// Act
			var result = adapter.Escape(input);

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void Table_Returns_ToString_Escaped()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			var table = F.Rnd.Int;

			// Act
			var result = adapter.EscapeTable(table);

			// Assert
			Assert.Equal($"`{table}`", result);
		}
	}
}
