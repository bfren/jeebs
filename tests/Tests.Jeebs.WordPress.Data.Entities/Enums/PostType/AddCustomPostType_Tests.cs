// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.WordPress.Data.Enums.PostType_Tests
{
	public class AddCustomPostType_Tests
	{
		[Fact]
		public void Adds_Custom_PostType_To_HashSet()
		{
			// Arrange
			var name = F.Rnd.Str;
			var type = new PostType(name);

			// Act
			PostType.AddCustomPostType(type);

			// Assert
			Assert.Contains(PostType.AllTest(),
				x => x.Equals(type)
			);
		}
	}
}
