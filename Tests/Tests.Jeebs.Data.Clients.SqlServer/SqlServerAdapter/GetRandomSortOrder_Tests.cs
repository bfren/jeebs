// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Data.Clients.SqlServer.SqlServerAdapter_Tests
{
	public class GetRandomSortOrder_Tests
	{
		[Fact]
		public void Returns_Rand()
		{
			// Arrange
			var adapter = new SqlServerAdapter();

			// Act
			var result = adapter.GetRandomSortOrder();

			// Assert
			Assert.Equal("NEWID()", result);
		}
	}
}
