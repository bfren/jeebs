// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.WordPress.Data.Clients.MySql.MySqlAdapter_Tests
{
	public class SplitAndEscape_Tests
	{
		[Theory]
		[InlineData("foo", "`foo`")]
		[InlineData("foo.bar", "`foo`.`bar`")]
		[InlineData("foo..bar", "`foo`.`bar`")]
		[InlineData("foo.   .bar", "`foo`.`bar`")]
		[InlineData("foo.bar.foo.bar", "`foo`.`bar`.`foo`.`bar`")]
		public void Splits_Escapes_And_Rejoins(string input, string expected)
		{
			// Arrange
			var adapter = new MySqlAdapter();

			// Act
			var result = adapter.SplitAndEscape(input);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
