// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities.StrongIds;
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
		var some = result.AssertSome();
		Assert.Same(v.Parts, some);
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
		var some = result.AssertSome();
		Assert.NotSame(v.Parts, some);
		Assert.Collection(some.Where,
			x =>
			{
				Assert.Equal(builder.TTest.TermTaxonomies.GetName(), x.column.TblName);
				Assert.Equal(builder.TTest.TermTaxonomies.Count, x.column.ColName);
				Assert.Equal(Compare.MoreThanOrEqual, x.compare);
				Assert.Equal(count, x.value);
			}
		);
	}
}
