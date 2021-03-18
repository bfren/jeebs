// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Data.Clients.SqlServer.SqlServerAdapter_Tests
{
	public class Escape_Tests
	{
		[Theory]
		[InlineData("foo", "[foo]")]
		[InlineData("foo.bar", "[foo].[bar]")]
		public void Returns_Escaped(string input, string expected)
		{
			// Arrange
			var adapter = new SqlServerAdapter();

			// Act
			var result = adapter.Escape(input);

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void Table_Returns_ToString_Escaped()
		{
			// Arrange
			var adapter = new SqlServerAdapter();
			var table = F.Rnd.Int;

			// Act
			var result = adapter.EscapeTable(table);

			// Assert
			Assert.Equal($"[{table}]", result);
		}
	}
}
