// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Data.Tables;
using Xunit;

namespace Jeebs.WordPress.Entities.Tables.TermTable_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Adds_Prefix_To_Table_Name()
	{
		// Arrange
		var prefix = F.Rnd.Str;
		var expected = $"{prefix}terms";

		// Act
		var result = new TermTable(prefix).GetName();

		// Assert
		Assert.Equal(expected, result);
	}
}
