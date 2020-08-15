using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.MimeType_Tests
{
	public class Parse_Tests
	{
		[Theory]
		[MemberData(nameof(GetMimeTypeData))]
		public void String_ReturnsMimeType(string input, MimeType expected)
		{
			// Arrange

			// Act
			var result = MimeType.Parse(input);

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void Null_ReturnsBlank()
		{
			// Arrange

			// Act
			var result = MimeType.Parse(null);

			// Assert
			Assert.Equal(MimeType.Blank, result);
		}

		[Theory]
		[InlineData("audio/aiff")]
		[InlineData("this could be anything")]
		public void UnknownMimeType_ReturnsGeneral(string input)
		{
			// Arrange

			// Act
			var result = MimeType.Parse(input);

			// Assert
			Assert.Equal(MimeType.General, result);
		}

		public static IEnumerable<object[]> GetMimeTypeData()
		{
			yield return new object[] { MimeType.Blank.ToString(), MimeType.Blank };
			yield return new object[] { MimeType.General.ToString(), MimeType.General };
			yield return new object[] { MimeType.Bmp.ToString(), MimeType.Bmp };
			yield return new object[] { MimeType.Doc.ToString(), MimeType.Doc };
			yield return new object[] { MimeType.Docx.ToString(), MimeType.Docx };
			yield return new object[] { MimeType.Gif.ToString(), MimeType.Gif };
			yield return new object[] { MimeType.Jpg.ToString(), MimeType.Jpg };
			yield return new object[] { MimeType.M4a.ToString(), MimeType.M4a };
			yield return new object[] { MimeType.Mp3.ToString(), MimeType.Mp3 };
			yield return new object[] { MimeType.Pdf.ToString(), MimeType.Pdf };
			yield return new object[] { MimeType.Png.ToString(), MimeType.Png };
			yield return new object[] { MimeType.Ppt.ToString(), MimeType.Ppt };
			yield return new object[] { MimeType.Pptx.ToString(), MimeType.Pptx };
			yield return new object[] { MimeType.Rar.ToString(), MimeType.Rar };
			yield return new object[] { MimeType.Tar.ToString(), MimeType.Tar };
			yield return new object[] { MimeType.Text.ToString(), MimeType.Text };
			yield return new object[] { MimeType.Xls.ToString(), MimeType.Xls };
			yield return new object[] { MimeType.Xlsx.ToString(), MimeType.Xlsx };
			yield return new object[] { MimeType.Zip.ToString(), MimeType.Zip };
		}
	}
}
