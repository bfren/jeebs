// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.WordPress.Constants_Tests;

public class AttachmentMetadata_Tests
{
	[Fact]
	public void Returns_Correct_Value()
	{
		// Arrange
		var expected = "_wp_attachment_metadata";

		// Act
		var result = Constants.AttachmentMetadata;

		// Assert
		Assert.Equal(expected, result);
	}
}
