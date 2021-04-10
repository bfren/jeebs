// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.WordPress.Enums.Taxonomy_Tests
{
	public class AddCustomTaxonomy_Tests
	{
		[Fact]
		public void Adds_Custom_Taxonomy_To_HashSet()
		{
			// Arrange
			var name = F.Rnd.Str;
			var type = new Taxonomy(name);

			// Act
			Taxonomy.AddCustomTaxonomy(type);

			// Assert
			Assert.Contains(Taxonomy.AllTest(),
				x => x.Equals(type)
			);
		}
	}
}
