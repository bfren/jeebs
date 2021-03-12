// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq;
using Xunit;
using static JeebsF.OptionF;

namespace Jeebs.EnumerableExtensions_Tests
{
	public class Filter_Tests
	{
		[Fact]
		public void Empty_Returns_Empty()
		{
			// Arrange
			var empty = Array.Empty<object>();

			// Act
			var result = empty.Filter();

			// Assert
			Assert.Empty(result);
		}

		[Fact]
		public void Removes_Empty_Items()
		{
			// Arrange
			var one = JeebsF.Rnd.Int;
			var two = JeebsF.Rnd.Str;
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
		public void Removes_None()
		{
			// Arrange
			var n0 = JeebsF.Rnd.Int;
			var n1 = JeebsF.Rnd.Int;
			var list = new[] { None<int>(true), Return(n0), None<int>(true), Return(n1), None<int>(true) };

			// Act
			var result = list.Filter();

			// Assert
			Assert.Collection(result,
				x => Assert.Equal(n0, Assert.IsType<Some<int>>(x).Value),
				x => Assert.Equal(n1, Assert.IsType<Some<int>>(x).Value)
			);
		}

		[Fact]
		public void Maps_Class_And_Removes_Empty_Items()
		{
			// Arrange
			var n0 = JeebsF.Rnd.Int;
			var n1 = JeebsF.Rnd.Int;
			var list = new object?[] { null, n0.ToString(), null, n1.ToString(), null };
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
				x => Assert.Equal(n0, x),
				x => Assert.Equal(n1, x)
			);
		}

		[Fact]
		public void Maps_Struct_And_Removes_Empty_Items()
		{
			// Arrange
			var n0 = JeebsF.Rnd.Int;
			var n1 = JeebsF.Rnd.Int;
			var list = new int?[] { null, n0, null, n1, null };
			static string? parse(int? x) => x?.ToString();

			// Act
			var result = list.Filter(parse);

			// Assert
			Assert.Equal(2, result.Count());
			Assert.Collection(result,
				x => Assert.Equal(n0.ToString(), x),
				x => Assert.Equal(n1.ToString(), x)
			);
		}
	}
}
