// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Services.Drivers.Drawing.Stb.Functions.PixelF_Tests;

public class Orient_Tests
{
	// Expected colours (top-left, top-right, bottom-left, bottom-right) of a 4x2 image whose quadrants are
	// R G / B W, checked against the Skia driver for all eight orientations
	[Theory]
	[InlineData(1, 4, 2, "RGBW")]
	[InlineData(2, 4, 2, "GRWB")]
	[InlineData(3, 4, 2, "WBGR")]
	[InlineData(4, 4, 2, "BWRG")]
	[InlineData(5, 2, 4, "RBGW")]
	[InlineData(6, 2, 4, "BRWG")]
	[InlineData(7, 2, 4, "WGBR")]
	[InlineData(8, 2, 4, "GWRB")]
	public void Turns_And_Flips_As_Exif_Says(int orientation, int width, int height, string corners)
	{
		// Arrange
		var pixels = TestImage.Pixels(4, 2, TestImage.Quadrants(4, 2));

		// Act
		var (result, w, h) = PixelF.Orient(pixels, 4, 2, orientation);

		// Assert
		Assert.Equal(width, w);
		Assert.Equal(height, h);
		var actual = TestImage.ColourAt(result, w, 0, 0) + TestImage.ColourAt(result, w, w - 1, 0)
			+ TestImage.ColourAt(result, w, 0, h - 1) + TestImage.ColourAt(result, w, w - 1, h - 1);
		Assert.Equal(corners, actual);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(9)]
	public void Unknown_Orientation_Returns_Pixels_Unchanged(int orientation)
	{
		// Arrange
		var pixels = TestImage.Pixels(4, 2, TestImage.Quadrants(4, 2));

		// Act
		var (result, w, h) = PixelF.Orient(pixels, 4, 2, orientation);

		// Assert
		Assert.Same(pixels, result);
		Assert.Equal((4, 2), (w, h));
	}
}
