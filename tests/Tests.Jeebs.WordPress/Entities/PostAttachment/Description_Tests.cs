// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Entities.PostAttachment_Tests;

public class Description_Tests
{
	[Fact]
	public void Sets_Excerpt_Property()
	{
		// Arrange
		var value = Rnd.Str;

		// Act
		var result = new Test { Description = value };

		// Assert
		Assert.Equal(value, result.Excerpt);
	}

	[Fact]
	public void Gets_Excerpt_Property()
	{
		// Arrange
		var value = Rnd.Str;

		// Act
		var result = new Test { Excerpt = value };

		// Assert
		Assert.Equal(value, result.Description);
	}

	public sealed record class Test : PostAttachment;
}
