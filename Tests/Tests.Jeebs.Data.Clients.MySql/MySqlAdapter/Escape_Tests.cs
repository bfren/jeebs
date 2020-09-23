using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Data.Clients.MySql.MySqlAdapter_Tests
{
	public class Escape_Tests
	{
		[Theory]
		[InlineData("foo", "`foo`")]
		[InlineData("`foo`", "`foo`")]
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
	}
}
