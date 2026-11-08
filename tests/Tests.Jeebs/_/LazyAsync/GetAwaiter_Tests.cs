// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.LazyAsync_Tests;

public class GetAwaiter_Tests
{
	[Fact]
	public async Task Await_Directly__Returns_Value__Runs_Once()
	{
		// Arrange
		var counter = 0;
		var lazy = new LazyAsync<int>(() => Task.FromResult(++counter));

		// Act
		var r0 = await lazy;
		var r1 = await lazy;

		// Assert
		Assert.Equal(1, r0);
		Assert.Equal(1, r1);
		Assert.Equal(1, counter);
	}
}
