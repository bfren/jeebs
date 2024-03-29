﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Enums.Taxonomy_Tests;

public class Parse_Tests
{
	public static TheoryData<string, Taxonomy> Returns_Correct_Taxonomy_Data =>
		new()
		{
			{ string.Empty, Taxonomy.Blank },
			{ "category", Taxonomy.PostCategory },
			{ "link_category", Taxonomy.LinkCategory },
			{ "nav_menu", Taxonomy.NavMenu },
			{ "post_tag", Taxonomy.PostTag }
		};

	[Theory]
	[MemberData(nameof(Returns_Correct_Taxonomy_Data))]
	public void Returns_Correct_Taxonomy(string name, Taxonomy type)
	{
		// Arrange

		// Act
		var result = Taxonomy.Parse(name);

		// Assert
		Assert.Same(type, result);
	}

	[Fact]
	public void Unknown_Returns_Blank()
	{
		// Arrange
		var name = Rnd.Str;

		// Act
		var result = Taxonomy.Parse(name);

		// Assert
		Assert.Same(Taxonomy.Blank, result);
	}

	[Fact]
	public void Returns_Custom_Taxonomy()
	{
		// Arrange
		var name = Rnd.Str;
		var type = new Taxonomy(name);
		Taxonomy.AddCustomTaxonomy(type);

		// Act
		var result = Taxonomy.Parse(name);

		// Assert
		Assert.Same(type, result);
	}
}
