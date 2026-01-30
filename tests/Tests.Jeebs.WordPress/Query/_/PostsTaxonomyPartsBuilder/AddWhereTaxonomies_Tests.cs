// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.Functions;
using Jeebs.WordPress.Entities.Ids;
using Jeebs.WordPress.Enums;
using static Jeebs.WordPress.Query.PostsTaxonomyPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.PostsTaxonomyPartsBuilder_Tests;

public class AddWhereTaxonomies_Tests : QueryPartsBuilder_Tests<PostsTaxonomyPartsBuilder, WpTermId>
{
	protected override PostsTaxonomyPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public void Empty_Taxonomies_Does_Nothing()
	{
		// Arrange
		var (builder, v) = Setup();

		// Act
		var result = builder.AddWhereTaxonomies(v.Parts, Substitute.For<IImmutableList<Taxonomy>>());

		// Assert
		var ok = result.AssertOk();
		Assert.Same(v.Parts, ok);
	}

	[Fact]
	public void Adds_Taxonomies_To_Where()
	{
		// Arrange
		var (builder, v) = Setup();
		var t0 = new Taxonomy(Rnd.Str);
		var t1 = new Taxonomy(Rnd.Str);
		var taxonomies = ListF.Create(t0, t1);

		// Act
		var result = builder.AddWhereTaxonomies(v.Parts, taxonomies);

		// Assert
		var ok = result.AssertOk();
		Assert.NotSame(v.Parts, ok);
		var (column, compare, value) = Assert.Single(ok.Where);
		Assert.Equal(builder.TTest.TermTaxonomies.GetName(), column.TblName);
		Assert.Equal(builder.TTest.TermTaxonomies.Taxonomy, column.ColName);
		Assert.Equal(Compare.In, compare);
		Assert.Equal(taxonomies, value);
	}
}
