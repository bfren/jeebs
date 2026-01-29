// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.CustomFields.AttachmentCustomField_Tests;

public class ParseAttachmentPostId_Tests
{
	[Fact]
	public void Invalid_Long_Returns_None_With_ValueIsInvalidPostIdMsg()
	{
		// Arrange
		var type = typeof(ParseAttachmentPostId_Tests);
		var value = Rnd.Str;

		// Act
		var result = AttachmentCustomField.ParseAttachmentPostId(type, value);

		// Assert
		_ = result.AssertFailure("'{Value}' is not a valid Post ID.",
			value
		);
	}

	[Fact]
	public void Valid_Long_Returns_TermId()
	{
		// Arrange
		var type = typeof(ParseAttachmentPostId_Tests);
		var value = Rnd.UInt64;

		// Act
		var result = AttachmentCustomField.ParseAttachmentPostId(type, value.ToString());

		// Assert
		var ok = result.AssertOk();
		Assert.Equal(value, ok.Value);
	}
}
