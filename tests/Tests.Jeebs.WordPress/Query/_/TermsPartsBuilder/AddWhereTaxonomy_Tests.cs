// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.Enums;
using static Jeebs.WordPress.Query.TermsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.TermsPartsBuilder_Tests;

public class AddWhereTaxonomy_Tests : QueryPartsBuilder_Tests<TermsPartsBuilder, WpTermId>
{
	protected override TermsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public void Null_Taxonomy_Does_Nothing()
	{
		// Arrange
		var (builder, v) = Setup();

		// Act
		var result = builder.AddWhereTaxonomy(v.Parts, null);

		// Assert
		var some = result.AssertSome();
		Assert.Same(v.Parts, some);
	}

	[Fact]
	public void Adds_Taxonomy_To_Where()
	{
		// Arrange
		var (builder, v) = Setup();
		var taxonomy = new Taxonomy(Rnd.Str);

		// Act
		var result = builder.AddWhereTaxonomy(v.Parts, taxonomy);

		// Assert
		var some = result.AssertSome();
		Assert.NotSame(v.Parts, some);
		var (column, compare, value) = Assert.Single(some.Where);
		Assert.Equal(builder.TTest.TermTaxonomies.GetName(), column.TblName);
		Assert.Equal(builder.TTest.TermTaxonomies.Taxonomy, column.ColName);
		Assert.Equal(Compare.Equal, compare);
		Assert.Equal(taxonomy, value);
	}
}
