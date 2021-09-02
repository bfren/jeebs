// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.WordPress.Data.Enums.Taxonomy_Tests;

public class AddCustomTaxonomy_Tests
{
	[Fact]
	public void Adds_Custom_Taxonomy_To_HashSet()
	{
		// Arrange
		var name = F.Rnd.Str;
		var type = new Taxonomy(name);

		// Act
		var result = Taxonomy.AddCustomTaxonomy(type);

		// Assert
		Assert.True(result);
		Assert.Contains(Taxonomy.AllTest(),
			x => x.Equals(type)
		);
	}

	[Fact]
	public void Does_Not_Add_Custom_Taxonomy_Twice()
	{
		// Arrange
		var name = F.Rnd.Str;
		var type = new Taxonomy(name);
		Taxonomy.AddCustomTaxonomy(type);

		// Act
		var result = Taxonomy.AddCustomTaxonomy(type);

		// Assert
		Assert.False(result);
		Assert.Contains(Taxonomy.AllTest(),
			x => x.Equals(type)
		);
	}
}
