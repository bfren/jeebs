// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public class GetMap_Tests
	{
		[Fact]
		public void Returns_Entity_Table_And_IdColumn()
		{
			// Arrange
			var (table, map, idColumn, _, options) = QueryOptions_Setup.Get();

			// Act
			var result = options.GetMapTest();

			// Assert
			var some = result.AssertSome();
			Assert.Same(table, some.table);
			Assert.Same(idColumn, some.idColumn);
		}
	}
}
