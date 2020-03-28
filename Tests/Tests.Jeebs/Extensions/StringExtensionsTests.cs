using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Extensions
{
	public sealed class StringExtensionsTests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void ConvertQuotes_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.ConvertCurlyQuotes();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("'Ben'", "‘Ben’")]
		[InlineData("'Ben' 'Green'", "‘Ben’ ‘Green’")]
		[InlineData("'Ben's Test'", "‘Ben’s Test’")]
		[InlineData("\"Ben\"", "“Ben”")]
		[InlineData("\"Ben\" \"Green\"", "“Ben” “Green”")]
		[InlineData("\"Ben's Test\"", "“Ben’s Test”")]
		public void ConvertQuotes_String_ReturnsValueWithQuotesConverted(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.ConvertCurlyQuotes();

			// Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData("<a href=\"test\">'Ben'</a>", "<a href=\"test\">&lsquo;Ben&rsquo;</a>")]
		[InlineData("<a href=\"test\">'Ben'</a> 'Green'", "<a href=\"test\">&lsquo;Ben&rsquo;</a> &lsquo;Green&rsquo;")]
		[InlineData("<a href=\"test\">'Ben's Test'</a>", "<a href=\"test\">&lsquo;Ben&rsquo;s Test&rsquo;</a>")]
		[InlineData("<a href=\"test\">\"Ben\"</a>", "<a href=\"test\">&ldquo;Ben&rdquo;</a>")]
		[InlineData("<a href=\"test\">\"Ben\"</a> \"Green\"", "<a href=\"test\">&ldquo;Ben&rdquo;</a> &ldquo;Green&rdquo;")]
		[InlineData("<a href=\"test\">\"Ben's Test\"</a>", "<a href=\"test\">&ldquo;Ben&rsquo;s Test&rdquo;</a>")]
		public void ConvertInnerHtmlQuotes_Html_ReturnsHtmlWithConvertedQuotes(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.ConvertInnerHtmlQuotes();

			// Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void EndWith_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var resultChar = input.EndWith('.');
			var resultStr = input.EndWith(".");

			// Assert
			Assert.Equal(input, resultChar);
			Assert.Equal(input, resultStr);
		}

		[Theory]
		[InlineData("Be", 'n', "Ben")]
		[InlineData("Ben", 'n', "Ben")]
		public void EndWith_String_ReturnsValueEndingWithCharacter(string input, char endWith, string expected)
		{
			// Arrange

			// Act
			var result = input.EndWith(endWith);

			// Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData("Be", "n Green", "Ben Green")]
		[InlineData("Ben Green", "n Green", "Ben Green")]
		public void EndWith_String_ReturnsValueEndingWithString(string input, string endWith, string expected)
		{
			// Arrange

			// Act
			var result = input.EndWith(endWith);

			// Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void GetMimeFromExtension_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.GetMimeFromExtension();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[MemberData(nameof(GetMimeTypeData))]
		public void GetMimeFromExtension_String_ReturnsMimeType(string input, MimeType expected)
		{
			// Arrange

			// Act
			var result = input.GetMimeFromExtension();

			// Assert
			Assert.Equal(expected.ToString(), result);
		}

		public static IEnumerable<object[]> GetMimeTypeData()
		{
			yield return new object[] { "file.xxx", MimeType.General };
			yield return new object[] { "file.bmp", MimeType.Bmp };
			yield return new object[] { "file.doc", MimeType.Doc };
			yield return new object[] { "file.docx", MimeType.Docx };
			yield return new object[] { "file.gif", MimeType.Gif };
			yield return new object[] { "file.jpg", MimeType.Jpg };
			yield return new object[] { "file.jpeg", MimeType.Jpg };
			yield return new object[] { "file.m4a", MimeType.M4a };
			yield return new object[] { "file.mp3", MimeType.Mp3 };
			yield return new object[] { "file.pdf", MimeType.Pdf };
			yield return new object[] { "file.png", MimeType.Png };
			yield return new object[] { "file.ppt", MimeType.Ppt };
			yield return new object[] { "file.pptx", MimeType.Pptx };
			yield return new object[] { "file.tar", MimeType.Tar };
			yield return new object[] { "file.xls", MimeType.Xls };
			yield return new object[] { "file.xlsx", MimeType.Xlsx };
			yield return new object[] { "file.zip", MimeType.Zip };
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void EscapeSingleQuotes_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.EscapeSingleQuotes();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("'", "\\'")]
		[InlineData("'Ben'", "\\'Ben\\'")]
		public void EscapeSingleQuotes_String_ReturnsValueWithSingleQuotesEscaped(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.EscapeSingleQuotes();

			// Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData("&amp;&lt;&gt;&#39;&#169;&copy;", "&<>'©©")]
		[InlineData("&lt;p&gt;Paragraph Text&lt;/p&gt;", "<p>Paragraph Text</p>")]
		public void HtmlDecode_Html_ReturnsDecodedHtml(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.HtmlDecode();

			// Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData("&<>'©©", "&amp;&lt;&gt;&#39;&#169;&#169;")]
		[InlineData("<p>Paragraph Text</p>", "&lt;p&gt;Paragraph Text&lt;/p&gt;")]
		public void HtmlEncode_Html_ReturnsEncodedHtml(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.HtmlEncode();

			// Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void NoLongerThan_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.NoLongerThan(10);

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData(null, 0, "..", "Empty", "Empty")]
		[InlineData("123", 4, null, null, "123")]
		[InlineData("1234", 4, null, null, "1234")]
		[InlineData("12345", 4, "..", null, "1234..")]
		[InlineData("12345", 4, null, null, "1234")]
		public void NoLongerThan_String_ReturnsTruncatedValue(string input, int max, string continuation, string empty, string expected)
		{
			// Arrange

			// Act
			var result = input.NoLongerThan(max, continuation, empty);

			// Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void Normalise_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.Normalise();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("&$G54F*FH(3)FKASD63&£asdf", "gffhfkasdasdf")]
		[InlineData("one two three", "one-two-three")]
		[InlineData("one-two-three", "one-two-three")]
		[InlineData(" one  two   three    ", "one-two-three")]
		[InlineData("1-two three", "two-three")]
		public void Normalise_String_ReturnsNormalisedValue(string input, string expcted)
		{
			// Arrange

			// Act
			var result = input.Normalise();

			// Assert
			Assert.Equal(expcted, result);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void NullIfEmpty_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.NullIfEmpty();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("Ben")]
		public void NullIfEmpty_String_ReturnsOriginalValue(string input)
		{
			// Arrange

			// Act
			var result = input.NullIfEmpty();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void ReplaceAll_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
			var result = input.ReplaceAll(null, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("Ben Green", new[] { "e", "n" }, null, "B Gr")]
		[InlineData("Ben Green", new[] { "e", "n" }, "-", "B-- Gr---")]
		public void ReplaceAll_String_ReturnsValueWithStringsReplaced(string input, string[] replace, string with, string expected)
		{
			// Arrange

			// Act
			var result = input.ReplaceAll(replace, with);

			// Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void ReplaceNonNumerical_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.ReplaceNonNumerical();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("Bro65ken12", null, "6512")]
		[InlineData("Bro65ken12", "-", "-65-12")]
		public void ReplaceNonNumerical_String_ReturnsValueWithNumbersReplaced(string input, string with, string expected)
		{
			// Arrange

			// Act
			var result = input.ReplaceNonNumerical(with);

			// Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void ReplaceNonWord_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.ReplaceNonWord();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData(" {B)e(n_ G}re $%en&", null, "Ben_Green")]
		[InlineData("B!n_Gr@#en", "e", "Ben_Green")]
		[InlineData(" {B)e(n_ G}re $%en&", "-", "-B-e-n_-G-re-en-")]
		public void ReplaceNonWord_String_ReturnsValueWithNonWordCharactersReplaced(string input, string with, string expected)
		{
			// Arrange

			// Act
			var result = input.ReplaceNonWord(with);

			// Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void SplitByCapitals_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.SplitByCapitals();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("BenjaminCharlesGreen", "Benjamin Charles Green")]
		[InlineData(" ben JaminCharlesGreen ", "ben Jamin Charles Green")]
		public void SplitByCapitals_String_ReturnsValueSplitByCapitals(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.SplitByCapitals();

			// Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void StartWith_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.StartWith(default(char));

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("en", 'B', "Ben")]
		[InlineData("Ben", 'B', "Ben")]
		[InlineData("ben", 'B', "Bben")]
		public void StartWith_String_ReturnsValueStartingWithCharacter(string input, char startWith, string expected)
		{
			// Arrange

			// Act
			var result = input.StartWith(startWith);

			// Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void ToASCII_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.ToASCII();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("Ben Green", "&#66;&#101;&#110;&#32;&#71;&#114;&#101;&#101;&#110;")]
		[InlineData("&<>#'{$^?~", "&#38;&#60;&#62;&#35;&#39;&#123;&#36;&#94;&#63;&#126;")]
		[InlineData("£ü©§", "&#63;&#63;&#63;&#63;")]
		public void ToASCII_String_ReturnsASCIIEncodedValue(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.ToASCII();

			// Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void ToLowerFirst_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.ToLowerFirst();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("Ben", "ben")]
		[InlineData("bEN", "bEN")]
		public void ToLowerFirst_String_ReturnsValueWithLowercaseFirstLetter(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.ToLowerFirst();

			// Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void ToUpperFirst_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.ToUpperFirst();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("bEN", "BEN")]
		[InlineData("Ben", "Ben")]
		public void ToUpperFirst_String_ReturnsValueWithUppercaseFirstLetter(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.ToUpperFirst();

			// Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void ToSentenceCase_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.ToSentenceCase();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("this is a test sentence", "This is a test sentence")]
		[InlineData("testing The PHP acronym", "Testing the php acronym")]
		public void ToSentenceCase_String_ReturnsValueInSentenceCase(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.ToSentenceCase();

			// Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void ToTitleCase_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.ToTitleCase();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("this is a test sentence", "This Is A Test Sentence")]
		[InlineData("testing The PHP acronym", "Testing The PHP Acronym")]
		public void ToTitleCase_String_ReturnsValueInTitleCase(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.ToTitleCase();

			// Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void ReplaceHtmlTags_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.ReplaceHtmlTags();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("<p>Ben</p>", "Ben")]
		[InlineData("<p class=\"attr\">Ben</p>", "Ben")]
		[InlineData("<p class=\"attr\">Ben <strong>Green</strong></p>", "Ben Green")]
		public void ReplaceHtmlTags_String_ReturnsValueWithHtmlTagsReplaced(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.ReplaceHtmlTags();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
