// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using NSubstitute;
using Xunit;

namespace Jeebs.OptionStatic_Tests
{
	public class True_Tests
	{
		[Fact]
		public void Returns_Some_With_Value_True()
		{
			// Arrange

			// Act
			var result = Option.True;

			// Assert
			var some = Assert.IsType<Some<bool>>(result);
			Assert.True(some.Value);
		}
	}
}
