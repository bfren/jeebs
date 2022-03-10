// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.CustomFields.FileCustomField_Tests;

public class GetValueAsString_Tests
{
	[Fact]
	public void Returns_File_UrlPath()
	{
		// Arrange
		var urlPath = Rnd.Str;
		var field = new Test(Rnd.Str, urlPath);

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
