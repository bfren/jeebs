using System;
using System.Collections.Generic;
using System.Text;
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
