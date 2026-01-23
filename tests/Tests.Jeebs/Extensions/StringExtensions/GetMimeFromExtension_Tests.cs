// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.StringExtensions_Tests;

public class GetMimeFromExtension_Tests
{
	[Theory]
	[InlineData("")]
	public void NullOrEmpty_ReturnsOriginal(string input)
	{
		// Arrange

		// Act
		var result = input.GetMimeFromExtension();

		// Assert
		Assert.Equal(input, result);
	}

	[Theory]
#pragma warning disable xUnit1044 // Avoid using TheoryData type arguments that are not serializable
	[MemberData(nameof(GetMimeTypeData))]
#pragma warning restore xUnit1044 // Avoid using TheoryData type arguments that are not serializable
	public void String_ReturnsMimeType(string input, MimeType expected)
	{
		// Arrange

		// Act
		var result = input.GetMimeFromExtension();

		// Assert
		Assert.Equal(expected.ToString(), result);
	}

	public static TheoryData<string, MimeType> GetMimeTypeData =>
		new()
		{
			{ "file.xxx", MimeType.General },
			{ "file.bmp", MimeType.Bmp },
			{ "file.doc", MimeType.Doc },
			{ "file.docx", MimeType.Docx },
			{ "file.gif", MimeType.Gif },
			{ "file.jpg", MimeType.Jpg },
			{ "file.jpeg", MimeType.Jpg },
			{ "file.m4a", MimeType.M4a },
			{ "file.mp3", MimeType.Mp3 },
			{ "file.pdf", MimeType.Pdf },
			{ "file.png", MimeType.Png },
			{ "file.ppt", MimeType.Ppt },
			{ "file.pptx", MimeType.Pptx },
			{ "file.tar", MimeType.Tar },
			{ "file.xls", MimeType.Xls },
			{ "file.xlsx", MimeType.Xlsx },
			{ "file.zip", MimeType.Zip }
		};
}
