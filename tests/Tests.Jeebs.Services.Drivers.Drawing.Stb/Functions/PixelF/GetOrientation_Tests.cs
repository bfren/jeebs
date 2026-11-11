// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Services.Drivers.Drawing.Stb.Functions.PixelF_Tests;

public class GetOrientation_Tests
{
	[Theory]
	[InlineData(6, false)]
	[InlineData(8, true)]
	[InlineData(3, false)]
	public void Jpeg_With_Exif_Returns_Orientation(int orientation, bool bigEndian)
	{
		// Arrange
		var jpeg = TestImage.WithOrientation(TestImage.Jpeg(8, 8, TestImage.Stripes), orientation, bigEndian);

		// Act
		var result = PixelF.GetOrientation(jpeg);

		// Assert
		Assert.Equal(orientation, result);
	}

	[Fact]
	public void Jpeg_Without_Exif_Returns_1()
	{
		// Arrange
		var jpeg = TestImage.Jpeg(8, 8, TestImage.Stripes);

		// Act
		var result = PixelF.GetOrientation(jpeg);

		// Assert
		Assert.Equal(1, result);
	}

	[Fact]
	public void Png_Returns_1()
	{
		// Arrange
		var png = TestImage.Png(8, 8, TestImage.Stripes);

		// Act
		var result = PixelF.GetOrientation(png);

		// Assert
		Assert.Equal(1, result);
	}

	[Theory]
	[InlineData(new byte[] { 0xFF, 0xD8, 0xFF, 0xE1, 0x00, 0x40, 0x45, 0x78 })]
	[InlineData(new byte[] { 0xFF, 0xD8, 0xFF, 0xE1, 0x00, 0x01 })]
	[InlineData(new byte[] { 0xFF, 0xD8 })]
	public void Truncated_Or_Broken_Header_Returns_1(byte[] data)
	{
		// Arrange

		// Act
		var result = PixelF.GetOrientation(data);

		// Assert
		Assert.Equal(1, result);
	}
}
