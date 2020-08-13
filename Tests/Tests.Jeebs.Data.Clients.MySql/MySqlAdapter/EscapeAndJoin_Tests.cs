using System;
using Xunit;

namespace Jeebs.Data.Clients.MySql
{
	public partial class MySqlAdapter_Tests
	{
		[Theory]
		[InlineData(new[] { "foo" }, "`foo`")]
		[InlineData(new[] { "foo", "bar" }, "`foo`.`bar`")]
		[InlineData(new[] { "foo", "", null, "   ", "bar", "" }, "`foo`.`bar`")]
		public void EscapeAndJoin_Name_ReturnsEscapedAndJoinedName(string[] input, string expected)
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
