// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Linq;
using Xunit;
using static F.Rnd.BooleanF;

namespace F.BooleanF_Tests
{
	public class FlipCoin_Tests
	{
		[Fact]
		public void Returns_True_Or_False()
		{
			// Arrange
			const int iterations = 100;
			var results = new List<bool>();

			// Act
			for (int i = 0; i < iterations; i++)
			{
				results.Add(FlipCoin());
			}

			// Assert
			Assert.Equal(2, results.Distinct().Count());
		}
	}
}
