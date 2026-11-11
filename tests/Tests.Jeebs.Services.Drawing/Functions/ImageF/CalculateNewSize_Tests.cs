// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Services.Drawing.Geometry;

namespace Jeebs.Services.Drawing.Functions.ImageF_Tests;

public class CalculateNewSize_Tests
{
	[Theory]
	[InlineData(400, 200, 100, 0, 100, 50)]
	[InlineData(400, 200, 0, 100, 200, 100)]
	[InlineData(400, 200, 100, 100, 100, 100)]
	public void Returns_Size_Keeping_Ratio_When_One_Side_Is_Zero(int imgW, int imgH, int newW, int newH, int expectedW, int expectedH)
	{
		// Arrange

		// Act
		var result = ImageF.CalculateNewSize(imgW, imgH, newW, newH);

		// Assert
		Assert.Equal(new Size(expectedW, expectedH), result);
	}
}
