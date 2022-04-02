// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities.StrongIds;
using static Jeebs.WordPress.Query.PostsTaxonomyPartsBuilder_Tests.Setup;
using static StrongId.Testing.Generator;

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
		var some = result.AssertSome();
		Assert.Same(v.Parts, some);
	}

	[Fact]
	public void Adds_Taxonomies_To_Where()
	{
		// Arrange
		var (builder, v) = Setup();
		var id0 = LongId<WpPostId>();
		var id1 = LongId<WpPostId>();
		var postIds = ImmutableList.Create(id0, id1);
		var postIdValues = postIds.Select(p => p.Value);

		// Act
		var result = builder.AddWherePostIds(v.Parts, postIds);

		// Assert
		var some = result.AssertSome();
		Assert.NotSame(v.Parts, some);
		Assert.Collection(some.Where,
			x =>
			{
				Assert.Equal(builder.TTest.TermRelationships.GetName(), x.column.TblName);
				Assert.Equal(builder.TTest.TermRelationships.PostId, x.column.ColName);
				Assert.Equal(Compare.In, x.cmp);
				Assert.Equal(postIdValues, x.value);
			}
		);
	}
}
