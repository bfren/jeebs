// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

namespace F.OptionF_Tests
{
	public class ReturnIf_Tests
	{
		[Fact]
		public void Exception_Thrown_By_Predicate_Without_Handler_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = Return(Rnd.Str);
			static bool throwFunc() => throw new Exception();

			// Act
			var result = ReturnIf(throwFunc, Rnd.Int);

			// Assert
			var msg = result.AssertNone();
			Assert.IsType<UnhandledExceptionMsg>(msg);
		}

		[Fact]
		public void Exception_Thrown_By_Predicate_With_Handler_Returns_None_Calls_Handler()
		{
			// Arrange
			var option = Return(Rnd.Str);
			var handler = Substitute.For<Handler>();
			var exception = new Exception();
			bool throwFunc() => throw exception;

			// Act
			var result = ReturnIf(throwFunc, () => Rnd.Int, handler);

			// Assert
			result.AssertNone();
			handler.Received().Invoke(exception);
		}

		[Fact]
		public void Exception_Thrown_By_ValueFunc_With_Handler_Returns_None_Calls_Handler()
		{
			// Arrange
			var option = Return(Rnd.Str);
			var handler = Substitute.For<Handler>();
			var exception = new Exception();
			int throwFunc() => throw exception;

			// Act
			var result = ReturnIf(() => true, throwFunc, handler);

			// Assert
			result.AssertNone();
			handler.Received().Invoke(exception);
		}

		[Fact]
		public void True_Returns_Some()
		{
			// Arrange
			var value = Rnd.Int;

			// Act
			var r0 = ReturnIf(() => true, value);
			var r1 = ReturnIf(() => true, () => value, DefaultHandler);

			// Assert
			var s0 = r0.AssertSome();
			Assert.Equal(value, s0);
			var s1 = r1.AssertSome();
			Assert.Equal(value, s1);
		}

		[Fact]
		public void False_Returns_None_With_PredicateWasFalseMsg()
		{
			// Arrange
			var value = Rnd.Int;

			// Act
			var r0 = ReturnIf(() => false, value);
			var r1 = ReturnIf(() => false, () => value, DefaultHandler);

			// Assert
			var m0 = r0.AssertNone();
			Assert.IsType<PredicateWasFalseMsg>(m0);
			var m1 = r1.AssertNone();
			Assert.IsType<PredicateWasFalseMsg>(m1);
		}

		[Fact]
		public void False_Bypasses_Value_Func()
		{
			// Arrange
			var getValue = Substitute.For<Func<int>>();

			// Act
			var result = ReturnIf(() => false, getValue);

			// Assert
			result.AssertNone();
			getValue.DidNotReceive().Invoke();
		}
	}
}
