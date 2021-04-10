// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.WordPress.Data.Enums;
using Xunit;

namespace Jeebs.WordPress.Data.TypeHandlers.CommentTypeTypeHandler_Tests
{
	public class Parse_Tests
	{
		public static TheoryData<string, CommentType> Valid_Value_Returns_CommentType_Data =>
			new()
			{
				{ string.Empty, CommentType.Blank },
				{ "pingback", CommentType.Pingback }
			};

		[Theory]
		[MemberData(nameof(Valid_Value_Returns_CommentType_Data))]
		public void Valid_Value_Returns_CommentType(string input, CommentType expected)
		{
			// Arrange
			var handler = new CommentTypeTypeHandler();

			// Act
			var result = handler.Parse(input);

			// Assert
			Assert.Same(expected, result);
		}

		[Theory]
		[InlineData(null)]
		public void Null_Value_Returns_Blank_CommentType(object input)
		{
			// Arrange
			var handler = new CommentTypeTypeHandler();

			// Act
			var result = handler.Parse(input);

			// Assert
			Assert.Same(CommentType.Blank, result);
		}

		[Fact]
		public void Invalid_Value_Returns_Blank_CommentType()
		{
			// Arrange
			var value = F.Rnd.Str;
			var handler = new CommentTypeTypeHandler();

			// Act
			var result = handler.Parse(value);

			// Assert
			Assert.Same(CommentType.Blank, result);
		}
	}
}
