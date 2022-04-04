// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities.StrongIds;
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
		var some = result.AssertSome();
		Assert.Same(v.Parts, some);
	}

	[Fact]
	public void Adds_Taxonomies_To_Where()
	{
		// Arrange
		var (builder, v) = Setup();
		var t0 = new Taxonomy(Rnd.Str);
		var t1 = new Taxonomy(Rnd.Str);
		var taxonomies = ImmutableList.Create(t0, t1);

		// Act
		var result = builder.AddWhereTaxonomies(v.Parts, taxonomies);

		// Assert
		var some = result.AssertSome();
		Assert.NotSame(v.Parts, some);
		Assert.Collection(some.Where,
			x =>
			{
				Assert.Equal(builder.TTest.TermTaxonomies.GetName(), x.column.TblName);
				Assert.Equal(builder.TTest.TermTaxonomies.Taxonomy, x.column.ColName);
				Assert.Equal(Compare.In, x.compare);
				Assert.Equal(taxonomies, x.value);
			}
		);
	}
}
