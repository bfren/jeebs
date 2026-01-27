// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities.Ids;
using Jeebs.WordPress.Tables;
using static Jeebs.WordPress.Query.PostsMetaPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.PostsMetaPartsBuilder_Tests;

public class Table_Tests : QueryPartsBuilder_Tests<PostsMetaPartsBuilder, WpPostMetaId>
{
	protected override PostsMetaPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public void Returns_PostMetaTable()
	{
		// Arrange
		var (builder, _) = Setup();

		// Act
		var result = builder.Table;

		// Assert
		Assert.IsType<PostsMetaTable>(result);
	}
}
