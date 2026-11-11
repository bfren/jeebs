// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using SkiaSharp;

namespace Jeebs.Services.Drivers.Drawing.Skia.ImageWrapper_Tests;

public class ApplyMask_Tests
{
	private static Services.Drawing.IImageWrapper Load(int width, int height)
	{
		using var stream = new MemoryStream(TestImage.StripedPng(width, height));
		return new ImageDriver().FromStream(stream).AssertOk();
	}

	[Fact]
	public void Zero_Width_And_Height_Returns_Failure()
	{
		// Arrange
		using var image = Load(40, 20);

		// Act
		var result = image.ApplyMask(0, 0);

		// Assert
		_ = result.AssertFailure("Mask width or height is required.");
	}

	[Fact]
	public void Returns_Image_Of_Mask_Size()
	{
		// Arrange
		using var image = Load(400, 200);

		// Act
		var result = image.ApplyMask(100, 100);

		// Assert
		using var masked = result.AssertOk();
		Assert.Equal(100, masked.Width);
		Assert.Equal(100, masked.Height);
	}

	[Fact]
	public void Smooths_When_Scaling_Down()
	{
		// Arrange
		using var image = Load(400, 200);

		// Act
		using var masked = image.ApplyMask(200, 100).AssertOk();
		var png = masked.ToPngByteArray().AssertOk();

		// Assert - halving black / white stripes gives grey, nearest-neighbour would give black or white
		using var bitmap = SKBitmap.Decode(png);
		var red = bitmap.GetPixel(100, 50).Red;
		Assert.InRange(red, 64, 192);
	}
}
