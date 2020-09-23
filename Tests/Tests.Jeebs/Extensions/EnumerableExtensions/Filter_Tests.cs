using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.EnumerableExtensions_Tests
{
	public class Filter_Tests
	{
		[Fact]
		public void Empty_Returns_Empty()
		{
			// Arrange
			var empty = new object[] { };

			// Act
			var result = empty.Filter();

			// Assert
			Assert.Empty(result);
		}

		[Fact]
		public void Removes_Empty_Items()
		{
			// Arrange
			var one = 1;
			var two = "two";
			var list = new object?[] { null, one, null, two, null };

			// Act
			var result = list.Filter();

			// Assert
			Assert.Equal(2, result.Count());
			Assert.Collection(result,
				x => Assert.Equal(one, x),
				x => Assert.Equal(two, x)
			);
		}

		[Fact]
		public void Maps_Class_And_Removes_Empty_Items()
		{
			// Arrange
			var one = "1";
			var two = "2";
			var list = new object?[] { null, one, null, two, null };
			static int? parse(object? x)
			{
				if (x is string y)
				{
					return int.Parse(y);
				}

				return null;
			}

			// Act
			var result = list.Filter(parse);

			// Assert
			Assert.Equal(2, result.Count());
			Assert.Collection(result,
				x => Assert.Equal(1, x),
				x => Assert.Equal(2, x)
			);
		}

		[Fact]
		public void Maps_Struct_And_Removes_Empty_Items()
		{
			// Arrange
			var one = 1;
			var two = 2;
			var list = new int?[] { null, one, null, two, null };
			static string? parse(int? x) => x?.ToString();

			// Act
			var result = list.Filter(parse);

			// Assert
			Assert.Equal(2, result.Count());
			Assert.Collection(result,
				x => Assert.Equal(one.ToString(), x),
				x => Assert.Equal(two.ToString(), x)
			);
		}
	}
}
