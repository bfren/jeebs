// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs;
using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests
{
	public class True_Tests
	{
		[Fact]
		public void Returns_Some_With_Value_True()
		{
			// Arrange

			// Act
			var result = True;

			// Assert
			result.AssertTrue();
		}
	}
}
