// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public class CreateParts_Tests
	{
		[Fact]
		public void Returns_New_QueryParts_With_Table()
		{
			// Arrange
			var (table, map, _, options) = QueryOptions_Setup.Get();

			// Act
			var result = options.CreatePartsTest(map);

			// Assert
			var some = result.AssertSome();
			Assert.Same(table, some.From);
		}
	}
}
