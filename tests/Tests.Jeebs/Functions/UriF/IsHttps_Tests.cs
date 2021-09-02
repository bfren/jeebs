// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace F.UriF_Tests
{
	public class IsHttps_Tests
	{
		[Theory]
		[InlineData(null, true)]
		[InlineData(null, false)]
		[InlineData("", true)]
		[InlineData("", false)]
		[InlineData(" ", true)]
		[InlineData(" ", false)]
		[InlineData("C:/test/path/file.txt", true)]
		[InlineData("C:/test/path/file.txt", false)]
		[InlineData("file:///C:/test/path/file.txt", true)]
		[InlineData("file:///C:/test/path/file.txt", false)]
		[InlineData("ftp://contoso/files/testfile.txt", true)]
		[InlineData("ftp://contoso/files/testfile.txt", false)]
		[InlineData("gopher://example.contoso.com/", true)]
		[InlineData("gopher://example.contoso.com/", false)]
		[InlineData("mailto:user@contoso.com?subject=uri", true)]
		[InlineData("mailto:user@contoso.com?subject=uri", false)]
		[InlineData("news:123456@contoso.com", true)]
		[InlineData("news:123456@contoso.com", false)]
		[InlineData("nntp://news.contoso.com/123456@contoso.com", true)]
		[InlineData("nntp://news.contoso.com/123456@contoso.com", false)]
		[InlineData("http://news.contoso.com", true)]
		public void Returns_False(string input, bool requireHttps)
		{
			// Arrange

			// Act
			var result = UriF.IsHttp(input, requireHttps);

			// Assert
			Assert.False(result);
		}

		[Theory]
		[InlineData("http://news.contoso.com", false)]
		[InlineData("https://news.contoso.com", false)]
		[InlineData("https://news.contoso.com", true)]
		public void Returns_True(string input, bool requireHttps)
		{
			// Arrange

			// Act
			var result = UriF.IsHttp(input, requireHttps);

			// Assert
			Assert.True(result);
		}
	}
}
