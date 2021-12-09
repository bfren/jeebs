// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using Xunit;

namespace Jeebs.EnumerableExtensions_Tests;

public class Filter_Tests
{
	[Fact]
	public void Maps_Class_And_Removes_Empty_Items()
	{
		// Arrange
		var n0 = F.Rnd.Int;
		var n1 = F.Rnd.Int;
		var list = new object?[] { null, n0.ToString(), null, n1.ToString(), null };
		var parse = int? (object? x) =>
		{
			if (x is string y)
			{
				return int.Parse(y);
			}

			return null;
		};

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
		var n0 = F.Rnd.Int;
		var n1 = F.Rnd.Int;
		var list = new int?[] { null, n0, null, n1, null };
		var parse = string? (int? x) => x?.ToString();

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
