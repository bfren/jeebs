// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;
using Jeebs.Data.QueryBuilder.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities.Ids;
using Jeebs.WordPress.Tables;
using static Jeebs.WordPress.Query.TermsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.TermsPartsBuilder_Tests;

public class IdColumn_Tests : QueryPartsBuilder_Tests<TermsPartsBuilder, WpTermId>
{
	protected override TermsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public void Returns_Id_Column()
	{
		// Arrange
		var (builder, _) = Setup();

		// Act
		var result = builder.IdColumn;

		// Assert
		Assert.Equal(builder.TTest.Terms.GetName(), result.TblName);
		Assert.Equal(builder.TTest.Terms.Id, result.ColName);
		Assert.Equal(nameof(TermsTable.Id), result.ColAlias);
	}
}
