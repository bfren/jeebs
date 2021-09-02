// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;
using static Jeebs.WordPress.Data.AttachmentCustomField.Msg;

namespace Jeebs.WordPress.Data.CustomFields.AttachmentCustomField_Tests
{
	public class ParseAttachmentPostId_Tests
	{
		[Fact]
		public void Invalid_Long_Returns_None_With_ValueIsInvalidPostIdMsg()
		{
			// Arrange
			var type = typeof(ParseAttachmentPostId_Tests);
			var value = F.Rnd.Str;

			// Act
			var result = AttachmentCustomField.ParseAttachmentPostId(type, value);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<ValueIsInvalidPostIdMsg>(none);
		}

		[Fact]
		public void Valid_Long_Returns_TermId()
		{
			// Arrange
			var type = typeof(ParseAttachmentPostId_Tests);
			var value = F.Rnd.Ulng;

			// Act
			var result = AttachmentCustomField.ParseAttachmentPostId(type, value.ToString());

			// Assert
			var some = result.AssertSome();
			Assert.Equal(value, some.Value);
		}
	}
}
