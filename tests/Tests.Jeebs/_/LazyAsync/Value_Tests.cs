// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.LazyAsync_Tests;

public class Value_Tests
{
	[Fact]
	public void Returns_Task()
	{
		// Arrange
		var task = new Task<int>(() => Rnd.Int);
		var l0 = new LazyAsync<int>(task);
		var l1 = new LazyAsync<int>(() => task);

		// Act
		var r0 = l0.Value;
		var r1 = l1.Value;

		// Assert
		Assert.Same(task, r0);
		Assert.Same(task, r1);
	}

	[Fact]
	public async Task Executes_Once()
	{
		// Arrange
		var lazyCounter = 0;
		var taskCounter = 0;
		var times = 10;
		var l = () => Task.FromResult(++lazyCounter);
		var t = () => Task.FromResult(++taskCounter);
		var a = new LazyAsync<int>(l);

		// Act
		for (int i = 0; i < times; i++)
		{
			await a.Value; // should run task once
		}
		for (int i = 0; i < times; i++)
		{
			await t(); // should run task each time
		}

		// Assert
		Assert.Equal(1, lazyCounter);
		Assert.Equal(times, taskCounter);
	}
}
