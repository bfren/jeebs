// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Tables;
using NSubstitute;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.PostsTaxonomyPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.PostsTaxonomyPartsBuilder_Tests
{
	public class GetColumns_Tests : GetColumns_Tests<Query.PostsTaxonomyPartsBuilder, WpTermId>
	{
		protected override Query.PostsTaxonomyPartsBuilder GetConfiguredBuilder(IExtract extract) =>
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
				Arg.Any<TermTable>(),
				Arg.Any<TermRelationshipTable>(),
				Arg.Any<TermTaxonomyTable>()
			);
		}

		[Fact]
		public override void Test00_Calls_Extract_From() =>
			Test00<WpPostEntity>();
	}
}
