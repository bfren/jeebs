// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.Functions;
using Jeebs.WordPress.Entities.Ids;
using static Jeebs.WordPress.Query.PostsTaxonomyPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.PostsTaxonomyPartsBuilder_Tests;

public class AddWherePostIds_Tests : QueryPartsBuilder_Tests<PostsTaxonomyPartsBuilder, WpTermId>
{
	protected override PostsTaxonomyPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public void Empty_PostIds_Does_Nothing()
	{
		// Arrange
		var (builder, v) = Setup();

		// Act
		var result = builder.AddWherePostIds(v.Parts, Substitute.For<IImmutableList<WpPostId>>());

		// Assert
		var ok = result.AssertOk();
		Assert.Same(v.Parts, ok);
	}

	[Fact]
	public void Adds_Taxonomies_To_Where()
	{
		// Arrange
		var (builder, v) = Setup();
		var id0 = IdGen.ULongId<WpPostId>();
		var id1 = IdGen.ULongId<WpPostId>();
		var postIds = ListF.Create(id0, id1);
		var postIdValues = postIds.Select(p => p.Value);

		// Act
		var result = builder.AddWherePostIds(v.Parts, postIds);

		// Assert
		var ok = result.AssertOk();
		Assert.NotSame(v.Parts, ok);
		var (column, compare, value) = Assert.Single(ok.Where);
		Assert.Equal(builder.TTest.TermRelationships.GetName(), column.TblName);
		Assert.Equal(builder.TTest.TermRelationships.PostId, column.ColName);
		Assert.Equal(Compare.In, compare);
		Assert.Equal(postIdValues, value);
	}
}
