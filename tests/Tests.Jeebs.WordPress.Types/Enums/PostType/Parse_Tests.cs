﻿// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.WordPress.Data.Enums.PostType_Tests
{
	public class Parse_Tests
	{
		public static TheoryData<string, PostType> Returns_Correct_PostType_Data =>
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
		[MemberData(nameof(Returns_Correct_PostType_Data))]
		public void Returns_Correct_PostType(string name, PostType type)
		{
			// Arrange

			// Act
			var result = PostType.Parse(name);

			// Assert
			Assert.Same(type, result);
		}

		[Fact]
		public void Unknown_Returns_Post()
		{
			// Arrange
			var name = F.Rnd.Str;

			// Act
			var result = PostType.Parse(name);

			// Assert
			Assert.Same(PostType.Post, result);
		}

		[Fact]
		public void Returns_Custom_PostType()
		{
			// Arrange
			var name = F.Rnd.Str;
			var type = new PostType(name);
			PostType.AddCustomPostType(type);

			// Act
			var result = PostType.Parse(name);

			// Assert
			Assert.Same(type, result);
		}
	}
}
