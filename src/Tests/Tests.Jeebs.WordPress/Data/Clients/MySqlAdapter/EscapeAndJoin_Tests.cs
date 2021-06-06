// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.WordPress.Data.Clients.MySql.MySqlAdapter_Tests
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
