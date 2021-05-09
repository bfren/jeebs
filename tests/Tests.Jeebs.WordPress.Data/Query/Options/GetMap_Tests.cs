// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.WordPress.Data.Query_Tests.Options_Tests
{
	public class GetMap_Tests : Options_Tests
	{
		[Fact]
		public void Returns_Table_And_IdColumn()
		{
			// Arrange
			var (options, v) = Setup();

			// Act
			var result = options.GetMapTest();

			// Assert
			var (table, idColumn) = result.AssertSome();
			Assert.Same(v.Table, table);
			Assert.Equal(v.Table.GetName(), idColumn.Table);
			Assert.Equal(v.Table.Id, idColumn.Name);
			Assert.Equal(nameof(TestTable.Id), idColumn.Alias);
		}
	}
}
