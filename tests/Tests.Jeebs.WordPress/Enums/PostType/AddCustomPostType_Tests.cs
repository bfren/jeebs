﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Enums.PostType_Tests;

public class AddCustomPostType_Tests
{
	[Fact]
	public void Adds_Custom_PostType_To_HashSet()
	{
		// Arrange
		var name = Rnd.Str;
		var type = new PostType(name);

		// Act
		var result = PostType.AddCustomPostType(type);

		// Assert
		Assert.True(result);
		Assert.Contains(PostType.AllTest(),
			x => x.Equals(type)
		);
	}

	[Fact]
	public void Does_Not_Add_Custom_PostType_Twice()
	{
		// Arrange
		var name = Rnd.Str;
		var type = new PostType(name);
		PostType.AddCustomPostType(type);

		// Act
		var result = PostType.AddCustomPostType(type);

		// Assert
		Assert.False(result);
		Assert.Contains(PostType.AllTest(),
			x => x.Equals(type)
		);
	}
}
