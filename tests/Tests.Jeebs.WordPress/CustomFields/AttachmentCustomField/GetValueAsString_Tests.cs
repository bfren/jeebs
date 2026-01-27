// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.CustomFields.AttachmentCustomField_Tests;

public class GetValueAsString_Tests
{
	[Fact]
	public void Returns_Attachment_Title()
	{
		// Arrange
		var title = Rnd.Str;
		var field = new Test(Rnd.Str, title);

		// Act
		var result = field.GetValueAsStringTest();

		// Assert
		Assert.Equal(title, result);
	}

	public class Test : AttachmentCustomField
	{
		public Test(string key, string urlPath) : base(key) =>
			ValueObj = new() { Title = urlPath };
	}
}
