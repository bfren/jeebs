// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.ContentFilters;

namespace Jeebs.WordPress.Functions.QueryPostsF_Tests;

public class ExecuteContentFilters_Tests
{
	[Fact]
	public void No_Posts_Does_Nothing()
	{
		// Arrange
		var posts = Substitute.For<IEnumerable<Model>>();
		var content = QueryPostsF.GetPostContentInfo<Model>().Unsafe().Unwrap();
		var filters = new[] { Substitute.For<IContentFilter>() };

		// Act
		var result = QueryPostsF.ExecuteContentFilters(posts, content, filters);

		// Assert
		Assert.Equal(posts, result);
	}

	[Fact]
	public void No_Filters_Does_Nothing()
	{
		// Arrange
		var posts = new[] { new Model(Rnd.Str) };
		var content = QueryPostsF.GetPostContentInfo<Model>().Unsafe().Unwrap();
		var filters = Array.Empty<IContentFilter>();

		// Act
		var result = QueryPostsF.ExecuteContentFilters(posts, content, filters);

		// Assert
		Assert.Equal(posts, result);
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

		var content = QueryPostsF.GetPostContentInfo<Model>().Unsafe().Unwrap();

		var f0 = Substitute.For<IContentFilter>();
		f0.Execute(Arg.Any<string>()).Returns(x => x.ArgAt<string>(0));

		var f1 = Substitute.For<IContentFilter>();
		f1.Execute(Arg.Any<string>()).Returns(x => x.ArgAt<string>(0));

		var filters = new[] { f0, f1 };

		// Act
		QueryPostsF.ExecuteContentFilters(posts, content, filters);

		// Assert
		f0.Received(1).Execute(c0);
		f1.Received(1).Execute(c0);
		f0.Received(1).Execute(c1);
		f1.Received(1).Execute(c1);
	}

	public sealed record class Model(string Content);
}
