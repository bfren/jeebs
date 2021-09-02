// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Tables;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.PostsTaxonomyPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.PostsTaxonomyPartsBuilder_Tests;

public class Table_Tests : QueryPartsBuilder_Tests<Query.PostsTaxonomyPartsBuilder, WpTermId>
{
	protected override Query.PostsTaxonomyPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public void Returns_PostMetaTable()
	{
		// Arrange
		var (builder, _) = Setup();

		// Act
		var result = builder.Table;

		// Assert
		Assert.IsType<TermTable>(result);
	}
}
