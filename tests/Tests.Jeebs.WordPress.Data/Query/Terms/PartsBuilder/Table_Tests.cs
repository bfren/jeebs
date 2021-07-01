// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Tables;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.TermsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.TermsPartsBuilder_Tests
{
	public class Table_Tests : QueryPartsBuilder_Tests<Query.TermsPartsBuilder, WpTermId>
	{
		protected override Query.TermsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
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
}
