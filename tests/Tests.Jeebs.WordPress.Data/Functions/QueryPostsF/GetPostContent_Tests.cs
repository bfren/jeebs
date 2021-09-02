﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;
using Jeebs.WordPress.Data.Entities;
using Xunit;
using static F.WordPressF.DataF.QueryPostsF;

namespace F.WordPressF.DataF.QueryPostsF_Tests
{
	public class GetPostContent_Tests
	{
		[Fact]
		public void No_Content_Property_Returns_None_With_ContentPropertyNotFoundMsg()
		{
			// Arrange

			// Act
			var result = GetPostContent<NoContentProperty>();

			// Assert
			var none = result.AssertNone();
			Assert.IsType<Msg.ContentPropertyNotFoundMsg<NoContentProperty>>(none);
		}

		[Fact]
		public void Content_Property_Wrong_Type_Returns_None_With_ContentPropertyNotFoundMsg()
		{
			// Arrange

			// Act
			var result = GetPostContent<WithContentPropertyWrongType>();

			// Assert
			var none = result.AssertNone();
			Assert.IsType<Msg.ContentPropertyNotFoundMsg<WithContentPropertyWrongType>>(none);
		}

		[Fact]
		public void With_Content_Property_Returns_Content_Property()
		{
			// Arrange

			// Act
			var result = GetPostContent<WithContentProperty>();

			// Assert
			var some = result.AssertSome();
			Assert.Equal(nameof(WpPostEntity.Content), some.Name);
			Assert.Equal(typeof(string), some.PropertyType);
		}

		public sealed record class NoContentProperty;

		public sealed record class WithContentPropertyWrongType(int Content);

		public sealed record class WithContentProperty(string Content);
	}
}
