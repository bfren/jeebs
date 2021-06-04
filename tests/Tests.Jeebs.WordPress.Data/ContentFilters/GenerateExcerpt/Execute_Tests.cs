// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.StringExtensions_Tests;
using Xunit;

namespace Jeebs.WordPress.Data.ContentFilters.GenerateExcerpt_Tests
{
	public class Execute_Tests
	{
		[Theory]
		[InlineData("")]
		[InlineData(" arg=\"one\"")]
		public void Removes_Square_Brackets_With_Content(string options)
		{
			// Arrange
			var shortcode = F.Rnd.Str;
			var content = F.Rnd.Str;
			var input = $"[{shortcode}{options}]{content}{Environment.NewLine}[/{shortcode}]";

			// Act
			var result = GenerateExcerpt.Create().Execute(input);

			// Assert
			Assert.Equal(string.Empty, result);
		}

		[Theory]
		[InlineData("")]
		[InlineData(" arg=\"one\"")]
		public void Removes_Square_Brackets_Without_Content(string options)
		{
			// Arrange
			var shortcode = F.Rnd.Str;
			var input = $"[{shortcode}{Environment.NewLine}{options}]";

			// Act
			var result = GenerateExcerpt.Create().Execute(input);

			// Assert
			Assert.Equal(string.Empty, result);
		}

		[Theory]
		[InlineData("\n")]
		[InlineData("\r")]
		public void Removes_New_Lines(string newline)
		{
			// Arrange
			var t0 = F.Rnd.Str;
			var t1 = F.Rnd.Str;
			var input = t0 + newline + t1;

			// Act
			var result = GenerateExcerpt.Create().Execute(input);

			// Assert
			Assert.Equal($"{t0} {t1}", result);
		}

		[Fact]
		public void With_More_Cuts_At_More()
		{
			// Arrange
			var t0 = F.Rnd.Str;
			var t1 = F.Rnd.Str;
			var input = $"{t0}<!--more-->{t1}";

			// Act
			var result = GenerateExcerpt.Create().Execute(input);

			// Assert
			Assert.Equal(t0, result);
		}

		[Theory]
		[MemberData(nameof(ReplaceHtmlTags_Tests.String_Returns_Value_With_Html_Tags_Replaced_Data), MemberType = typeof(ReplaceHtmlTags_Tests))]
		public void Removes_Html_Tags(string input, string expected)
		{
			// Arrange

			// Act
			var result = GenerateExcerpt.Create().Execute(input);

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void Removes_Multiple_Spaces_And_Trims()
		{
			// Arrange
			var t0 = F.Rnd.Str;
			var t1 = F.Rnd.Str;
			var input = $"  {t0}     {t1}  ";

			// Act
			var result = GenerateExcerpt.Create().Execute(input);

			// Assert
			Assert.Equal($"{t0} {t1}", result);
		}

		[Fact]
		public void Cuts_To_Maximum_Length()
		{
			// Arrange
			var max = F.Rnd.Int;
			var length = max * 2;
			var input = F.Rnd.StringF.Get(length);
			var expected = input[..max] + "..";

			// Act
			var result = GenerateExcerpt.Create(max).Execute(input);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
