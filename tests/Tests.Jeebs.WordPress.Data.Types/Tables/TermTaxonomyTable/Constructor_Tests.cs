// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Data.Tables;
using Xunit;

namespace Jeebs.WordPress.Entities.Tables.TermTaxonomyTable_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Adds_Prefix_To_Table_Name()
	{
		// Arrange
		var prefix = F.Rnd.Str;
		var expected = $"{prefix}term_taxonomy";

		// Act
		var result = new TermTaxonomyTable(prefix).ToString();

		// Assert
		Assert.Equal(expected, result);
	}
}
