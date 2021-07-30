﻿// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.WordPress.Data.Enums.Taxonomy_Tests
{
	public class IsRegistered_Tests
	{
		[Fact]
		public void Returns_True_If_Added()
		{
			// Arrange
			var name = F.Rnd.Str;
			var type = new Taxonomy(name);
			Taxonomy.AddCustomTaxonomy(type);

			// Act
			var result = Taxonomy.IsRegistered(type);

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void Returns_False_If_Not_Added()
		{
			// Arrange
			var name = F.Rnd.Str;
			var type = new Taxonomy(name);

			// Act
			var result = Taxonomy.IsRegistered(type);

			// Assert
			Assert.False(result);
		}
	}
}
