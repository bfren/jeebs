// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Xunit;
using static F.OptionF;

namespace Jeebs.None_Tests
{
	public class AddReason_Tests
	{
		[Fact]
		public void As_Type()
		{
			// Arrange
			var none = None<int>(true);

			// Act
			var result = none.AddReason<TestMsg>();

			// Assert
			Assert.IsType<TestMsg>(result.Reason);
		}

		[Fact]
		public void For_Exception()
		{
			// Arrange
			var none = None<int>(true);
			var exception = new Exception();

			// Act
			var result = none.AddReason<TestExceptionMsg>(exception);

			// Assert
			Assert.IsType<TestExceptionMsg>(result.Reason);
		}

		public record TestMsg : IMsg { }
		public record TestExceptionMsg : Jeebs.ExceptionMsg { }
	}
}
