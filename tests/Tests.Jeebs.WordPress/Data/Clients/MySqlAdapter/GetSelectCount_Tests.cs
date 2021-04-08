// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.WordPress.Data.Clients.MySql.MySqlAdapter_Tests
{
	public class GetSelectCount_Tests
	{
		[Fact]
		public void Returns_Select_Count()
		{
			// Arrange
			var adapter = new MySqlAdapter();

			// Act
			var result = adapter.GetSelectCount();

			// Assert
			Assert.Equal("COUNT(*)", result);
		}
	}
}
