// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Entities.PostAttachment_Tests;

public class GetFilePath_Tests
{
	[Fact]
	public void Ensure_WpUploads_Ends_With_Forward_Slash()
	{
		// Arrange
		var wpUploads = Rnd.Str;
		var attachment = new Test();

		// Act
		var result = attachment.GetFilePath(wpUploads);

		// Assert
		Assert.Equal($"{wpUploads}/", result);
	}

	[Fact]
	public void Trim_Forward_Slash_From_UrlPath()
	{
		// Arrange
		var urlPath = Rnd.Str;
		var attachment = new Test { UrlPath = "/" + urlPath };

		// Act
		var result = attachment.GetFilePath(string.Empty);

		// Assert
		Assert.Equal(urlPath, result);
	}

	[Fact]
	public void Returns_File_Path()
	{
		// Arrange
		var wpUploads = Rnd.Str;
		var urlPath = Rnd.Str;
		var attachment = new Test { UrlPath = urlPath };

		// Act
		var result = attachment.GetFilePath(wpUploads);

		// Assert
		Assert.Equal($"{wpUploads}/{urlPath}", result);
	}

	public record class Test : PostAttachment;
}
