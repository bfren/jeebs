using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs
{
	public sealed class PagedListTests
	{
		[Fact]
		public void CalculateAndApply_Empty_ReturnsEmpty()
		{
			// Arrange
			var list = new PagedList<string>(new string[] { });

			// Act
			(var newList, var _) = list.CalculateAndApply(1);

			// Assert
			Assert.Empty(newList);
		}

		[Fact]
		public void CalculateAndApply_ReturnsNewList()
		{
			// Arrange
			var list = new PagedList<string>(new string[] { });

			// Act
			(var newList, var _) = list.CalculateAndApply(1);

			// Assert
			Assert.NotEqual(newList.GetHashCode(), list.GetHashCode());
		}

		[Fact]
		public void CalculateAndApply_ReturnsSubsection()
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
