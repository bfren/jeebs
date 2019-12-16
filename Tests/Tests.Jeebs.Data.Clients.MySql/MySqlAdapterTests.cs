using System;
using Xunit;

namespace Jeebs.Data.Clients.MySql.Tests
{
	public class MySqlAdapterTests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void Escape_NullOrWhitespace_ThrowsArgumentNullException(string input)
		{
			// Arrange
			var adapter = new MySqlAdapter();

			// Act
			Action result = () => adapter.Escape(input);

			// Assert
			Assert.Throws<ArgumentNullException>(result);
		}

		[Theory]
		[InlineData("foo", "`foo`")]
		[InlineData("`foo`", "`foo`")]
		[InlineData("foo.bar", "`foo`.`bar`")]
		public void Escape_Name_ReturnsEscapedName(string input, string expected)
		{
			// Arrange
			var adapter = new MySqlAdapter();

			// Act
			var result = adapter.Escape(input);

			// Assert
			Assert.Equal(expected, result);
		}

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
			var result = adapter.Escape(input);

			// Assert
			Assert.Equal(expected, result);
		}

	}
}
