// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using static Jeebs.WordPress.Functions.QueryPostsF.M;

namespace Jeebs.WordPress.Functions.QueryPostsF_Tests;

public class GetMetaDictionary_Tests
{
	[Fact]
	public void No_MetaDictionary_Properties_Returns_None_With_MetaDictionaryPropertyNotFoundMsg()
	{
		// Arrange

		// Act
		var result = QueryPostsF.GetMetaDictionary<NoMetaDictionaryProperties>();

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<MetaDictionaryPropertyNotFoundMsg<NoMetaDictionaryProperties>>(none);
	}

	[Fact]
	public void More_Than_One_MetaDictionary_Properties_Returns_None_With_MoreThanOneMetaDictionaryMsg()
	{
		// Arrange

		// Act
		var result = QueryPostsF.GetMetaDictionary<MoreThanOneMetaDictionaryProperty>();

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<MoreThanOneMetaDictionaryMsg<MoreThanOneMetaDictionaryProperty>>(none);
	}

	[Fact]
	public void With_Single_MetaDictionary_Property_Returns_PropertyInfo()
	{
		// Arrange

		// Act
		var result = QueryPostsF.GetMetaDictionary<SingleMetaDictionaryProperty>();

		// Assert
		var some = result.AssertSome();
		Assert.Equal(nameof(SingleMetaDictionaryProperty.Meta), some.Name);
	}

	public sealed record class NoMetaDictionaryProperties;

	public sealed record class MoreThanOneMetaDictionaryProperty(MetaDictionary Meta0, MetaDictionary Meta1);

	public sealed record class SingleMetaDictionaryProperty(MetaDictionary Meta);
}
