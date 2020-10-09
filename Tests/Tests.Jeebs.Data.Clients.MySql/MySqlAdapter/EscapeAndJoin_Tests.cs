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
		[InlineData(new object?[] { "foo", 5, "", null, "   ", "bar", "" }, "`foo`.`5`.`bar`")]
		public void Removes_Invalid_Returns_Escaped_And_Joined(object?[] input, string expected)
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
