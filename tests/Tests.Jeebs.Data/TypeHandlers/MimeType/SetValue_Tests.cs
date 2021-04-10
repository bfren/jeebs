// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.TypeHandlers.MimeTypeTypeHandler_Tests
{
	public class SetValue_Tests
	{
		public static TheoryData<MimeType, string> Sets_Value_To_MimeType_Name_Data =>
			new()
			{
				{ MimeType.Blank, string.Empty },
				{ MimeType.General, "application/octet-stream" },
				{ MimeType.Bmp, "image/bmp" },
				{ MimeType.Doc, "application/msword" },
				{ MimeType.Docx, "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
				{ MimeType.Gif, "image/gif" },
				{ MimeType.Jpg, "image/jpeg" },
				{ MimeType.M4a, "audio/mp4" },
				{ MimeType.Mp3, "audio/mp3" },
				{ MimeType.Pdf, "application/pdf" },
				{ MimeType.Png, "image/png" },
				{ MimeType.Ppt, "application/vnd.ms-powerpoint" },
				{ MimeType.Pptx, "application/vnd.openxmlformats-officedocument.presentationml.presentation" },
				{ MimeType.Rar, "application/x-rar-compressed" },
				{ MimeType.Tar, "application/x-tar" },
				{ MimeType.Text, "text/plain" },
				{ MimeType.Xls, "application/vnd.ms-excel" },
				{ MimeType.Xlsx, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
				{ MimeType.Zip, "application/zip" },
			};

		[Theory]
		[MemberData(nameof(Sets_Value_To_MimeType_Name_Data))]
		public void Sets_Value_To_MimeType_Name(MimeType input, string expected)
		{
			// Arrange
			var handler = new MimeTypeTypeHandler();
			var parameter = Substitute.For<IDbDataParameter>();

			// Act
			handler.SetValue(parameter, input);

			// Assert
			parameter.Received().Value = expected;
		}
	}
}
