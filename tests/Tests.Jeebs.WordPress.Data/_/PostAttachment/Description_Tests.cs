// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.WordPress.Data.PostAttachment_Tests
{
	public class Description_Tests
	{
		[Fact]
		public void Sets_Excerpt_Property()
		{
			// Arrange
			var value = F.Rnd.Str;

			// Act
			var result = new Test { Description = value };

			// Assert
			Assert.Equal(value, result.Excerpt);
		}

		[Fact]
		public void Gets_Excerpt_Property()
		{
			// Arrange
			var value = F.Rnd.Str;

			// Act
			var result = new Test { Excerpt = value };

			// Assert
			Assert.Equal(value, result.Description);
		}

		public sealed record class Test : PostAttachment;
	}
}
