// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.PostAttachment_Tests
{
	public class GetFilePath_Tests
	{
		[Fact]
		public void Ensure_WpUploads_Ends_With_Forward_Slash()
		{
			// Arrange
			var wpUploads = F.Rnd.Str;
			var attachment = Substitute.ForPartsOf<PostAttachment>();

			// Act
			var result = attachment.GetFilePath(wpUploads);

			// Assert
			Assert.Equal($"{wpUploads}/", result);
		}

		[Fact]
		public void Trim_Forward_Slash_From_UrlPath()
		{
			// Arrange
			var urlPath = F.Rnd.Str;
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
			var wpUploads = F.Rnd.Str;
			var urlPath = F.Rnd.Str;
			var attachment = new Test { UrlPath = urlPath };

			// Act
			var result = attachment.GetFilePath(wpUploads);

			// Assert
			Assert.Equal($"{wpUploads}/{urlPath}", result);
		}

		public record class Test : PostAttachment;
	}
}
