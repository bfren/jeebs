// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Jeebs.Random.Rnd_Tests.ByteF_Tests;

public class Get_Tests
{
	[Theory]
	[InlineData(2)]
	[InlineData(4)]
	[InlineData(8)]
	[InlineData(16)]
	[InlineData(32)]
	[InlineData(64)]
	public void Returns_Byte_Array_Of_Length(int length)
	{
		// Arrange

		// Act
		var result = Rnd.ByteF.Get(length);

		// Assert
		Assert.Equal(length, result.Length);
	}

	[Fact]
	public void Returns_Different_Bytes_Each_Time()
	{
		// Arrange
		const int iterations = 10000;
		var numbers = new List<byte[]>();

		// Act
		for (int i = 0; i < iterations; i++)
		{
			numbers.Add(Rnd.ByteF.Get(4));
		}

		var unique = numbers.Distinct();

		// Assert
		Assert.Equal(unique.Count(), numbers.Count);
	}
}
