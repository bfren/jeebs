﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Data.Tables;
using Xunit;

namespace Jeebs.WordPress.Entities.Tables.CommentTable_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Adds_Prefix_To_Table_Name()
	{
		// Arrange
		var prefix = F.Rnd.Str;
		var expected = $"{prefix}comments";

		// Act
		var result = new CommentTable(prefix).ToString();

		// Assert
		Assert.Equal(expected, result);
	}
}