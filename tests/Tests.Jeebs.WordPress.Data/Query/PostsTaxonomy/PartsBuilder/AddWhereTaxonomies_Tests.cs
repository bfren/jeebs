// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;
using NSubstitute;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.PostsTaxonomyPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.PostsTaxonomyPartsBuilder_Tests;

public class AddWhereTaxonomies_Tests : QueryPartsBuilder_Tests<Query.PostsTaxonomyPartsBuilder, WpTermId>
{
	protected override Query.PostsTaxonomyPartsBuilder GetConfiguredBuilder(IExtract extract) =>
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
		var t0 = new Taxonomy(F.Rnd.Str);
		var t1 = new Taxonomy(F.Rnd.Str);
		var taxonomies = ImmutableList.Create(t0, t1);

		// Act
		var result = builder.AddWhereTaxonomies(v.Parts, taxonomies);

		// Assert
		var some = result.AssertSome();
		Assert.NotSame(v.Parts, some);
		Assert.Collection(some.Where,
			x =>
			{
				Assert.Equal(builder.TTest.TermTaxonomy.GetName(), x.column.Table);
				Assert.Equal(builder.TTest.TermTaxonomy.Taxonomy, x.column.Name);
				Assert.Equal(Compare.In, x.cmp);
				Assert.Equal(taxonomies, x.value);
			}
		);
	}
}
