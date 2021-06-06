// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
