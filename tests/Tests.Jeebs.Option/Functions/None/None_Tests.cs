// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests
{
	public class None_Tests
	{
		[Fact]
		public void Returns_None_Without_Reason()
		{
			// Arrange

			// Act
			var result = Create.None<int>();

			// Assert
			result.AssertNone();
		}

		[Fact]
		public void Returns_None_With_Reason_Object()
		{
			// Arrange
			var reason = Substitute.For<IMsg>();

			// Act
			var result = None<int>(reason);

			// Assert
			var none = result.AssertNone();
			Assert.Same(reason, none);
		}

		[Fact]
		public void Returns_None_With_Reason_Type()
		{
			// Arrange

			// Act
			var result = None<int, TestMsg>();

			// Assert
			var none = result.AssertNone();
			Assert.IsType<TestMsg>(none);
		}

		[Fact]
		public void Returns_None_With_Reason_Exception_Type()
		{
			// Arrange
			var exception = new Exception();

			// Act
			var result = None<int, TestExceptionMsg>(exception);

			// Assert
			var none = result.AssertNone();
			var msg = Assert.IsType<TestExceptionMsg>(none);
			Assert.Same(exception, msg.Exception);
		}

		public record class TestMsg : IMsg { }

		public record class TestExceptionMsg() : IExceptionMsg
		{
			public Exception Exception { get; init; } = new();
		}
	}
}
