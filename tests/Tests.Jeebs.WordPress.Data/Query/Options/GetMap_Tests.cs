﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.WordPress.Data.Query_Tests.Options_Tests
{
	public class GetMap_Tests
	{
		[Fact]
		public void Returns_Table_And_IdColumn()
		{
			// Arrange
			var (_, _, _, table, options) = Options_Setup.Get();

			// Act
			var result = options.GetMapTest();

			// Assert
			var some = result.AssertSome();
			Assert.Same(table, some.table);
			Assert.Equal(table.GetName(), some.idColumn.Table);
			Assert.Equal(table.Id, some.idColumn.Name);
			Assert.Equal(nameof(TestTable.Id), some.idColumn.Alias);
		}
	}
}