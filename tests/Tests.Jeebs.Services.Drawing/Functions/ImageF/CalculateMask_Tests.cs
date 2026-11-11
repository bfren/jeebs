// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Services.Drawing.Geometry;

namespace Jeebs.Services.Drawing.Functions.ImageF_Tests;

public class CalculateMask_Tests
{
	[Theory]
	[InlineData(400, 200, 100, 100, 100, 0, 200, 200)]
	[InlineData(200, 400, 100, 100, 0, 100, 200, 200)]
	[InlineData(400, 200, 200, 100, 0, 0, 400, 200)]
	public void Returns_Centred_Crop_With_Mask_Ratio(int imgW, int imgH, int maskW, int maskH, int x, int y, int w, int h)
	{
		// Arrange

		// Act
		var result = ImageF.CalculateMask(imgW, imgH, maskW, maskH);

		// Assert
		Assert.Equal(new Rectangle(x, y, w, h), result);
	}
}
