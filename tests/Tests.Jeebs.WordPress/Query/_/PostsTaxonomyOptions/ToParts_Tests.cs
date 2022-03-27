// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq.Expressions;
using Jeebs.Collections;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Data.Query.QueryOptions_Tests;
using Jeebs.Id;
using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.Enums;
using Jeebs.WordPress.Tables;

namespace Jeebs.WordPress.Query.PostsTaxonomyOptions_Tests;

public class ToParts_Tests : ToParts_Tests<PostsTaxonomyOptions, IQueryPostsTaxonomyPartsBuilder, WpTermId>
{
	protected override (PostsTaxonomyOptions options, IQueryPostsTaxonomyPartsBuilder builder) Setup()
	{
		var schema = new WpDbSchema(Rnd.Str);
		var builder = GetConfiguredBuilder(schema.Terms);
		var options = new PostsTaxonomyOptions(schema, builder);

		return (options, builder);
	}

	[Fact]
	public void Calls_Builder_AddInnerJoin()
	{
		// Arrange
		var (options, builder) = Setup();
		var t = options.TTest;
		var termId = t.Terms.Id;
		var termTaxonomyId = t.TermTaxonomies.Id;

		// Act
		options.ToParts<TestModel>();

		// Assert
		builder.Received().AddInnerJoin(
			Qp,
			t.Terms,
			Arg.Is<Expression<Func<TermsTable, string>>>(x => termId == x.Compile().Invoke(t.Terms)),
			t.TermTaxonomies,
			Arg.Is<Expression<Func<TermTaxonomiesTable, string>>>(x => termId == x.Compile().Invoke(t.TermTaxonomies))
		);
		builder.Received().AddInnerJoin(
			Qp,
			t.TermTaxonomies,
			Arg.Is<Expression<Func<TermTaxonomiesTable, string>>>(x => termTaxonomyId == x.Compile().Invoke(t.TermTaxonomies)),
			t.TermRelationships,
			Arg.Is<Expression<Func<TermRelationshipsTable, string>>>(x => termTaxonomyId == x.Compile().Invoke(t.TermRelationships))
		);
	}

	[Fact]
	public void Taxonomies_Empty_Does_Not_Call_AddWhereTaxonomies()
	{
		// Arrange
		var (options, builder) = Setup();

		// Act
		options.ToParts<TestModel>();

		// Assert
		builder.DidNotReceive().AddWhereTaxonomies(Qp, Arg.Any<IImmutableList<Taxonomy>>());
	}

	[Fact]
	public void Taxonomies_Not_Empty_Calls_AddWhereTaxonomies()
	{
		// Arrange
		var (options, builder) = Setup();
		var taxonomies = ImmutableList.Create(Taxonomy.NavMenu);
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
	public void PostIds_Empty_Does_Not_Call_Builder_AddWherePostIds()
	{
		// Arrange
		var (options, builder) = Setup();

		// Act
		options.ToParts<TestModel>();

		// Assert
		builder.DidNotReceive().AddWherePostIds(Qp, Arg.Any<IImmutableList<WpPostId>>());
	}

	[Fact]
	public void PostIds_Not_Empty_Calls_Builder_AddWherePostIds()
	{
		// Arrange
		var (options, builder) = Setup();
		var i0 = StrongId.RndId<WpPostId>();
		var i1 = StrongId.RndId<WpPostId>();
		var postIds = ImmutableList.Create(i0, i1);
		var opt = options with
		{
			PostIds = postIds
		};

		// Act
		opt.ToParts<TestModel>();

		// Assert
		builder.Received().AddWherePostIds(Qp, postIds);
	}

	[Fact]
	public void Calls_Builder_AddSort_With_SortBy()
	{
		// Arrange
		var (options, builder) = Setup();
		var sortBy = (TaxonomySort)Rnd.Int;
		var opt = options with
		{
			SortBy = sortBy
		};

		// Act
		opt.ToParts<TestModel>();

		// Assert
		builder.ReceivedWithAnyArgs().AddSort(Qp, default, Arg.Any<IImmutableList<(IColumn, SortOrder)>>(), sortBy);
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
