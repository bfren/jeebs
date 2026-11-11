// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Services.Drawing.Geometry;

namespace Jeebs.Services.Drivers.Drawing.Stb.Functions.PixelF_Tests;

public class Resize_Tests
{
	[Fact]
	public void Shrinking_Averages_Neighbouring_Pixels()
	{
		// Arrange - black / white columns
		var pixels = TestImage.Pixels(8, 1, TestImage.Stripes);

		// Act
		var result = PixelF.Resize(pixels, 8, 1, new Rectangle(0, 0, 8, 1), 4, 1);

		// Assert - an inside pixel is mid grey
		Assert.InRange(result[4], 120, 136);
		Assert.Equal(255, result[7]);
	}

	[Fact]
	public void Enlarging_Keeps_Solid_Colour()
	{
		// Arrange
		byte[] pixels = [100, 150, 200, 255];

		// Act
		var result = PixelF.Resize(pixels, 1, 1, new Rectangle(0, 0, 1, 1), 3, 3);

		// Assert
		Assert.Equal(3 * 3 * 4, result.Length);
		for (var i = 0; i < result.Length; i += 4)
		{
			Assert.Equal([100, 150, 200, 255], result[i..(i + 4)]);
		}
	}

	[Fact]
	public void Transparent_Pixels_Do_Not_Darken_Colour()
	{
		// Arrange - opaque red next to fully transparent black
		byte[] pixels = [255, 0, 0, 255, 0, 0, 0, 0];

		// Act
		var result = PixelF.Resize(pixels, 2, 1, new Rectangle(0, 0, 2, 1), 1, 1);

		// Assert - still full red, half transparent (without alpha weighting the red would halve)
		Assert.Equal(255, result[0]);
		Assert.InRange(result[3], 120, 136);
	}

	[Fact]
	public void Uses_Only_The_Cropped_Area()
	{
		// Arrange - left half red, right half blue
		var pixels = TestImage.Pixels(8, 2, (x, _) => x < 4 ? TestImage.Red : TestImage.Blue);

		// Act - crop the right half
		var result = PixelF.Resize(pixels, 8, 2, new Rectangle(4, 0, 4, 2), 2, 1);

		// Assert - the right output pixel is pure blue (the left one may pick up a little red from the edge)
		Assert.Equal("B", TestImage.ColourAt(result, 2, 1, 0));
	}
}
