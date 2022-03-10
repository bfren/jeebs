﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Querying.QueryOptions_Tests;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;
using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.Query_Tests.PostsOptions_Tests;

public class ToParts_Tests : ToParts_Tests<Query.PostsOptions, IQueryPostsPartsBuilder, WpPostId>
{
	protected override (Query.PostsOptions options, IQueryPostsPartsBuilder builder) Setup()
	{
		var schema = new WpDbSchema(F.Rnd.Str);
		var builder = GetConfiguredBuilder(schema.Post);
		var options = new Query.PostsOptions(schema, builder);

		return (options, builder);
	}

	[Fact]
	public void Calls_Builder_AddWhereType()
	{
		// Arrange
		var (options, builder) = Setup();
		var type = Enums.PostType.Attachment;
		var opt = options with
		{
			Type = type
		};

		// Act
		_ = opt.ToParts<TestModel>();

		// Assert
		_ = builder.Received().AddWhereType(Qp, type);
	}

	[Fact]
	public void Calls_Builder_AddWhereStatus()
	{
		// Arrange
		var (options, builder) = Setup();
		var status = Enums.PostStatus.Pending;
		var opt = options with
		{
			Status = status
		};

		// Act
		_ = opt.ToParts<TestModel>();

		// Assert
		_ = builder.Received().AddWhereStatus(Qp, status);
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	public void SearchText_Null_Or_Empty_Does_Not_Call_Builder_AddWhereSearch(string input)
	{
		// Arrange
		var (options, builder) = Setup();
		var opt = options with
		{
			SearchText = input
		};

		// Act
		_ = opt.ToParts<TestModel>();

		// Assert
		_ = builder.DidNotReceiveWithAnyArgs().AddWhereSearch(Qp, default, default, default);
	}

	[Fact]
	public void Calls_Builder_AddWhereSearch()
	{
		// Arrange
		var (options, builder) = Setup();
		var fields = SearchPostField.Excerpt;
		var cmp = Compare.Like;
		var text = F.Rnd.Str;
		var opt = options with
		{
			SearchFields = fields,
			SearchComparison = cmp,
			SearchText = text
		};

		// Act
		_ = opt.ToParts<TestModel>();

		// Assert
		_ = builder.Received().AddWhereSearch(Qp, fields, cmp, text);
	}

	[Fact]
	public void From_Null_Does_Not_Call_Builder_AddWherePublishedFrom()
	{
		// Arrange
		var (options, builder) = Setup();

		// Act
		_ = options.ToParts<TestModel>();

		// Assert
		_ = builder.DidNotReceiveWithAnyArgs().AddWherePublishedFrom(Qp, default);
	}

	[Fact]
	public void Calls_Builder_AddWherePublishedFrom()
	{
		// Arrange
		var (options, builder) = Setup();
		var from = F.Rnd.DateTime;
		var opt = options with
		{
			FromDate = from
		};

		// Act
		_ = opt.ToParts<TestModel>();

		// Assert
		_ = builder.Received().AddWherePublishedFrom(Qp, from);
	}

	[Fact]
	public void To_Null_Does_Not_Call_Builder_AddWherePublishedTo()
	{
		// Arrange
		var (options, builder) = Setup();

		// Act
		_ = options.ToParts<TestModel>();

		// Assert
		_ = builder.DidNotReceiveWithAnyArgs().AddWherePublishedTo(Qp, default);
	}

	[Fact]
	public void Calls_Builder_AddWherePublishedTo()
	{
		// Arrange
		var (options, builder) = Setup();
		var to = F.Rnd.DateTime;
		var opt = options with
		{
			ToDate = to
		};

		// Act
		_ = opt.ToParts<TestModel>();

		// Assert
		_ = builder.Received().AddWherePublishedTo(Qp, to);
	}

	[Fact]
	public void ParentId_Null_Does_Not_Call_Builder_AddWhereParentId()
	{
		// Arrange
		var (options, builder) = Setup();

		// Act
		_ = options.ToParts<TestModel>();

		// Assert
		_ = builder.DidNotReceiveWithAnyArgs().AddWhereParentId(Qp, default);
	}

	[Fact]
	public void Calls_Builder_AddWhereParentId()
	{
		// Arrange
		var (options, builder) = Setup();
		var parentId = new WpPostId(F.Rnd.Lng);
		var opt = options with
		{
			ParentId = parentId
		};

		// Act
		_ = opt.ToParts<TestModel>();

		// Assert
		_ = builder.Received().AddWhereParentId(Qp, parentId);
	}

	[Fact]
	public void Taxonomies_Empty_Does_Not_Call_Builder_AddWhereTaxonomies()
	{
		// Arrange
		var (options, builder) = Setup();

		// Act
		_ = options.ToParts<TestModel>();

		// Assert
		_ = builder.DidNotReceiveWithAnyArgs().AddWhereTaxonomies(Qp, Arg.Any<IImmutableList<(Taxonomy, WpTermId)>>());
	}

	[Fact]
	public void Calls_Builder_AddWhereTaxonomies()
	{
		// Arrange
		var (options, builder) = Setup();
		var taxonomies = ImmutableList.Create(
			(Taxonomy.LinkCategory, new WpTermId(F.Rnd.Lng))
		);
		var opt = options with
		{
			Taxonomies = taxonomies
		};

		// Act
		_ = opt.ToParts<TestModel>();

		// Assert
		_ = builder.Received().AddWhereTaxonomies(Qp, taxonomies);
	}

	[Fact]
	public void CustomFields_Empty_Does_Not_Call_Builder_AddWhereCustomFields()
	{
		// Arrange
		var (options, builder) = Setup();

		// Act
		_ = options.ToParts<TestModel>();

		// Assert
		_ = builder.DidNotReceiveWithAnyArgs().AddWhereCustomFields(Qp, Arg.Any<IImmutableList<(ICustomField, Compare, object)>>());
	}

	[Fact]
	public void Calls_Builder_AddWhereCustomFields()
	{
		// Arrange
		var (options, builder) = Setup();
		var fields = ImmutableList.Create(
			(Substitute.For<ICustomField>(), Compare.Like, (object)F.Rnd.Guid)
		);
		var opt = options with
		{
			CustomFields = fields
		};

		// Act
		_ = opt.ToParts<TestModel>();

		// Assert
		_ = builder.Received().AddWhereCustomFields(Qp, fields);
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