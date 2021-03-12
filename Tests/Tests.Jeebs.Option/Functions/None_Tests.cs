// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs;
using NSubstitute;
using Xunit;
using static JeebsF.OptionF;

namespace JeebsF.OptionF_Tests
{
	public class None_Tests
	{
		[Fact]
		public void Returns_None()
		{
			// Arrange

			// Act
			var result = None<int>(true);

			// Assert
			Assert.IsType<None<int>>(result);
		}

		[Fact]
		public void Returns_None_With_Reason()
		{
			// Arrange
			var reason = Substitute.For<IMsg>();

			// Act
			var result = None<int>(reason);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.Same(reason, none.Reason);
		}
	}
}
