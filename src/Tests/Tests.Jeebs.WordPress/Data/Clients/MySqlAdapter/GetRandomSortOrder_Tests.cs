// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
