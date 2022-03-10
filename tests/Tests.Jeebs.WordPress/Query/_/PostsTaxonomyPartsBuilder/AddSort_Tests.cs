// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.Enums;
using Jeebs.WordPress.Tables;
using static Jeebs.WordPress.Query.PostsTaxonomyPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.PostsTaxonomyPartsBuilder_Tests;

public class AddSort_Tests : AddSort_Tests<PostsTaxonomyPartsBuilder, WpTermId>
{
	protected override PostsTaxonomyPartsBuilder GetConfiguredBuilder(IExtract extract) =>
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
				Assert.Equal(builder.TTest.Terms.GetName(), x.column.TblName);
				Assert.Equal(builder.TTest.Terms.Title, x.column.ColName);
				Assert.Equal(nameof(TermsTable.Title), x.column.ColAlias);
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
				Assert.Equal(builder.TTest.TermTaxonomies.GetName(), x.column.TblName);
				Assert.Equal(builder.TTest.TermTaxonomies.Count, x.column.ColName);
				Assert.Equal(nameof(TermTaxonomiesTable.Count), x.column.ColAlias);
				Assert.Equal(SortOrder.Descending, x.order);
			},
			x =>
			{
				Assert.Equal(builder.TTest.Terms.GetName(), x.column.TblName);
				Assert.Equal(builder.TTest.Terms.Title, x.column.ColName);
				Assert.Equal(nameof(TermsTable.Title), x.column.ColAlias);
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
