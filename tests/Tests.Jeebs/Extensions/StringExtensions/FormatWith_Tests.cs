// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.StringExtensions_Tests;

public class FormatWith_Tests
{
	[Fact]
	public void Replace_Ordered_Numbered_Values()
	{
		// Arrange
		const string format = "{0} , {1} , {2}";
		var n0 = F.Rnd.Int;
		var n1 = F.Rnd.Int;
		var n2 = F.Rnd.Int;
		var values = new[] { n0, n1, n2 };

		// Act
		var result = format.FormatWith(values);

		// Assert
		Assert.Equal($"{n0} , {n1} , {n2}", result);
	}

	[Fact]
	public void Replace_Unordered_Numbered_Values()
	{
		// Arrange
		const string format = "{1} , {0} , {2}";
		var n0 = F.Rnd.Int;
		var n1 = F.Rnd.Int;
		var n2 = F.Rnd.Int;
		var values = new[] { n0, n1, n2 };

		// Act
		var result = format.FormatWith(values);

		// Assert
		Assert.Equal($"{n0} , {n1} , {n2}", result);
	}

	[Fact]
	public void Replace_Named_Values()
	{
		// Arrange
		const string format = "{zero} , {@one} , {two}";
		var n0 = F.Rnd.Int;
		var n1 = F.Rnd.Int;
		var n2 = F.Rnd.Int;
		var values = new { zero = n0, one = n1, two = n2 };

		// Act
		var result = format.FormatWith(values);

		// Assert
		Assert.Equal($"{n0} , {n1} , {n2}", result);
	}

	[Fact]
	public void Replace_Array_Values()
	{
		// Arrange
		const string format = "{zero} , {one} , {two:0.0}";
		var zero = F.Rnd.Int;
		var one = F.Rnd.Str;
		var two = (double)F.Rnd.Int;
		var values = new object[] { zero, one, two };

		// Act
		var result = format.FormatWith(values);

		// Assert
		Assert.Equal($"{zero} , {one} , {two:0.0}", result);
	}

	[Fact]
	public void Replace_Numbered_Values_With_Formats()
	{
		// Arrange
		const string format = "{0:00} , {1:0.00} , {2:0,000.0}";
		var n0 = F.Rnd.Int;
		var n1 = F.Rnd.Int;
		var n2 = F.Rnd.Int;
		var values = new[] { n0, n1, n2 };

		// Act
		var result = format.FormatWith(values);

		// Assert
		Assert.Equal($"{n0:00} , {n1:0.00} , {n2:0,000.0}", result);
	}

	[Fact]
	public void Replace_Named_Values_With_Formats()
	{
		// Arrange
		const string format = "{zero:00} , {one:0.00} , {two:0,000.0}";
		var n0 = F.Rnd.Int;
		var n1 = F.Rnd.Int;
		var n2 = F.Rnd.Int;
		var values = new { zero = n0, one = n1, two = n2 };

		// Act
		var result = format.FormatWith(values);

		// Assert
		Assert.Equal($"{n0:00} , {n1:0.00} , {n2:0,000.0}", result);
	}

	[Fact]
	public void Replace_Array_Values_With_Formats()
	{
		// Arrange
		const string format = "{zero:00} , {one:0.00} , {2:0,000.0}";
		var n0 = F.Rnd.Int;
		var n1 = F.Rnd.Int;
		var n2 = F.Rnd.Int;
		var values = new object[] { n0, n1, n2 };

		// Act
		var result = format.FormatWith(values);

		// Assert
		Assert.Equal($"{n0:00} , {n1:0.00} , {n2:0,000.0}", result);
	}

	[Fact]
	public void Replace_Values_When_Format_Named_Values_Numbered()
	{
		// Arrange
		const string format = "{zero} , {one} , {two}";
		var n0 = F.Rnd.Int;
		var n1 = F.Rnd.Int;
		var n2 = F.Rnd.Int;
		var values = new[] { n0, n1, n2 };

		// Act
		var result = format.FormatWith(values);

		// Assert
		Assert.Equal($"{n0} , {n1} , {n2}", result);
	}

	[Fact]
	public void Replace_Values_When_Format_Mixed_Values_Numbered()
	{
		// Arrange
		const string format = "{zero} , {0} , {1}";
		var n0 = F.Rnd.Int;
		var n1 = F.Rnd.Int;
		var n2 = F.Rnd.Int;
		var values = new[] { n0, n1, n2 };

		// Act
		var result = format.FormatWith(values);

		// Assert
		Assert.Equal($"{n0} , {n1} , {n2}", result);
	}

	[Fact]
	public void Return_Format_When_Format_Numbered_Values_Named()
	{
		// Arrange
		const string format = "{0} , {1} , {2}";
		var values = new { zero = 3, one = 4, two = 5 };

		// Act
		var result = format.FormatWith(values);

		// Assert
		Assert.Equal(format, result);
	}
}
