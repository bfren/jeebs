// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Id.StrongId_Tests;

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

	public readonly record struct TestId(long Value) : IStrongId;
}
