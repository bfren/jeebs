// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.Tables;
using static Jeebs.WordPress.Query.PostsMetaPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.PostsMetaPartsBuilder_Tests;

public class IdColumn_Tests : QueryPartsBuilder_Tests<PostsMetaPartsBuilder, WpPostMetaId>
{
	protected override PostsMetaPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public void Returns_Id_Column()
	{
		// Arrange
		var (builder, _) = Setup();

		// Act
		var result = builder.IdColumn;

		// Assert
		Assert.Equal(builder.TTest.PostsMeta.GetName(), result.TblName);
		Assert.Equal(builder.TTest.PostsMeta.Id, result.ColName);
		Assert.Equal(nameof(PostsMetaTable.Id), result.ColAlias);
	}
}
