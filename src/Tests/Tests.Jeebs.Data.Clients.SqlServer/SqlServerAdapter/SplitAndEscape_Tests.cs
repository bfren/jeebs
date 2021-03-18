// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Data.Clients.SqlServer.SqlServerAdapter_Tests
{
	public class SplitAndEscape_Tests
	{
		[Theory]
		[InlineData("foo", "[foo]")]
		[InlineData("foo.bar", "[foo].[bar]")]
		[InlineData("foo..bar", "[foo].[bar]")]
		[InlineData("foo.   .bar", "[foo].[bar]")]
		[InlineData("foo.bar.foo.bar", "[foo].[bar].[foo].[bar]")]
		public void Splits_Escapes_And_Rejoins(string input, string expected)
		{
			// Arrange
			var adapter = new SqlServerAdapter();

			// Act
			var result = adapter.SplitAndEscape(input);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
