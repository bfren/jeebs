// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.WordPress.Data.CustomFields.AttachmentCustomField_Tests
{
	public class GetValueAsString_Tests
	{
		[Fact]
		public void Returns_Attachment_Title()
		{
			// Arrange
			var title = F.Rnd.Str;
			var field = new Test(F.Rnd.Str, title);

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
}
