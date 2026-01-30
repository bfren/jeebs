// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data.Map;
using Jeebs.Data.QueryBuilder.QueryPartsBuilder_Tests;
using Jeebs.Functions;
using Jeebs.WordPress.Entities.Ids;
using Jeebs.WordPress.Enums;
using static Jeebs.WordPress.Query.PostsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.PostsPartsBuilder_Tests;

public class AddWhereTaxonomies_Tests : QueryPartsBuilder_Tests<PostsPartsBuilder, WpPostId>
{
	protected override PostsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public void No_Taxonomies_Does_Nothing()
	{
		// Arrange
		var (builder, v) = Setup();

		// Act
		var result = builder.AddWhereTaxonomies(v.Parts, Substitute.For<IImmutableList<(Taxonomy, WpTermId)>>());

		// Assert
		var ok = result.AssertOk();
		Assert.Same(v.Parts, ok);
	}

	[Fact]
	public void Single_Taxonomy_Adds_Taxonomy_Clause()
	{
		// Arrange
		var (builder, v) = Setup();
		var taxonomy = Taxonomy.PostCategory;
		var id = IdGen.ULongId<WpTermId>();
		var taxonomies = ListF.Create((taxonomy, id));

		var tt = builder.TTest.TermTaxonomies.ToString();
		var tr = builder.TTest.TermRelationships.ToString();
		var p = builder.TTest.Posts.ToString();

		// Act
		var result = builder.AddWhereTaxonomies(v.Parts, taxonomies);

		// Assert
		var ok = result.AssertOk();
		var (clause, _) = Assert.Single(ok.WhereCustom);
		Assert.Equal("(" +
			$"SELECT COUNT(1) FROM `{tr}` " +
			$"INNER JOIN `{tt}` ON `{tr}`.`term_taxonomy_id` = `{tt}`.`term_taxonomy_id` " +
			$"WHERE `{tt}`.`taxonomy` = @taxonomy0 " +
			$"AND `{tr}`.`object_id` = `{p}`.`ID` " +
			$"AND `{tt}`.`term_id` IN (@taxonomy0_0)" +
			") = 1",
			clause
		);
	}

	[Fact]
	public void Single_Taxonomy_Adds_Parameters()
	{
		// Arrange
		var (builder, v) = Setup();
		var taxonomy = Taxonomy.PostCategory;
		var id = IdGen.ULongId<WpTermId>();
		var taxonomies = ListF.Create((taxonomy, id));

		// Act
		var result = builder.AddWhereTaxonomies(v.Parts, taxonomies);

		// Assert
		var ok = result.AssertOk();
		var (_, parameters) = Assert.Single(ok.WhereCustom);
		Assert.Collection(parameters,
			y =>
			{
				Assert.Equal("@taxonomy0", y.Key);
				Assert.Equal(taxonomy, y.Value);
			},
			y =>
			{
				Assert.Equal("@taxonomy0_0", y.Key);
				Assert.Equal(id.Value, y.Value);
			}
		);
	}

	[Fact]
	public void Multiple_Taxonomies_Adds_Taxonomy_Clause()
	{
		// Arrange
		var (builder, v) = Setup();
		var t0 = Taxonomy.PostCategory;
		var id0 = IdGen.ULongId<WpTermId>();
		var t1 = Taxonomy.NavMenu;
		var id1 = IdGen.ULongId<WpTermId>();
		var id2 = IdGen.ULongId<WpTermId>();
		var taxonomies = ListF.Create((t0, id0), (t1, id1), (t1, id2));

		var tt = builder.TTest.TermTaxonomies.ToString();
		var tr = builder.TTest.TermRelationships.ToString();
		var p = builder.TTest.Posts.ToString();

		// Act
		var result = builder.AddWhereTaxonomies(v.Parts, taxonomies);

		// Assert
		var ok = result.AssertOk();
		var (clause, _) = Assert.Single(ok.WhereCustom);
		Assert.Equal("(" +
			$"SELECT COUNT(1) FROM `{tr}` " +
			$"INNER JOIN `{tt}` ON `{tr}`.`term_taxonomy_id` = `{tt}`.`term_taxonomy_id` " +
			$"WHERE `{tt}`.`taxonomy` = @taxonomy0 " +
			$"AND `{tr}`.`object_id` = `{p}`.`ID` " +
			$"AND `{tt}`.`term_id` IN (@taxonomy0_0)" +
			") = 1 AND (" +
			$"SELECT COUNT(1) FROM `{tr}` " +
			$"INNER JOIN `{tt}` ON `{tr}`.`term_taxonomy_id` = `{tt}`.`term_taxonomy_id` " +
			$"WHERE `{tt}`.`taxonomy` = @taxonomy1 " +
			$"AND `{tr}`.`object_id` = `{p}`.`ID` " +
			$"AND `{tt}`.`term_id` IN (@taxonomy1_0, @taxonomy1_1)" +
			") = 2",
			clause
		);
	}

	[Fact]
	public void Multiple_Taxonomies_Adds_Parameters()
	{
		// Arrange
		var (builder, v) = Setup();
		var t0 = Taxonomy.PostCategory;
		var id0 = IdGen.ULongId<WpTermId>();
		var id1 = IdGen.ULongId<WpTermId>();
		var t1 = Taxonomy.LinkCategory;
		var id2 = IdGen.ULongId<WpTermId>();
		var taxonomies = ListF.Create((t0, id0), (t0, id1), (t1, id2));

		// Act
		var result = builder.AddWhereTaxonomies(v.Parts, taxonomies);

		// Assert
		var ok = result.AssertOk();
		var (_, parameters) = Assert.Single(ok.WhereCustom);
		Assert.Collection(parameters,
			y =>
			{
				Assert.Equal("@taxonomy0", y.Key);
				Assert.Equal(t0, y.Value);
			},
			y =>
			{
				Assert.Equal("@taxonomy0_0", y.Key);
				Assert.Equal(id0.Value, y.Value);
			},
			y =>
			{
				Assert.Equal("@taxonomy0_1", y.Key);
				Assert.Equal(id1.Value, y.Value);
			},
			y =>
			{
				Assert.Equal("@taxonomy1", y.Key);
				Assert.Equal(t1, y.Value);
			},
			y =>
			{
				Assert.Equal("@taxonomy1_0", y.Key);
				Assert.Equal(id2.Value, y.Value);
			}
		);
	}
}
