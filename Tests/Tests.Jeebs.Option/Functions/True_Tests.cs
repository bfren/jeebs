// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs;
using Xunit;
using static JeebsF.OptionF;

namespace JeebsF.OptionF_Tests
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
			var some = Assert.IsType<Some<bool>>(result);
			Assert.True(some.Value);
		}
	}
}
