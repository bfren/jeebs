﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Tables;
using NSubstitute;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.TermsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.TermsPartsBuilder_Tests;

public class GetColumns_Tests : GetColumns_Tests<Query.TermsPartsBuilder, WpTermId>
{
	protected override Query.TermsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
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
			Arg.Any<TermTaxonomyTable>()
		);
	}

	[Fact]
	public override void Test00_Calls_Extract_From() =>
		Test00<WpPostEntity>();
}
