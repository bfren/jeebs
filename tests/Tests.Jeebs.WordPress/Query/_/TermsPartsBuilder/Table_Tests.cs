// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.Tables;
using static Jeebs.WordPress.Query.TermsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.TermsPartsBuilder_Tests;

public class Table_Tests : QueryPartsBuilder_Tests<TermsPartsBuilder, WpTermId>
{
	protected override TermsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public void Returns_PostMetaTable()
	{
		// Arrange
		var (builder, _) = Setup();

		// Act
		var result = builder.Table;

		// Assert
		_ = Assert.IsType<TermsTable>(result);
	}
}
