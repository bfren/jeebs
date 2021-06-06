// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using Xunit;

namespace Jeebs.ListExtensions_Tests
{
	public class GetSlice_Tests
	{
		[Fact]
		public void Returns_Correct_Slice()
		{
			// Arrange
			var v0 = F.Rnd.Str;
			var v1 = F.Rnd.Str;
			var v2 = F.Rnd.Str;
			var v3 = F.Rnd.Str;
			var v4 = F.Rnd.Str;
			var v5 = F.Rnd.Str;
			var v6 = F.Rnd.Str;
			var v7 = F.Rnd.Str;
			var v8 = F.Rnd.Str;
			var v9 = F.Rnd.Str;
			var list = new List<string> { v0, v1, v2, v3, v4, v5, v6, v7, v8, v9 };
			var expected = new List<string> { v3, v4 };

			// Act
			var result = list.GetSlice(3, 5);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
