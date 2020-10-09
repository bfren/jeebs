using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Org.BouncyCastle.Pkix;
using Xunit;

namespace Jeebs.Data.Clients.MySql.MySqlAdapter_Tests
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
			var table = F.Rand.Integer;

			// Act
			var result = adapter.Escape(table);

			// Assert
			Assert.Equal($"`{table}`", result);
		}
	}
}
