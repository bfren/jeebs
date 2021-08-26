// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
			Assert.Equal(0U, id.Value);
		}

		public readonly record struct TestId(ulong Value) : IStrongId;
	}
}
