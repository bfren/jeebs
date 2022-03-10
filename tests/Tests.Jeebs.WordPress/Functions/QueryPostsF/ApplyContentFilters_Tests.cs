// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using Jeebs;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;
using static F.WordPressF.DataF.QueryPostsF;

namespace F.WordPressF.DataF.QueryPostsF_Tests;

public class ApplyContentFilters_Tests
{
	[Fact]
	public void No_Posts_Does_Nothing()
	{
		// Arrange
		var posts = Substitute.For<IEnumerable<Model>>();
		var filters = new[] { Substitute.For<IContentFilter>() };

		// Act
		var result = ApplyContentFilters<IEnumerable<Model>, Model>(posts, filters);

		// Assert
		var some = result.AssertSome();
		Assert.Same(posts, some);
	}

	[Fact]
	public void No_Filters_Does_Nothing()
	{
		// Arrange
		var posts = new[] { new Model(new(Rnd.Lng), Rnd.Str) };
		var filters = Array.Empty<IContentFilter>();

		// Act
		var result = ApplyContentFilters<IEnumerable<Model>, Model>(posts, filters);

		// Assert
		var some = result.AssertSome();
		Assert.Same(posts, some);
	}

	[Fact]
	public void Catches_Exception_In_ContentFilter_Returns_None_With_ApplyContentFiltersExceptionMsg()
	{
		// Arrange
		var posts = new[] { new Model(new(Rnd.Lng), Rnd.Str) };
		var filter = Substitute.For<IContentFilter>();
		_ = filter.Execute(Arg.Any<string>()).Throws(new Exception());
		var filters = new[] { filter };

		// Act
		var result = ApplyContentFilters<IEnumerable<Model>, Model>(posts, filters);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<M.ApplyContentFiltersExceptionMsg<Model>>(none);
	}

	[Fact]
	public void Applies_Filters_To_Each_Post()
	{
		// Arrange
		var c0 = Rnd.Str;
		var p0 = new Model(new(Rnd.Lng), c0);
		var c1 = Rnd.Str;
		var p1 = new Model(new(Rnd.Lng), c1);
		var posts = new[] { p0, p1 };

		var f0 = Substitute.For<IContentFilter>();
		_ = f0.Execute(Arg.Any<string>()).Returns(x => x.ArgAt<string>(0));

		var f1 = Substitute.For<IContentFilter>();
		_ = f1.Execute(Arg.Any<string>()).Returns(x => x.ArgAt<string>(0));

		var filters = new[] { f0, f1 };

		// Act
		_ = ApplyContentFilters<IEnumerable<Model>, Model>(posts, filters);

		// Assert
		_ = f0.Received(1).Execute(c0);
		_ = f1.Received(1).Execute(c0);
		_ = f0.Received(1).Execute(c1);
		_ = f1.Received(1).Execute(c1);
	}

	public sealed record class Model(WpPostId Id, string Content) : IWithId<WpPostId>;
}
