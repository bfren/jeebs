using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs
{
	public sealed class PagedListTests
	{
		private PagedList<Foo> GetEmptyList(double currentPage, double totalItems = 25, double itemsPerPage = 10, double numberOfPagesPerGroup = 10)
		{
			return new PagedList<Foo>(currentPage, totalItems, itemsPerPage, numberOfPagesPerGroup);
		}

		private PagedList<Foo> GetFilledList(double currentPage, double totalItems = 25, double itemsPerPage = 10, double numberOfPagesPerGroup = 10)
		{
			var list = new List<Foo>();
			for (int i = 1; i <= totalItems; i++)
			{
				list.Add(new Foo { Bar = i });
			}

			return new PagedList<Foo>(list, currentPage)
			{
				ItemsPerPage = itemsPerPage,
				NumberOfPagesPerGroup = numberOfPagesPerGroup
			};
		}

		[Fact]
		public void FilledListTotalItems_Tests()
		{
			// Arrange
			var filledList = GetFilledList(1);

			// Act

			// Assert
			Assert.Equal(filledList.Count, filledList.TotalItems);
		}

		[Fact]
		public void NoPageOrTotal_Tests()
		{
			// Arrange
			var emptyListWithoutPage = GetEmptyList(0);
			var emptyListWithoutTotal = GetEmptyList(1, 0);
			var filledListWithoutPage = GetFilledList(0);
			var filledListWithoutItems = GetFilledList(1, 0);
			var emptyListWithInvalidPage = GetEmptyList(-1);

			// Act
			emptyListWithoutPage.Calculate();
			emptyListWithoutTotal.Calculate();
			filledListWithoutPage.Calculate();
			filledListWithoutItems.Calculate();
			Action invalidPage = () => emptyListWithInvalidPage.Calculate();

			// Assert
			Assert.Equal(1, emptyListWithoutPage.CurrentPage);
			Assert.Equal(1, emptyListWithoutTotal.Pages);
			Assert.Equal(1, filledListWithoutPage.CurrentPage);
			Assert.Equal(1, filledListWithoutItems.Pages);
			Assert.Throws<InvalidOperationException>(invalidPage);
		}

		[Fact]
		public void CurrentPageGreaterThanPages_Tests()
		{
			// Arrange
			var emptyList = GetEmptyList(20);
			var filledList = GetFilledList(20);

			// Act
			emptyList.Calculate();
			filledList.Calculate();

			// Assert
			Assert.Equal(3, emptyList.Pages);
			Assert.Equal(3, filledList.Pages);
		}

		[Fact]
		public void FirstAndLastItem_Tests()
		{
			// Arrange
			var listPage1 = GetEmptyList(1);
			var listPage2 = GetEmptyList(2);
			var listPage3 = GetEmptyList(3);
			var listPage4 = GetEmptyList(4);

			// Act
			listPage1.Calculate();
			listPage2.Calculate();
			listPage3.Calculate();
			listPage4.Calculate();

			// Assert
			Assert.Equal(1, listPage1.FirstItem);
			Assert.Equal(10, listPage1.LastItem);
			Assert.Equal(11, listPage2.FirstItem);
			Assert.Equal(20, listPage2.LastItem);
			Assert.Equal(21, listPage3.FirstItem);
			Assert.Equal(25, listPage3.LastItem);
			Assert.Equal(21, listPage4.FirstItem);
			Assert.Equal(25, listPage4.LastItem);
		}

		[Fact]
		public void LowerAndUpperPage_Tests()
		{
			// Arrange
			var listWithoutPagesOfPages = GetEmptyList(2);
			var listOnFirstPageOfPages = GetEmptyList(4, 478);
			var listOnSecondPageOfPages = GetEmptyList(17, 478);
			var listOnLastPageOfPages = GetEmptyList(45, 478);

			// Act
			listWithoutPagesOfPages.Calculate();
			listOnFirstPageOfPages.Calculate();
			listOnSecondPageOfPages.Calculate();
			listOnLastPageOfPages.Calculate();

			// Assert
			Assert.Equal(1, listWithoutPagesOfPages.LowerPage);
			Assert.Equal(3, listWithoutPagesOfPages.UpperPage);
			Assert.Equal(1, listOnFirstPageOfPages.LowerPage);
			Assert.Equal(10, listOnFirstPageOfPages.UpperPage);
			Assert.Equal(11, listOnSecondPageOfPages.LowerPage);
			Assert.Equal(20, listOnSecondPageOfPages.UpperPage);
			Assert.Equal(41, listOnLastPageOfPages.LowerPage);
			Assert.Equal(48, listOnLastPageOfPages.UpperPage);
		}

		[Fact]
		public void CalculateAndApply_Tests()
		{
			// Arrange
			var listSecondPage = GetFilledList(2);
			var listThirdPage = GetFilledList(3);

			// Act
			listSecondPage.CalculateAndApply();
			listThirdPage.CalculateAndApply();

			// Assert
			Assert.Equal(10, listSecondPage.Count);
			Assert.Equal(11, listSecondPage.First().Bar);
			Assert.Equal(20, listSecondPage.Last().Bar);
			Assert.Equal(5, listThirdPage.Count);
			Assert.Equal(21, listThirdPage.First().Bar);
			Assert.Equal(25, listThirdPage.Last().Bar);
		}

		public sealed class Foo
		{
			public int Bar { get; set; }
		}
	}
}
