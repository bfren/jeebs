// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;
using Jeebs.Data.QueryBuilder.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Entities.Ids;
using Jeebs.WordPress.Tables;
using static Jeebs.WordPress.Query.PostsTaxonomyPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.PostsTaxonomyPartsBuilder_Tests;

public class GetColumns_Tests : GetColumns_Tests<PostsTaxonomyPartsBuilder, WpTermId>
{
	protected override PostsTaxonomyPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public void Calls_Extract_From_With_Tables()
	{
		// Arrange
		var (builder, v) = Setup();

		// Act
		builder.GetColumns<WpPostEntity>();

		// Assert
		v.Extract.Received().From<WpPostEntity>(
			Arg.Any<TermsTable>(),
			Arg.Any<TermRelationshipsTable>(),
			Arg.Any<TermTaxonomiesTable>()
		);
	}

	[Fact]
	public override void Test00_Calls_Extract_From() =>
		Test00<WpPostEntity>();
}
