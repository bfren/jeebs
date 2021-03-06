using System;
using Xunit;

namespace Jeebs.Option_Tests
{
	public class AddReason_Tests
	{
		[Fact]
		public void As_Object()
		{
			// Arrange
			var none = Option.None<int>();

			// Act
			var result = none.AddReason(new TestMsg());

			// Assert
			Assert.True(result.Reason is TestMsg);
		}

		[Fact]
		public void As_Type()
		{
			// Arrange
			var none = Option.None<int>();

			// Act
			var result = none.AddReason<TestMsg>();

			// Assert
			Assert.True(result.Reason is TestMsg);
		}

		[Fact]
		public void For_Exception()
		{
			// Arrange
			var none = Option.None<int>();
			var exception = new Exception();

			// Act
			var result = none.AddReason<TestExceptionMsg>(exception);

			// Assert
			Assert.True(result.Reason is TestExceptionMsg);
		}

		public class TestMsg : IMsg { }
		public class TestExceptionMsg : Jm.ExceptionMsg { }
	}
}
