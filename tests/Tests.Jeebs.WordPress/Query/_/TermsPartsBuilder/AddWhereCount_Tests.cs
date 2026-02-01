// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities.Ids;
using static Jeebs.WordPress.Query.TermsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.TermsPartsBuilder_Tests;

public class AddWhereCount_Tests : QueryPartsBuilder_Tests<TermsPartsBuilder, WpTermId>
{
	protected override TermsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Theory]
	[InlineData(0)]
	[InlineData(-1)]
	public void Invalid_Count_Does_Nothing(long input)
	{
		// Arrange
		var (builder, v) = Setup();

		// Act
		var result = builder.AddWhereCount(v.Parts, input);

		// Assert
		var ok = result.AssertOk();
		Assert.Same(v.Parts, ok);
	}

	[Fact]
	public void Adds_Count_To_Where()
	{
		// Arrange
		var (builder, v) = Setup();
		var count = Rnd.NumberF.GetInt64(1, 1000);

		// Act
		var result = builder.AddWhereCount(v.Parts, count);

		// Assert
		var ok = result.AssertOk();
		Assert.NotSame(v.Parts, ok);
		var (column, compare, value) = Assert.Single(ok.Where);
		Assert.Equal(builder.TTest.TermTaxonomies.GetName(), column.TblName);
		Assert.Equal(builder.TTest.TermTaxonomies.Count, column.ColName);
		Assert.Equal(Compare.MoreThanOrEqual, compare);
		Assert.Equal(count, value);
	}
}
