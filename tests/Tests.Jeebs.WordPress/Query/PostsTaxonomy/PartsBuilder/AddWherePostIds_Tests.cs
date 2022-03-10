// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using NSubstitute;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.PostsTaxonomyPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.PostsTaxonomyPartsBuilder_Tests;

public class AddWherePostIds_Tests : QueryPartsBuilder_Tests<Query.PostsTaxonomyPartsBuilder, WpTermId>
{
	protected override Query.PostsTaxonomyPartsBuilder GetConfiguredBuilder(IExtract extract) =>
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
		var id0 = new WpPostId(F.Rnd.Lng);
		var id1 = new WpPostId(F.Rnd.Lng);
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
				Assert.Equal(builder.TTest.TermRelationship.GetName(), x.column.TblName);
				Assert.Equal(builder.TTest.TermRelationship.PostId, x.column.ColName);
				Assert.Equal(Compare.In, x.cmp);
				Assert.Equal(postIdValues, x.value);
			}
		);
	}
}
