// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.TermsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.TermsPartsBuilder_Tests;

public class AddWhereCount_Tests : QueryPartsBuilder_Tests<Query.TermsPartsBuilder, WpTermId>
{
	protected override Query.TermsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
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
		var some = result.AssertSome();
		Assert.Same(v.Parts, some);
	}

	[Fact]
	public void Adds_Count_To_Where()
	{
		// Arrange
		var (builder, v) = Setup();
		var count = F.Rnd.NumberF.GetInt64(1, 1000);

		// Act
		var result = builder.AddWhereCount(v.Parts, count);

		// Assert
		var some = result.AssertSome();
		Assert.NotSame(v.Parts, some);
		Assert.Collection(some.Where,
			x =>
			{
				Assert.Equal(builder.TTest.TermTaxonomy.GetName(), x.column.Table);
				Assert.Equal(builder.TTest.TermTaxonomy.Count, x.column.Name);
				Assert.Equal(Compare.MoreThanOrEqual, x.cmp);
				Assert.Equal(count, x.value);
			}
		);
	}
}
