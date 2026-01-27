// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Tables.PostsMetaTable_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Adds_Prefix_To_Table_Name()
	{
		// Arrange
		var prefix = Rnd.Str;
		var expected = $"{prefix}postmeta";

		// Act
		var result = new PostsMetaTable(prefix).ToString();

		// Assert
		Assert.Equal(expected, result);
	}
}
