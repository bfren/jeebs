﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data.Query.QueryOptions_Tests;
using Jeebs.WordPress.Entities.StrongIds;

namespace Jeebs.WordPress.Query.PostsMetaOptions_Tests;

public class ToParts_Tests : ToParts_Tests<PostsMetaOptions, IQueryPostsMetaPartsBuilder, WpPostMetaId>
{
	protected override (PostsMetaOptions options, IQueryPostsMetaPartsBuilder builder) Setup()
	{
		var schema = new WpDbSchema(Rnd.Str);
		var builder = GetConfiguredBuilder(schema.PostsMeta);
		var options = new PostsMetaOptions(schema, builder);

		return (options, builder);
	}

	[Fact]
	public void PostId_Null_PostIds_Empty_Does_Not_Call_Builder_AddWherePostId()
	{
		// Arrange
		var (options, builder) = Setup();

		// Act
		_ = options.ToParts<TestModel>();

		// Assert
		_ = builder.DidNotReceiveWithAnyArgs().AddWherePostId(Qp, default, Arg.Any<IImmutableList<WpPostId>>());
	}

	[Fact]
	public void PostId_Not_Null_Calls_Builder_AddWherePostId()
	{
		// Arrange
		var (options, builder) = Setup();
		var postId = new WpPostId(Rnd.Lng);
		var opt = options with
		{
			PostId = postId
		};

		// Act
		_ = opt.ToParts<TestModel>();

		// Assert
		_ = builder.Received().AddWherePostId(Qp, postId, Arg.Any<IImmutableList<WpPostId>>());
	}

	[Fact]
	public void PostIds_Not_Empty_Calls_Builder_AddWherePostId()
	{
		// Arrange
		var (options, builder) = Setup();
		var i0 = new WpPostId(Rnd.Lng);
		var i1 = new WpPostId(Rnd.Lng);
		var postIds = ImmutableList.Create(i0, i1);
		var opt = options with
		{
			PostIds = postIds
		};

		// Act
		_ = opt.ToParts<TestModel>();

		// Assert
		_ = builder.Received().AddWherePostId(Qp, null, postIds);
	}

	[Fact]
	public void Key_Null_Does_Not_Call_Builder_AddWhereKey()
	{
		// Arrange
		var (options, builder) = Setup();

		// Act
		_ = options.ToParts<TestModel>();

		// Assert
		_ = builder.DidNotReceiveWithAnyArgs().AddWhereKey(Qp, default);
	}

	[Fact]
	public void Key_Not_Null_Calls_Builder_AddWhereKey()
	{
		// Arrange
		var (options, builder) = Setup();
		var key = Rnd.Str;
		var opt = options with
		{
			Key = key
		};

		// Act
		_ = opt.ToParts<TestModel>();

		// Assert
		_ = builder.Received().AddWhereKey(Qp, key);
	}

	[Fact]
	public override void Test00_Calls_Builder_Create_With_Maximum_And_Skip() =>
		Test00();

	[Fact]
	public override void Test01_Id_Null_Ids_Empty_Does_Not_Call_Builder_AddWhereId() =>
		Test01();

	[Fact]
	public override void Test02_Id_Not_Null_Calls_Builder_AddWhereId() =>
		Test02();

	[Fact]
	public override void Test03_Ids_Not_Empty_Calls_Builder_AddWhereId() =>
		Test03();

	[Fact]
	public override void Test04_SortRandom_False_Sort_Empty_Does_Not_Call_Builder_AddSort() =>
		Test04();

	[Fact]
	public override void Test05_SortRandom_True_Calls_Builder_AddSort() =>
		Test05();

	[Fact]
	public override void Test06_Sort_Not_Empty_Calls_Builder_AddSort() =>
		Test06();
}