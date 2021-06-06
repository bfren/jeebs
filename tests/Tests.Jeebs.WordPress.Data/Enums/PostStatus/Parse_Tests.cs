// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.WordPress.Data.Enums.PostStatus_Tests
{
	public class Parse_Tests
	{
		public static TheoryData<string, PostStatus> Returns_Correct_PostStatus_Data =>
			new()
			{
				{ "auto-draft", PostStatus.AutoDraft },
				{ "draft", PostStatus.Draft },
				{ "inherit", PostStatus.Inherit },
				{ "pending", PostStatus.Pending },
				{ "publish", PostStatus.Publish }
			};

		[Theory]
		[MemberData(nameof(Returns_Correct_PostStatus_Data))]
		public void Returns_Correct_PostStatus(string name, PostStatus type)
		{
			// Arrange

			// Act
			var result = PostStatus.Parse(name);

			// Assert
			Assert.Same(type, result);
		}

		[Fact]
		public void Unknown_Returns_Draft()
		{
			// Arrange
			var name = F.Rnd.Str;

			// Act
			var result = PostStatus.Parse(name);

			// Assert
			Assert.Same(PostStatus.Draft, result);
		}
	}
}
