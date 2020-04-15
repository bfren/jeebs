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
		public void ApplyValues_Empty_ReturnsEmpty()
		{
			// Arrange
			var list = new PagedList<string>(new string[] { }, 0);

			// Act
			var newList = list.ApplyValues();

			// Assert
			Assert.Empty(newList);
		}

		[Fact]
		public void ApplyValues_ReturnsNewList()
		{
			// Arrange
			var list = new PagedList<string>(new string[] { }, 0);

			// Act
			var newList = list.ApplyValues();

			// Assert
			Assert.NotEqual(newList.GetHashCode(), list.GetHashCode());
		}

		[Fact]
		public void ApplyValues_ReturnsSubsection()
		{
			// Arrange
			var items = new List<string>();
			for (int i = 1; i <= 25; i++)
			{
				items.Add($"Item {i}");
			}
			var list = new PagedList<string>(items, 3, 5);

			// Act
			var newList = list.ApplyValues();

			// Assert
			Assert.Equal(1, newList.Values.CurrentPage);
			Assert.Equal(1, newList.Values.Pages);
			Assert.Equal(5, newList.Values.Items);
			Assert.Equal("Item 11", newList[0]);
			Assert.Equal("Item 15", newList[4]);
		}
	}
}
