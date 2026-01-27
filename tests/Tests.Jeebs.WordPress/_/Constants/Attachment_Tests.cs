// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Constants_Tests;

public class Attachment_Tests
{
	[Fact]
	public void Returns_Correct_Value()
	{
		// Arrange
		var expected = "_wp_attached_file";

		// Act
		var result = Constants.Attachment;

		// Assert
		Assert.Equal(expected, result);
	}
}
