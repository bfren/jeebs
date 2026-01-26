// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.ContentFilters;
using Jeebs.WordPress.Entities.Ids;
using NSubstitute.ExceptionExtensions;

namespace Jeebs.WordPress.Functions.QueryPostsF_Tests;

public class ApplyContentFilters_Tests
{
	[Fact]
	public void No_Posts_Does_Nothing()
	{
		// Arrange
		var posts = Substitute.For<IEnumerable<Model>>();
		var filters = new[] { Substitute.For<IContentFilter>() };

		// Act
		var result = QueryPostsF.ApplyContentFilters<IEnumerable<Model>, Model>(posts, filters);

		// Assert
		var ok = result.AssertOk();
		Assert.Equal(posts, ok);
	}

	[Fact]
	public void No_Filters_Does_Nothing()
	{
		// Arrange
		var posts = new[] { new Model(Rnd.ULng, Rnd.Str) };
		var filters = Array.Empty<IContentFilter>();

		// Act
		var result = QueryPostsF.ApplyContentFilters<IEnumerable<Model>, Model>(posts, filters);

		// Assert
		var ok = result.AssertOk();
		Assert.Equal(posts, ok);
	}

	[Fact]
	public void Catches_Exception_In_ContentFilter_Returns_None_With_ApplyContentFiltersExceptionMsg()
	{
		// Arrange
		var posts = new[] { new Model(Rnd.ULng, Rnd.Str) };
		var filter = Substitute.For<IContentFilter>();
		var ex = new Exception();
		filter.Execute(Arg.Any<string>()).Throws(ex);
		var filters = new[] { filter };

		// Act
		var result = QueryPostsF.ApplyContentFilters<IEnumerable<Model>, Model>(posts, filters);

		// Assert
		_ = result.AssertFailure(ex);
	}

	[Fact]
	public void Applies_Filters_To_Each_Post()
	{
		// Arrange
		var c0 = Rnd.Str;
		var p0 = new Model(Rnd.ULng, c0);
		var c1 = Rnd.Str;
		var p1 = new Model(Rnd.ULng, c1);
		var posts = new[] { p0, p1 };

		var f0 = Substitute.For<IContentFilter>();
		f0.Execute(Arg.Any<string>()).Returns(x => x.ArgAt<string>(0));

		var f1 = Substitute.For<IContentFilter>();
		f1.Execute(Arg.Any<string>()).Returns(x => x.ArgAt<string>(0));

		var filters = new[] { f0, f1 };

		// Act
		QueryPostsF.ApplyContentFilters<IEnumerable<Model>, Model>(posts, filters);

		// Assert
		f0.Received(1).Execute(c0);
		f1.Received(1).Execute(c0);
		f0.Received(1).Execute(c1);
		f1.Received(1).Execute(c1);
	}

	public sealed record class Model : WithId<WpPostId, ulong>
	{
		public string Content { get; init; }

		public Model(ulong value, string content) : base(value) =>
			Content = content;
	}
}
