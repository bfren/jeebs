// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.WordPress.Data.Constants_Tests
{
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
}
