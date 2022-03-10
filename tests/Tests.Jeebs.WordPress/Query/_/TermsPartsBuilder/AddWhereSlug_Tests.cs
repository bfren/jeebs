// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities.StrongIds;
using static Jeebs.WordPress.Query.TermsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.TermsPartsBuilder_Tests;

public class AddWhereSlug_Tests : QueryPartsBuilder_Tests<TermsPartsBuilder, WpTermId>
{
	protected override TermsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public void Null_Slug_Does_Nothing()
	{
		// Arrange
		var (builder, v) = Setup();

		// Act
		var result = builder.AddWhereSlug(v.Parts, null);

		// Assert
		var some = result.AssertSome();
		Assert.Same(v.Parts, some);
	}

	[Fact]
	public void Adds_Slug_To_Where()
	{
		// Arrange
		var (builder, v) = Setup();
		var slug = Rnd.Str;

		// Act
		var result = builder.AddWhereSlug(v.Parts, slug);

		// Assert
		var some = result.AssertSome();
		Assert.NotSame(v.Parts, some);
		Assert.Collection(some.Where,
			x =>
			{
				Assert.Equal(builder.TTest.Terms.GetName(), x.column.TblName);
				Assert.Equal(builder.TTest.Terms.Slug, x.column.ColName);
				Assert.Equal(Compare.Equal, x.cmp);
				Assert.Equal(slug, x.value);
			}
		);
	}
}
