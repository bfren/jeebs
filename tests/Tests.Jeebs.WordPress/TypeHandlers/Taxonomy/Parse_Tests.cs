// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Enums;
using Base = Jeebs.WordPress.Enums.Taxonomy_Tests.Parse_Tests;

namespace Jeebs.WordPress.TypeHandlers.TaxonomyTypeHandler_Tests;

public class Parse_Tests
{
	[Theory]
	[MemberData(nameof(Base.Returns_Correct_Taxonomy_Data), MemberType = typeof(Base))]
	public void Valid_Value_Returns_TaxonomyType(string input, Taxonomy expected)
	{
		// Arrange
		var handler = new TaxonomyTypeHandler();

		// Act
		var result = handler.Parse(input);

		// Assert
		Assert.Same(expected, result);
	}

	[Fact]
	public void Null_Value_Returns_Blank_Taxonomy()
	{
		// Arrange
		var handler = new TaxonomyTypeHandler();

		// Act
		var result = handler.Parse(null!);

		// Assert
		Assert.Same(Taxonomy.Blank, result);
	}

	[Fact]
	public void Invalid_Value_Returns_Blank_Taxonomy()
	{
		// Arrange
		var value = Rnd.Str;
		var handler = new TaxonomyTypeHandler();

		// Act
		var result = handler.Parse(value);

		// Assert
		Assert.Same(Taxonomy.Blank, result);
	}
}
