// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using Xunit;

namespace Jeebs.ImmutableList_Tests
{
	public class Count_Tests
	{
		[Fact]
		public void Returns_Number_Of_Items()
		{
			// Arrange
			var count = F.Rnd.NumberF.GetInt32(10, 20);
			var items = new List<int>();
			for (int i = 0; i < count; i++)
			{
				items.Add(F.Rnd.Int);
			}
			var list = ImmutableList.Create(items);

			// Act
			var result = list.Count;

			// Assert
			Assert.Equal(count, result);
		}
	}
}
