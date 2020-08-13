using System;
using Xunit;

namespace Jeebs.Data.Clients.MySql
{
	public partial class MySqlAdapter_Tests
	{
		[Theory]
		[InlineData("foo", "`foo`")]
		[InlineData("`foo`", "`foo`")]
		[InlineData("foo.bar", "`foo`.`bar`")]
		[InlineData("foo..bar", "`foo`.`bar`")]
		[InlineData("foo.   .bar", "`foo`.`bar`")]
		[InlineData("foo.bar.foo.bar", "`foo`.`bar`.`foo`.`bar`")]
		public void SplitAndEscape_Name_ReturnsSplitAndEscapedName(string input, string expected)
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
