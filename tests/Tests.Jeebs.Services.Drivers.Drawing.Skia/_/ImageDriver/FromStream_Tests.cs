// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Services.Drivers.Drawing.Skia.ImageDriver_Tests;

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
		using var stream = new MemoryStream(TestImage.StripedPng(40, 20));

		// Act
		var result = driver.FromStream(stream);

		// Assert
		using var image = result.AssertOk();
		Assert.Equal(40, image.Width);
		Assert.Equal(20, image.Height);
	}
}
