// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Xunit;
using static F.MaybeF;

namespace Jeebs.MaybeExtensions_Tests;

public class GetAsyncEnumerator_Tests
{
	[Fact]
	public async Task When_Some_Returns_Value()
	{
		// Arrange
		var value = F.Rnd.Int;
		var maybe = Some(value).AsTask;

		// Act
		var result = 0;
		await foreach (var item in maybe)
		{
			result = item;
		}

		// Assert
		Assert.Equal(value, result);
	}

	[Fact]
	public async Task When_None_Does_Nothing()
	{
		// Arrange
		var value = F.Rnd.Int;
		var maybe = Create.None<int>().AsTask;

		// Act
		var result = value;
		await foreach (var item in maybe)
		{
			result = 0;
		}

		// Assert
		Assert.Equal(value, result);
	}
}
