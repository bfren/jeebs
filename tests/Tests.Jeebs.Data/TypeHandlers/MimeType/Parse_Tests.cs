// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;
using Base = Jeebs.MimeType_Tests.Parse_Tests;

namespace Jeebs.Data.TypeHandlers.MimeTypeTypeHandler_Tests
{
	public class Parse_Tests
	{
		[Theory]
		[MemberData(nameof(Base.Returns_Correct_MimeType_Data), MemberType = typeof(Base))]
		public void Valid_Value_Returns_MimeType(string input, MimeType expected)
		{
			// Arrange
			var handler = new MimeTypeTypeHandler();

			// Act
			var result = handler.Parse(input);

			// Assert
			Assert.Same(expected, result);
		}

		[Theory]
		[InlineData(null)]
		public void Null_Value_Returns_Blank_MimeType(object input)
		{
			// Arrange
			var handler = new MimeTypeTypeHandler();

			// Act
			var result = handler.Parse(input);

			// Assert
			Assert.Same(MimeType.Blank, result);
		}

		[Fact]
		public void Invalid_Value_Returns_General_MimeType()
		{
			// Arrange
			var value = F.Rnd.Str;
			var handler = new MimeTypeTypeHandler();

			// Act
			var result = handler.Parse(value);

			// Assert
			Assert.Same(MimeType.General, result);
		}
	}
}
