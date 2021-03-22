// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static F.Rnd.GuidF;

namespace F.GuidF_Tests
{
	public class Get_Tests
	{
		[Fact]
		public void Returns_Different_Bytes_Each_Time()
		{
			// Arrange
			const int iterations = 10000;
			var numbers = new List<Guid>();

			// Act
			for (int i = 0; i < iterations; i++)
			{
				numbers.Add(Get());
			}

			var unique = numbers.Distinct();

			// Assert
			Assert.Equal(unique.Count(), numbers.Count);
		}
	}
}
