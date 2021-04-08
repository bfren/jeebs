// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.StrongId_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_Default()
		{
			// Arrange

			// Act
			var id = new TestId();

			// Assert
			Assert.Equal(0L, id.Value);
		}

		public record TestId() : StrongId(0);
	}
}
