// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Tables;
using NSubstitute;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.TermsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.TermsPartsBuilder_Tests
{
	public class AddSort_Tests : AddSort_Tests<Query.TermsPartsBuilder, WpTermId>
	{
		protected override Query.TermsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
			GetBuilder(extract);

		[Theory]
		[InlineData(true)]
		[InlineData(false)]
		public void Ignores_Base_Sort_Adds_Title_Ascending_Count_Descending(bool input)
		{
			// Arrange
			var (builder, v) = Setup();

			// Act
			var result = builder.AddSort(v.Parts, input, Substitute.For<IImmutableList<(IColumn, SortOrder)>>());

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
				},
				x =>
				{
					Assert.Equal(builder.TTest.TermTaxonomy.GetName(), x.column.Table);
					Assert.Equal(builder.TTest.TermTaxonomy.Count, x.column.Name);
					Assert.Equal(nameof(TermTaxonomyTable.Count), x.column.Alias);
					Assert.Equal(SortOrder.Descending, x.order);
				}
			);
		}

		[Fact]
		public override void Test00_SortRandom_True_Returns_New_Parts_With_SortRandom_True() { }

		[Fact]
		public override void Test01_SortRandom_False_With_Sort_Returns_New_Parts_With_Sort() { }

		[Fact]
		public override void Test02_SortRandom_False_And_Sort_Empty_Returns_Original_Parts() { }
	}
}
