// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public class AddSelect_Tests
	{
		[Fact]
		public void Returns_New_Parts_With_Select()
		{
			// Arrange
			var (_, _, parts, options) = QueryOptions_Setup.Get();
			var cols = Substitute.For<IColumnList>();

			// Act
			var result = options.AddSelectTest(parts, cols);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(parts, some);
			Assert.Same(cols, some.Select);
		}
	}
}
