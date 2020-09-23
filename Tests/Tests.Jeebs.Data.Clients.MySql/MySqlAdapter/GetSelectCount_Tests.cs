using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Data.Clients.MySql.MySqlAdapter_Tests
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
