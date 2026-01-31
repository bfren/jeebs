// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities.Ids;
using Jeebs.WordPress.Tables;
using static Jeebs.WordPress.Query.PostsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.PostsPartsBuilder_Tests;

public class IdColumn_Tests : QueryPartsBuilder_Tests<PostsPartsBuilder, WpPostId>
{
	protected override PostsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public void Returns_Id_Column()
	{
		// Arrange
		var (builder, _) = Setup();

		// Act
		var result = builder.IdColumn;

		// Assert
		Assert.Equal(builder.TTest.Posts.GetName(), result.TblName);
		Assert.Equal(builder.TTest.Posts.Id, result.ColName);
		Assert.Equal(nameof(PostsTable.Id), result.ColAlias);
	}
}
