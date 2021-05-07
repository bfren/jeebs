// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.WordPress.Data.Query_Tests.Option_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_Properties()
		{
			// Arrange
			var (client, wpDb, schema, table, _) = Query_Setup.Get();

			// Act
			var result = new TestOptions(client, wpDb, table);

			// Assert
			Assert.Same(client, result.ClientTest);
			Assert.Same(wpDb, result.DbTest);
			Assert.Same(schema, result.TTest);
			Assert.Same(table, result.TableTest);
		}
	}
}
