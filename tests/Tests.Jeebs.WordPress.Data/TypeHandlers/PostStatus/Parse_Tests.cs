// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.WordPress.Data.Enums;
using Xunit;
using Base = Jeebs.WordPress.Data.Enums.PostStatus_Tests.Parse_Tests;

namespace Jeebs.WordPress.Data.TypeHandlers.PostStatusTypeHandler_Tests
{
	public class Parse_Tests
	{
		[Theory]
		[MemberData(nameof(Base.Returns_Correct_PostStatus_Data), MemberType = typeof(Base))]
		public void Valid_Value_Returns_PostStatusType(string input, PostStatus expected)
		{
			// Arrange
			var handler = new PostStatusTypeHandler();

			// Act
			var result = handler.Parse(input);

			// Assert
			Assert.Same(expected, result);
		}

		[Theory]
		[InlineData(null)]
		public void Null_Value_Returns_Draft_PostStatus(object input)
		{
			// Arrange
			var handler = new PostStatusTypeHandler();

			// Act
			var result = handler.Parse(input);

			// Assert
			Assert.Same(PostStatus.Draft, result);
		}

		[Fact]
		public void Invalid_Value_Returns_Draft_PostStatus()
		{
			// Arrange
			var value = F.Rnd.Str;
			var handler = new PostStatusTypeHandler();

			// Act
			var result = handler.Parse(value);

			// Assert
			Assert.Same(PostStatus.Draft, result);
		}
	}
}
