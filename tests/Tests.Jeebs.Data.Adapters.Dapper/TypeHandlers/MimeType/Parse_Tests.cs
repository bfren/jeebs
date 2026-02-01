// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Adapters.Dapper.TypeHandlers.MimeTypeTypeHandler_Tests;

public class Parse_Tests
{
	public static TheoryData<string, MimeType> Returns_Correct_MimeType_Data =>
		[
			(string.Empty, MimeType.Blank),
			("application/octet-stream", MimeType.General),
			("image/bmp", MimeType.Bmp),
			("application/msword", MimeType.Doc),
			("application/vnd.openxmlformats-officedocument.wordprocessingml.document", MimeType.Docx),
			("image/gif", MimeType.Gif),
			("image/jpeg", MimeType.Jpg),
			("audio/mp4", MimeType.M4a),
			("audio/mp3", MimeType.Mp3),
			("application/pdf", MimeType.Pdf),
			("image/png", MimeType.Png),
			("application/vnd.ms-powerpoint", MimeType.Ppt),
			("application/vnd.openxmlformats-officedocument.presentationml.presentation", MimeType.Pptx),
			("application/x-rar-compressed", MimeType.Rar),
			("application/x-tar", MimeType.Tar),
			("text/plain", MimeType.Text),
			("application/vnd.ms-excel", MimeType.Xls),
			("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", MimeType.Xlsx),
			("application/zip", MimeType.Zip)
		];

	[Theory]
	[MemberData(nameof(Returns_Correct_MimeType_Data))]
	public void Valid_Value_Returns_MimeType(string input, MimeType expected)
	{
		// Arrange
		var handler = new MimeTypeTypeHandler();

		// Act
		var result = handler.Parse(input);

		// Assert
		Assert.Same(expected, result);
	}

	[Fact]
	public void Null_Value_Returns_Blank_MimeType()
	{
		// Arrange
		var handler = new MimeTypeTypeHandler();

		// Act
		var result = handler.Parse(null!);

		// Assert
		Assert.Same(MimeType.Blank, result);
	}

	[Fact]
	public void Invalid_Value_Returns_Blank_MimeType()
	{
		// Arrange
		var value = Rnd.Str;
		var handler = new MimeTypeTypeHandler();

		// Act
		var result = handler.Parse(value);

		// Assert
		Assert.Same(MimeType.Blank, result);
	}
}
