using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.PagedList_Tests
{
	public class CalculateAndApply_Tests
	{
		[Fact]
		public void Empty_ReturnsEmpty()
		{
			// Arrange
			var list = new PagedList<string>(new string[] { });

			// Act
			(var newList, var _) = list.CalculateAndApply(1);

			// Assert
			Assert.Empty(newList);
		}

		[Fact]
		public void ReturnsNewList()
		{
			// Arrange
			var list = new PagedList<string>(new string[] { });

			// Act
			(var newList, var _) = list.CalculateAndApply(1);

			// Assert
			Assert.NotEqual(newList.GetHashCode(), list.GetHashCode());
		}

		[Fact]
		public void ReturnsSubsection()
		{
			// Arrange
			var items = new List<string>();
			for (int i = 1; i <= 25; i++)
			{
				items.Add($"Item {i}");
			}
			var list = new PagedList<string>(items);

			// Act
			(var newList, var values) = list.CalculateAndApply(3, 5);

			// Assert
			Assert.Equal(1, values.Page);
			Assert.Equal(1, values.Pages);
			Assert.Equal(5, values.Items);
			Assert.Equal("Item 11", newList[0]);
			Assert.Equal("Item 15", newList[4]);
		}
	}
}
