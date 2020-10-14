using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static Jeebs.Data.Adapter_Tests.Adapter;

namespace Jeebs.Data.Adapter_Tests
{
	public class JoinColumns_Tests
	{
		[Fact]
		public void No_Columns_Returns_Empty()
		{
			// Arrange
			var adapter = GetAdapter();

			// Act
			var result = adapter.JoinColumns();

			// Assert
			Assert.Equal(string.Empty, result);
		}

		[Fact]
		public void Removes_Empty_Joins_Remaining()
		{
			// Arrange
			var adapter = GetAdapter();
			var input = new string[] { " one ", "  two", "", " ", "three " };

			// Act
			var result = adapter.JoinColumns(input);

			// Assert
			Assert.Equal(" one |   two| three ", result);
		}
	}
}
