// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.WordPress.Data.Constants_Tests
{
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
}
