using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Data.Clients.MySql.MySqlAdapter_Tests
{
	public class EscapeAndJoin_Tests
	{
		[Theory]
		[InlineData(new[] { "foo" }, "`foo`")]
		[InlineData(new[] { "foo", "bar" }, "`foo`.`bar`")]
		[InlineData(new[] { "foo", "", null, "   ", "bar", "" }, "`foo`.`bar`")]
		public void Returns_Escaped_And_Joined(string[] input, string expected)
		{
			// Arrange
			var adapter = new MySqlAdapter();

			// Act
			var result = adapter.EscapeAndJoin(input);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
