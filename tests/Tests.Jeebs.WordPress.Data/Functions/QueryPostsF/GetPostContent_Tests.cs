// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.WordPress.Data.Entities;
using Microsoft.VisualBasic.FileIO;
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
			var result = GetPostContent<ContentPropertyWrongType>();

			// Assert
			var none = result.AssertNone();
			Assert.IsType<Msg.ContentPropertyNotFoundMsg<ContentPropertyWrongType>>(none);
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

		public sealed record NoContentProperty;

		public sealed record ContentPropertyWrongType(int Content);

		public sealed record WithContentProperty(string Content);
	}
}
