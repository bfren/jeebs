// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.WordPress.Data.Clients.MySql.MySqlAdapter_Tests
{
	public class GetRandomSortOrder_Tests
	{
		[Fact]
		public void Returns_Rand()
		{
			// Arrange
			var adapter = new MySqlAdapter();

			// Act
			var result = adapter.GetRandomSortOrder();

			// Assert
			Assert.Equal("RAND()", result);
		}
	}
}
