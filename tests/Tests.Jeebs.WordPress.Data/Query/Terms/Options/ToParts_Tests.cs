// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq.Expressions;
using Jeebs.Data.Querying.QueryOptions_Tests;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;
using Jeebs.WordPress.Data.Tables;
using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.Query_Tests.TermsOptions_Tests
{
	public class ToParts_Tests : ToParts_Tests<Query.TermsOptions, IQueryTermsPartsBuilder, WpTermId>
	{
		protected override (Query.TermsOptions options, IQueryTermsPartsBuilder builder) Setup()
		{
			var schema = new WpDbSchema(F.Rnd.Str);
			var builder = GetConfiguredBuilder(schema.Term);
			var options = new Query.TermsOptions(schema, builder);

			return (options, builder);
		}

		[Fact]
		public void Calls_Builder_AddInnerJoin()
		{
			// Arrange
			var (options, builder) = Setup();
			var t = options.TTest;
			var termId = t.Term.Id;

			// Act
			options.ToParts<TestModel>();

			// Assert
			builder.Received().AddInnerJoin(
				Qp,
				t.Term,
				Arg.Is<Expression<Func<TermTable, string>>>(x => termId == x.Compile().Invoke(t.Term)),
				t.TermTaxonomy,
				Arg.Is<Expression<Func<TermTaxonomyTable, string>>>(x => termId == x.Compile().Invoke(t.TermTaxonomy))
			);
		}

		[Fact]
		public void Taxonomy_Null_Does_Not_Call_AddWhereTaxonomy()
		{
			// Arrange
			var (options, builder) = Setup();

			// Act
			options.ToParts<TestModel>();

			// Assert
			builder.DidNotReceiveWithAnyArgs().AddWhereTaxonomy(Qp, default);
		}

		[Fact]
		public void Taxonomy_Not_Null_Calls_Add_Where_Taxonomy()
		{
			// Arrange
			var (options, builder) = Setup();
			var taxonomy = Taxonomy.NavMenu;
			var opt = options with
			{
				Taxonomy = taxonomy
			};

			// Act
			opt.ToParts<TestModel>();

			// Assert
			builder.Received().AddWhereTaxonomy(Qp, taxonomy);
		}

		[Fact]
		public void Slug_Null_Does_Not_Call_AddWhereSlug()
		{
			// Arrange
			var (options, builder) = Setup();

			// Act
			options.ToParts<TestModel>();

			// Assert
			builder.DidNotReceiveWithAnyArgs().AddWhereSlug(Qp, default);
		}

		[Fact]
		public void Slug_Not_Null_Calls_Add_Where_Slug()
		{
			// Arrange
			var (options, builder) = Setup();
			var slug = F.Rnd.Str;
			var opt = options with
			{
				Slug = slug
			};

			// Act
			opt.ToParts<TestModel>();

			// Assert
			builder.Received().AddWhereSlug(Qp, slug);
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(0)]
		public void CountAtLeast_Less_Than_Or_Equal_To_Zero_Does_Not_Call_Builder_AddWhereCount(long input)
		{
			// Arrange
			var (options, builder) = Setup();
			var opt = options with
			{
				CountAtLeast = input
			};

			// Act
			opt.ToParts<TestModel>();

			// Assert
			builder.DidNotReceiveWithAnyArgs().AddWhereCount(Qp, default);
		}

		[Fact]
		public void CountAtLeast_Greater_Than_Zero_Calls_Builder_AddWhereCount()
		{
			// Arrange
			var (options, builder) = Setup();
			var count = F.Rnd.Lng;
			var opt = options with
			{
				CountAtLeast = count
			};

			// Act
			opt.ToParts<TestModel>();

			// Assert
			builder.Received().AddWhereCount(Qp, count);
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
}
