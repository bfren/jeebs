// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;
using Jeebs.WordPress.Data.Tables;
using NSubstitute;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.PostsTaxonomyPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.PostsTaxonomyPartsBuilder_Tests
{
	public class AddSort_Tests : AddSort_Tests<Query.PostsTaxonomyPartsBuilder, WpTermId>
	{
		protected override Query.PostsTaxonomyPartsBuilder GetConfiguredBuilder(IExtract extract) =>
			GetBuilder(extract);

		[Fact]
		public void SortBy_TitleAscending_Adds_Sort_Title_Ascending()
		{
			// Arrange
			var (builder, v) = Setup();
			var sortBy = TaxonomySort.TitleAscending;

			// Act
			var result = builder.AddSort(v.Parts, false, Substitute.For<IImmutableList<(IColumn, SortOrder)>>(), sortBy);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Collection(some.Sort,
				x =>
				{
					Assert.Equal(builder.TTest.Term.GetName(), x.column.Table);
					Assert.Equal(builder.TTest.Term.Title, x.column.Name);
					Assert.Equal(nameof(TermTable.Title), x.column.Alias);
					Assert.Equal(SortOrder.Ascending, x.order);
				}
			);
		}

		[Fact]
		public void SortBy_CountDescending_Adds_Sort_Count_Descending_Title_Ascending()
		{
			// Arrange
			var (builder, v) = Setup();
			var sortBy = TaxonomySort.CountDescending;

			// Act
			var result = builder.AddSort(v.Parts, false, Substitute.For<IImmutableList<(IColumn, SortOrder)>>(), sortBy);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Collection(some.Sort,
				x =>
				{
					Assert.Equal(builder.TTest.TermTaxonomy.GetName(), x.column.Table);
					Assert.Equal(builder.TTest.TermTaxonomy.Count, x.column.Name);
					Assert.Equal(nameof(TermTaxonomyTable.Count), x.column.Alias);
					Assert.Equal(SortOrder.Descending, x.order);
				},
				x =>
				{
					Assert.Equal(builder.TTest.Term.GetName(), x.column.Table);
					Assert.Equal(builder.TTest.Term.Title, x.column.Name);
					Assert.Equal(nameof(TermTable.Title), x.column.Alias);
					Assert.Equal(SortOrder.Ascending, x.order);
				}
			);
		}


		[Fact]
		public override void Test00_SortRandom_True_Returns_New_Parts_With_SortRandom_True() =>
			Test00();

		[Fact]
		public override void Test01_SortRandom_False_With_Sort_Returns_New_Parts_With_Sort() =>
			Test01();

		[Fact]
		public override void Test02_SortRandom_False_And_Sort_Empty_Returns_Original_Parts() =>
			Test02();
	}
}
