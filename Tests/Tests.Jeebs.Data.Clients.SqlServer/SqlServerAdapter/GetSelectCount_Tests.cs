// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Data.Clients.SqlServer.SqlServerAdapter_Tests
{
	public class GetSelectCount_Tests
	{
		[Fact]
		public void Returns_Select_Count()
		{
			// Arrange
			var adapter = new SqlServerAdapter();

			// Act
			var result = adapter.GetSelectCount();

			// Assert
			Assert.Equal("COUNT(*)", result);
		}
	}
}
