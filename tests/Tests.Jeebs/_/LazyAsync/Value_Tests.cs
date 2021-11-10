// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Xunit;

namespace Jeebs.LazyAsync_Tests;

public class Value_Tests
{
	[Fact]
	public void Returns_Task()
	{
		// Arrange
		var task = new Task<int>(() => F.Rnd.Int);
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
		var counter = 0;
		var t = () => Task.FromResult(++counter);
		var l = new LazyAsync<int>(t);

		// Act
		for (int i = 0; i < 100; i++)
		{
			_ = await l.Value;
		}

		// Assert
		Assert.Equal(1, counter);
	}
}
