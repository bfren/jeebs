// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;
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
			var (table, _, _, _, options) = QueryOptions_Setup.Get();
			var cols = Substitute.For<IColumnList>();

			// Act
			var result = options.CreatePartsTest(table, cols);

			// Assert
			var some = result.AssertSome();
			Assert.Same(table, some.From);
		}
	}
}
