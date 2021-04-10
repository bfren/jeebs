// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.WordPress.Data.Enums;
using Xunit;

namespace Jeebs.WordPress.Data.TypeHandlers.PostTypeTypeHandler_Tests
{
	public class Parse_Tests
	{
		public static TheoryData<string, PostType> Valid_Value_Returns_PostType_Data =>
			new()
			{
				{ "acf", PostType.AdvancedCustomField },
				{ "attachment", PostType.Attachment },
				{ "nav_menu_item", PostType.MenuItem },
				{ "page", PostType.Page },
				{ "post", PostType.Post },
				{ "revision", PostType.Revision }
			};

		[Theory]
		[MemberData(nameof(Valid_Value_Returns_PostType_Data))]
		public void Valid_Value_Returns_PostType(string input, PostType expected)
		{
			// Arrange
			var handler = new PostTypeTypeHandler();

			// Act
			var result = handler.Parse(input);

			// Assert
			Assert.Same(expected, result);
		}

		[Theory]
		[InlineData(null)]
		public void Null_Value_Returns_Post_PostType(object input)
		{
			// Arrange
			var handler = new PostTypeTypeHandler();

			// Act
			var result = handler.Parse(input);

			// Assert
			Assert.Same(PostType.Post, result);
		}

		[Fact]
		public void Invalid_Value_Returns_Post_PostType()
		{
			// Arrange
			var value = F.Rnd.Str;
			var handler = new PostTypeTypeHandler();

			// Act
			var result = handler.Parse(value);

			// Assert
			Assert.Same(PostType.Post, result);
		}
	}
}
