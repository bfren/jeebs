// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using Xunit;
using static F.WordPressF.DataF.QueryPostsF;

namespace F.WordPressF.DataF.QueryPostsF_Tests
{
	public class GetPreviousAndNext_Tests
	{
		[Fact]
		public void Empty_List_Returns_None_With_ListIsEmptyMsg()
		{
			// Arrange
			var list = new List<ulong>();

			// Act
			var (r0, r1) = GetPreviousAndNext(Rnd.Ulng, list);

			// Assert
			Assert.Null(r0);
			Assert.Null(r1);
		}

		[Fact]
		public void Single_Item_Returns_None_With_ListContainsSingleItemMsg()
		{
			// Arrange
			var list = new List<ulong> { Rnd.Ulng };

			// Act
			var (r0, r1) = GetPreviousAndNext(Rnd.Ulng, list);

			// Assert
			Assert.Null(r0);
			Assert.Null(r1);
		}

		[Fact]
		public void Item_Not_In_List_Returns_None_With_ListDoesNotContainItemMsg()
		{
			// Arrange
			var value = 10001U;
			var list = new List<ulong> { Rnd.Ulng, Rnd.Ulng, Rnd.Ulng };

			// Act
			var (r0, r1) = GetPreviousAndNext(value, list);

			// Assert
			Assert.Null(r0);
			Assert.Null(r1);
		}

		[Fact]
		public void First_Item_Returns_None_And_Next_Item()
		{
			// Arrange
			var value = Rnd.Ulng;
			var next = Rnd.Ulng;
			var list = new List<ulong> { value, next, Rnd.Ulng, Rnd.Ulng };

			// Act
			var (r0, r1) = GetPreviousAndNext(value, list);

			// Assert
			Assert.Null(r0);
			Assert.NotNull(r1);
			Assert.Equal(next, r1!.Value);
		}

		[Fact]
		public void Last_Item_Returns_Previous_Item_And_None()
		{
			// Arrange
			var value = Rnd.Ulng;
			var prev = Rnd.Ulng;
			var list = new List<ulong> { Rnd.Ulng, Rnd.Ulng, prev, value };

			// Act
			var (r0, r1) = GetPreviousAndNext(value, list);

			// Assert
			Assert.NotNull(r0);
			Assert.Equal(prev, r0!.Value);
			Assert.Null(r1);
		}

		[Fact]
		public void Returns_Previous_And_Next_Items()
		{
			// Arrange
			var value = Rnd.Ulng;
			var prev = Rnd.Ulng;
			var next = Rnd.Ulng;
			var list = new List<ulong> { Rnd.Ulng, Rnd.Ulng, prev, value, next, Rnd.Ulng, Rnd.Ulng };

			// Act
			var (r0, r1) = GetPreviousAndNext(value, list);

			// Assert
			Assert.NotNull(r0);
			Assert.Equal(prev, r0!.Value);
			Assert.NotNull(r1);
			Assert.Equal(next, r1!.Value);
		}
	}
}
