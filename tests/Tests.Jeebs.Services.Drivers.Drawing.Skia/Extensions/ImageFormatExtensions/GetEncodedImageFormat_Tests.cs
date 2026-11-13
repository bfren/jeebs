// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Services.Drawing;
using SkiaSharp;

namespace Jeebs.Services.Drivers.Drawing.Skia.ImageFormatExtensions_Tests;

public class GetEncodedImageFormat_Tests
{
	[Theory]
	[InlineData(ImageFormat.Jpeg, SKEncodedImageFormat.Jpeg)]
	[InlineData(ImageFormat.Png, SKEncodedImageFormat.Png)]
	public void Returns_Skia_Format(ImageFormat input, SKEncodedImageFormat expected)
	{
		// Arrange

		// Act
		var result = input.GetEncodedImageFormat();

		// Assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public void Unknown_Format_Throws_UnsupportedImageFormatException()
	{
		// Arrange
		var input = ImageFormat.Unsupported;

		// Act
		object action() => input.GetEncodedImageFormat();

		// Assert
		_ = Assert.Throws<UnsupportedImageFormatException>(action);
	}
}
