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
	public class Return_Tests
	{
		[Fact]
		public void Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			int throwFunc() => throw new Exception();
			int? throwFuncNullable() => throw new Exception();

			// Act
			var r0 = Return(throwFunc, DefaultHandler);
			var r1 = Return(throwFuncNullable, DefaultHandler);
			var r2 = Return(throwFuncNullable, true, DefaultHandler);

			// Assert
			var n0 = r0.AssertNone();
			Assert.IsType<UnhandledExceptionMsg>(n0);
			var n1 = r1.AssertNone();
			Assert.IsType<UnhandledExceptionMsg>(n1);
			var n2 = r2.AssertNone();
			Assert.IsType<UnhandledExceptionMsg>(n2);
		}

		[Fact]
		public void Exception_Thrown_With_Handler_Returns_None_Calls_Handler()
		{
			// Arrange
			var handler = Substitute.For<Handler>();
			var exception = new Exception();
			int throwFunc() => throw exception;
			int? throwFuncNullable() => throw exception;

			// Act
			var r0 = Return(throwFunc, handler);
			var r1 = Return(throwFuncNullable, handler);
			var r2 = Return(throwFuncNullable, false, handler);

			// Assert
			r0.AssertNone();
			r1.AssertNone();
			r2.AssertNone();
			handler.Received(3).Invoke(exception);
		}

		[Fact]
		public void Null_Input_Returns_None()
		{
			// Arrange
			int? value = null;

			// Act
			var r0 = Return(value);
			var r1 = Return(() => value, DefaultHandler);
			var r2 = Return(value, false);
			var r3 = Return(() => value, false, DefaultHandler);

			// Assert
			var n0 = r0.AssertNone();
			Assert.IsType<NullValueMsg>(n0);
			var n1 = r1.AssertNone();
			Assert.IsType<NullValueMsg>(n1);
			var n2 = r2.AssertNone();
			Assert.IsType<AllowNullWasFalseMsg>(n2);
			var n3 = r3.AssertNone();
			Assert.IsType<AllowNullWasFalseMsg>(n2);
		}

		[Fact]
		public void Null_Input_Returns_Some_If_AllowNull_Is_True()
		{
			// Arrange
			int? value = null;

			// Act
			var r0 = Return(value, true);
			var r1 = Return(() => value, true, DefaultHandler);

			// Assert
			var s0 = r0.AssertSome();
			Assert.Null(s0);
			var s1 = r1.AssertSome();
			Assert.Null(s1);
		}

		[Theory]
		[InlineData(18)]
		[InlineData("foo")]
		public void Value_Input_Returns_Some<T>(T input)
		{
			// Arrange

			// Act
			var r0 = Return(input);
			var r1 = Return(() => input, DefaultHandler);
			var r2 = Return(() => input, false, DefaultHandler);

			// Assert
			var s0 = r0.AssertSome();
			Assert.Equal(input, s0);
			var s1 = r1.AssertSome();
			Assert.Equal(input, s1);
			var s2 = r2.AssertSome();
			Assert.Equal(input, s2);
		}

		[Theory]
		[InlineData(18)]
		[InlineData("foo")]
		public void Value_Input_Returns_Some_Nullable<T>(T? input)
		{
			// Arrange

			// Act
			var r0 = Return(input, true);
			var r1 = Return(() => input, true, DefaultHandler);

			// Assert
			var s0 = r0.AssertSome();
			Assert.Equal(input, s0);
			var s1 = r1.AssertSome();
			Assert.Equal(input, s1);
		}

		[Fact]
		public void Runs_Function_Returns_Some()
		{
			// Arrange
			var value = Rnd.Int;
			var get = Substitute.For<Func<string>>();
			string getNotNull() => get();
			string? getNullable() => get();

			// Act
			Return(getNotNull, DefaultHandler);
			Return(getNullable, false, DefaultHandler);

			// Assert
			get.Received(2).Invoke();
		}
	}
}
