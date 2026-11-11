// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Services.Drivers.Drawing.Stb.ImageDriver_Tests;

public class FromFile_Tests
{
	[Fact]
	public void Missing_File_Returns_Failure()
	{
		// Arrange
		var driver = new ImageDriver();

		// Act
		var result = driver.FromFile(Path.Combine(Path.GetTempPath(), Rnd.Str));

		// Assert
		_ = result.AssertFailure("Image file does not exist: '{Path}'.");
	}

	[Fact]
	public void Not_An_Image_Returns_Failure()
	{
		// Arrange
		var driver = new ImageDriver();
		var path = Path.GetTempFileName();
		File.WriteAllText(path, Rnd.Str);

		try
		{
			// Act
			var result = driver.FromFile(path);

			// Assert
			_ = result.AssertFailure("Not a valid image.");
		}
		finally
		{
			File.Delete(path);
		}
	}

	[Fact]
	public void Image_Returns_Wrapper()
	{
		// Arrange
		var driver = new ImageDriver();
		var path = Path.GetTempFileName();
		File.WriteAllBytes(path, TestImage.Png(40, 20, TestImage.Stripes));

		try
		{
			// Act
			var result = driver.FromFile(path);

			// Assert
			using var image = result.AssertOk();
			Assert.Equal(40, image.Width);
			Assert.Equal(20, image.Height);
		}
		finally
		{
			File.Delete(path);
		}
	}
}
