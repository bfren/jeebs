// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq.Expressions;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying.QueryOptions_Tests;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;
using Jeebs.WordPress.Data.Tables;
using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.Query_Tests.PostsTaxonomyOptions_Tests;

public class ToParts_Tests : ToParts_Tests<Query.PostsTaxonomyOptions, IQueryPostsTaxonomyPartsBuilder, WpTermId>
{
	protected override (Query.PostsTaxonomyOptions options, IQueryPostsTaxonomyPartsBuilder builder) Setup()
	{
		var schema = new WpDbSchema(F.Rnd.Str);
		var builder = GetConfiguredBuilder(schema.Term);
		var options = new Query.PostsTaxonomyOptions(schema, builder);

		return (options, builder);
	}

	[Fact]
	public void Calls_Builder_AddInnerJoin()
	{
		// Arrange
		var (options, builder) = Setup();
		var t = options.TTest;
		var termId = t.Term.Id;
		var termTaxonomyId = t.TermTaxonomy.Id;

		// Act
		_ = options.ToParts<TestModel>();

		// Assert
		_ = builder.Received().AddInnerJoin(
			Qp,
			t.Term,
			Arg.Is<Expression<Func<TermTable, string>>>(x => termId == x.Compile().Invoke(t.Term)),
			t.TermTaxonomy,
			Arg.Is<Expression<Func<TermTaxonomyTable, string>>>(x => termId == x.Compile().Invoke(t.TermTaxonomy))
		);
		_ = builder.Received().AddInnerJoin(
			Qp,
			t.TermTaxonomy,
			Arg.Is<Expression<Func<TermTaxonomyTable, string>>>(x => termTaxonomyId == x.Compile().Invoke(t.TermTaxonomy)),
			t.TermRelationship,
			Arg.Is<Expression<Func<TermRelationshipTable, string>>>(x => termTaxonomyId == x.Compile().Invoke(t.TermRelationship))
		);
	}

	[Fact]
	public void Taxonomies_Empty_Does_Not_Call_AddWhereTaxonomies()
	{
		// Arrange
		var (options, builder) = Setup();

		// Act
		_ = options.ToParts<TestModel>();

		// Assert
		_ = builder.DidNotReceive().AddWhereTaxonomies(Qp, Arg.Any<IImmutableList<Taxonomy>>());
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
		_ = opt.ToParts<TestModel>();

		// Assert
		_ = builder.Received().AddWhereTaxonomies(Qp, taxonomies);
	}

	[Fact]
	public void PostIds_Empty_Does_Not_Call_Builder_AddWherePostIds()
	{
		// Arrange
		var (options, builder) = Setup();

		// Act
		_ = options.ToParts<TestModel>();

		// Assert
		_ = builder.DidNotReceive().AddWherePostIds(Qp, Arg.Any<IImmutableList<WpPostId>>());
	}

	[Fact]
	public void PostIds_Not_Empty_Calls_Builder_AddWherePostIds()
	{
		// Arrange
		var (options, builder) = Setup();
		var i0 = new WpPostId(F.Rnd.Lng);
		var i1 = new WpPostId(F.Rnd.Lng);
		var postIds = ImmutableList.Create(i0, i1);
		var opt = options with
		{
			PostIds = postIds
		};

		// Act
		_ = opt.ToParts<TestModel>();

		// Assert
		_ = builder.Received().AddWherePostIds(Qp, postIds);
	}

	[Fact]
	public void Calls_Builder_AddSort_With_SortBy()
	{
		// Arrange
		var (options, builder) = Setup();
		var sortBy = (TaxonomySort)F.Rnd.Int;
		var opt = options with
		{
			SortBy = sortBy
		};

		// Act
		_ = opt.ToParts<TestModel>();

		// Assert
		_ = builder.ReceivedWithAnyArgs().AddSort(Qp, default, Arg.Any<IImmutableList<(IColumn, SortOrder)>>(), sortBy);
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
