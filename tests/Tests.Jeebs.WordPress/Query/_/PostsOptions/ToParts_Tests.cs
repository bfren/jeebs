// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data.Enums;
using Jeebs.Data.Query.QueryOptions_Tests;
using Jeebs.WordPress.CustomFields;
using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.Enums;
using static StrongId.Testing.Generator;

namespace Jeebs.WordPress.Query.PostsOptions_Tests;

public class ToParts_Tests : ToParts_Tests<PostsOptions, IQueryPostsPartsBuilder, WpPostId>
{
	protected override (PostsOptions options, IQueryPostsPartsBuilder builder) Setup()
	{
		var schema = new WpDbSchema(Rnd.Str);
		var builder = GetConfiguredBuilder(schema.Posts);
		var options = new PostsOptions(schema, builder);

		return (options, builder);
	}

	[Fact]
	public void Calls_Builder_AddWhereType()
	{
		// Arrange
		var (options, builder) = Setup();
		var type = PostType.Attachment;
		var opt = options with
		{
			Type = type
		};

		// Act
		opt.ToParts<TestModel>();

		// Assert
		builder.Received().AddWhereType(Qp, type);
	}

	[Fact]
	public void Calls_Builder_AddWhereStatus()
	{
		// Arrange
		var (options, builder) = Setup();
		var status = PostStatus.Pending;
		var opt = options with
		{
			Status = status
		};

		// Act
		opt.ToParts<TestModel>();

		// Assert
		builder.Received().AddWhereStatus(Qp, status);
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
		opt.ToParts<TestModel>();

		// Assert
		builder.DidNotReceiveWithAnyArgs().AddWhereSearch(Qp, default, default, default);
	}

	[Fact]
	public void Calls_Builder_AddWhereSearch()
	{
		// Arrange
		var (options, builder) = Setup();
		var fields = SearchPostField.Excerpt;
		var cmp = Compare.Like;
		var text = Rnd.Str;
		var opt = options with
		{
			SearchFields = fields,
			SearchComparison = cmp,
			SearchText = text
		};

		// Act
		opt.ToParts<TestModel>();

		// Assert
		builder.Received().AddWhereSearch(Qp, fields, cmp, text);
	}

	[Fact]
	public void From_Null_Does_Not_Call_Builder_AddWherePublishedFrom()
	{
		// Arrange
		var (options, builder) = Setup();

		// Act
		options.ToParts<TestModel>();

		// Assert
		builder.DidNotReceiveWithAnyArgs().AddWherePublishedFrom(Qp, default);
	}

	[Fact]
	public void Calls_Builder_AddWherePublishedFrom()
	{
		// Arrange
		var (options, builder) = Setup();
		var from = Rnd.DateTime;
		var opt = options with
		{
			FromDate = from
		};

		// Act
		opt.ToParts<TestModel>();

		// Assert
		builder.Received().AddWherePublishedFrom(Qp, from);
	}

	[Fact]
	public void To_Null_Does_Not_Call_Builder_AddWherePublishedTo()
	{
		// Arrange
		var (options, builder) = Setup();

		// Act
		options.ToParts<TestModel>();

		// Assert
		builder.DidNotReceiveWithAnyArgs().AddWherePublishedTo(Qp, default);
	}

	[Fact]
	public void Calls_Builder_AddWherePublishedTo()
	{
		// Arrange
		var (options, builder) = Setup();
		var to = Rnd.DateTime;
		var opt = options with
		{
			ToDate = to
		};

		// Act
		opt.ToParts<TestModel>();

		// Assert
		builder.Received().AddWherePublishedTo(Qp, to);
	}

	[Fact]
	public void ParentId_Null_Does_Not_Call_Builder_AddWhereParentId()
	{
		// Arrange
		var (options, builder) = Setup();

		// Act
		options.ToParts<TestModel>();

		// Assert
		builder.DidNotReceiveWithAnyArgs().AddWhereParentId(Qp, default);
	}

	[Fact]
	public void Calls_Builder_AddWhereParentId()
	{
		// Arrange
		var (options, builder) = Setup();
		var parentId = LongId<WpPostId>();
		var opt = options with
		{
			ParentId = parentId
		};

		// Act
		opt.ToParts<TestModel>();

		// Assert
		builder.Received().AddWhereParentId(Qp, parentId);
	}

	[Fact]
	public void Taxonomies_Empty_Does_Not_Call_Builder_AddWhereTaxonomies()
	{
		// Arrange
		var (options, builder) = Setup();

		// Act
		options.ToParts<TestModel>();

		// Assert
		builder.DidNotReceiveWithAnyArgs().AddWhereTaxonomies(Qp, Arg.Any<IImmutableList<(Taxonomy, WpTermId)>>());
	}

	[Fact]
	public void Calls_Builder_AddWhereTaxonomies()
	{
		// Arrange
		var (options, builder) = Setup();
		var taxonomies = ImmutableList.Create(
			(Taxonomy.LinkCategory, LongId<WpTermId>())
		);
		var opt = options with
		{
			Taxonomies = taxonomies
		};

		// Act
		opt.ToParts<TestModel>();

		// Assert
		builder.Received().AddWhereTaxonomies(Qp, taxonomies);
	}

	[Fact]
	public void CustomFields_Empty_Does_Not_Call_Builder_AddWhereCustomFields()
	{
		// Arrange
		var (options, builder) = Setup();

		// Act
		options.ToParts<TestModel>();

		// Assert
		builder.DidNotReceiveWithAnyArgs().AddWhereCustomFields(Qp, Arg.Any<IImmutableList<(ICustomField, Compare, object)>>());
	}

	[Fact]
	public void Calls_Builder_AddWhereCustomFields()
	{
		// Arrange
		var (options, builder) = Setup();
		var fields = ImmutableList.Create(
			(Substitute.For<ICustomField>(), Compare.Like, (object)Rnd.Guid)
		);
		var opt = options with
		{
			CustomFields = fields
		};

		// Act
		opt.ToParts<TestModel>();

		// Assert
		builder.Received().AddWhereCustomFields(Qp, fields);
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
