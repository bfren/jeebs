// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using Jeebs;
using Jeebs.WordPress.Data;
using NSubstitute;
using Xunit;
using static F.WordPressF.DataF.QueryPostsF;

namespace F.WordPressF.DataF.QueryPostsF_Tests
{
	public class ExecuteContentFilters_Tests
	{
		[Fact]
		public void No_Posts_Does_Nothing()
		{
			// Arrange
			var posts = Substitute.For<IEnumerable<Model>>();
			var content = GetPostContentInfo<Model>().UnsafeUnwrap();
			var filters = new[] { Substitute.For<IContentFilter>() };

			// Act
			var result = ExecuteContentFilters(posts, content, filters);

			// Assert
			Assert.Same(posts, result);
		}

		[Fact]
		public void No_Filters_Does_Nothing()
		{
			// Arrange
			var posts = new[] { new Model(Rnd.Str) };
			var content = GetPostContentInfo<Model>().UnsafeUnwrap();
			var filters = Array.Empty<IContentFilter>();

			// Act
			var result = ExecuteContentFilters(posts, content, filters);

			// Assert
			Assert.Same(posts, result);
		}

		[Fact]
		public void Executes_Filters_On_Each_Post()
		{
			// Arrange
			var c0 = Rnd.Str;
			var p0 = new Model(c0);
			var c1 = Rnd.Str;
			var p1 = new Model(c1);
			var posts = new[] { p0, p1 };

			var content = GetPostContentInfo<Model>().UnsafeUnwrap();

			var f0 = Substitute.For<IContentFilter>();
			f0.Execute(Arg.Any<string>()).Returns(x => x.ArgAt<string>(0));

			var f1 = Substitute.For<IContentFilter>();
			f1.Execute(Arg.Any<string>()).Returns(x => x.ArgAt<string>(0));

			var filters = new[] { f0, f1 };

			// Act
			_ = ExecuteContentFilters(posts, content, filters);

			// Assert
			f0.Received(1).Execute(c0);
			f1.Received(1).Execute(c0);
			f0.Received(1).Execute(c1);
			f1.Received(1).Execute(c1);
		}

		public sealed record Model(string Content);
	}
}
