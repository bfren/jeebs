// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.WordPress.Data.CustomFields.FileCustomField_Tests
{
	public class GetValueAsString_Tests
	{
		[Fact]
		public void Returns_File_UrlPath()
		{
			// Arrange
			var urlPath = F.Rnd.Str;
			var field = new Test(F.Rnd.Str, urlPath);

			// Act
			var result = field.GetValueAsStringTest();

			// Assert
			Assert.Equal(urlPath, result);
		}

		public class Test : FileCustomField
		{
			public Test(string key, string urlPath) : base(key) =>
				ValueObj = new() { UrlPath = urlPath };
		}
	}
}
