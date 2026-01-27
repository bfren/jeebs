// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

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
		_ = result.AssertFailure("MetaDictionary property not found for model '{Type}'.",
			nameof(NoMetaDictionaryProperties)
		);
	}

	[Fact]
	public void More_Than_One_MetaDictionary_Properties_Returns_None_With_MoreThanOneMetaDictionaryMsg()
	{
		// Arrange

		// Act
		var result = QueryPostsF.GetMetaDictionary<MoreThanOneMetaDictionaryProperty>();

		// Assert
		_ = result.AssertFailure("Multiple MetaDictionary properties found for model '{Type}'.",
			nameof(MoreThanOneMetaDictionaryProperty)
		);
	}

	[Fact]
	public void With_Single_MetaDictionary_Property_Returns_PropertyInfo()
	{
		// Arrange

		// Act
		var result = QueryPostsF.GetMetaDictionary<SingleMetaDictionaryProperty>();

		// Assert
		var ok = result.AssertOk();
		Assert.Equal(nameof(SingleMetaDictionaryProperty.Meta), ok.Name);
	}

	public sealed record class NoMetaDictionaryProperties;

	public sealed record class MoreThanOneMetaDictionaryProperty(MetaDictionary Meta0, MetaDictionary Meta1);

	public sealed record class SingleMetaDictionaryProperty(MetaDictionary Meta);
}
