// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.MimeType_Tests;

public class Parse_Tests
{
	public static TheoryData<string, MimeType> Returns_Correct_MimeType_Data =>
		new()
		{
			{ string.Empty, MimeType.Blank },
			{ "application/octet-stream", MimeType.General },
			{ "image/bmp", MimeType.Bmp },
			{ "application/msword", MimeType.Doc },
			{ "application/vnd.openxmlformats-officedocument.wordprocessingml.document", MimeType.Docx },
			{ "image/gif", MimeType.Gif },
			{ "image/jpeg", MimeType.Jpg },
			{ "audio/mp4", MimeType.M4a },
			{ "audio/mp3", MimeType.Mp3 },
			{ "application/pdf", MimeType.Pdf },
			{ "image/png", MimeType.Png },
			{ "application/vnd.ms-powerpoint", MimeType.Ppt },
			{ "application/vnd.openxmlformats-officedocument.presentationml.presentation", MimeType.Pptx },
			{ "application/x-rar-compressed", MimeType.Rar },
			{ "application/x-tar", MimeType.Tar },
			{ "text/plain", MimeType.Text },
			{ "application/vnd.ms-excel", MimeType.Xls },
			{ "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", MimeType.Xlsx },
			{ "application/zip", MimeType.Zip },
		};

	[Theory]
	[MemberData(nameof(Returns_Correct_MimeType_Data))]
	public void Returns_Correct_MimeType(string name, MimeType type)
	{
		// Arrange

		// Act
		var result = MimeType.Parse(name);

		// Assert
		Assert.Same(type, result);
	}

	[Fact]
	public void Null_Returns_Blank()
	{
		// Arrange

		// Act
		var result = MimeType.Parse(null);

		// Assert
		Assert.Same(MimeType.Blank, result);
	}

	[Fact]
	public void Unknown_Returns_General()
	{
		// Arrange
		var name = F.Rnd.Str;

		// Act
		var result = MimeType.Parse(name);

		// Assert
		Assert.Same(MimeType.General, result);
	}

	[Fact]
	public void Returns_Custom_MimeType()
	{
		// Arrange
		var name = F.Rnd.Str;
		var type = new MimeType(name);
		MimeType.AddCustomMimeType(type);

		// Act
		var result = MimeType.Parse(name);

		// Assert
		Assert.Same(type, result);
	}
}
