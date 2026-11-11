// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Services.Drivers.Drawing.Stb.ImageDriver_Tests;

public class FromStream_Tests
{
	[Fact]
	public void Not_An_Image_Returns_Failure()
	{
		// Arrange
		var driver = new ImageDriver();
		using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(Rnd.Str));

		// Act
		var result = driver.FromStream(stream);

		// Assert
		_ = result.AssertFailure("Not a valid image.");
	}

	[Fact]
	public void Image_Returns_Wrapper()
	{
		// Arrange
		var driver = new ImageDriver();
		using var stream = new MemoryStream(TestImage.Png(40, 20, TestImage.Stripes));

		// Act
		var result = driver.FromStream(stream);

		// Assert
		using var image = result.AssertOk();
		Assert.Equal(40, image.Width);
		Assert.Equal(20, image.Height);
	}

	[Fact]
	public void Applies_Exif_Orientation()
	{
		// Arrange
		var driver = new ImageDriver();
		var jpeg = TestImage.WithOrientation(TestImage.Jpeg(40, 20, TestImage.Quadrants(40, 20)), 6);
		using var stream = new MemoryStream(jpeg);

		// Act
		var result = driver.FromStream(stream);

		// Assert - turned 90 clockwise, so the bottom-left (blue) quadrant is now top-left
		using var image = result.AssertOk();
		Assert.Equal(20, image.Width);
		Assert.Equal(40, image.Height);
		Assert.Equal("B", TestImage.ColourAt(((ImageWrapper)image).Pixels, 20, 2, 2));
	}
}
