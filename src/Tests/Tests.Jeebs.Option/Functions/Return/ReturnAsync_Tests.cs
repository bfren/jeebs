// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

namespace F.OptionF_Tests
{
	public class ReturnAsync_Tests
	{
		[Fact]
		public async Task Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			Task<int> throwFunc() => throw new Exception();
			Task<int?> throwFuncNullable() => throw new Exception();

			// Act
			var r0 = await ReturnAsync(throwFunc, DefaultHandler);
			var r1 = await ReturnAsync(throwFuncNullable, DefaultHandler);
			var r2 = await ReturnAsync(throwFuncNullable, true, DefaultHandler);

			// Assert
			var n0 = r0.AssertNone();
			Assert.IsType<UnhandledExceptionMsg>(n0);
			var n1 = r1.AssertNone();
			Assert.IsType<UnhandledExceptionMsg>(n1);
			var n2 = r2.AssertNone();
			Assert.IsType<UnhandledExceptionMsg>(n2);
		}

		[Fact]
		public async Task Exception_Thrown_With_Handler_Returns_None_Calls_Handler()
		{
			// Arrange
			var handler = Substitute.For<Handler>();
			var exception = new Exception();
			Task<int> throwFunc() => throw exception;
			Task<int?> throwFuncNullable() => throw exception;

			// Act
			var r0 = await ReturnAsync(throwFunc, handler);
			var r1 = await ReturnAsync(throwFuncNullable, handler);
			var r2 = await ReturnAsync(throwFuncNullable, false, handler);

			// Assert
			r0.AssertNone();
			r1.AssertNone();
			r2.AssertNone();
			handler.Received(3).Invoke(exception);
		}

		[Fact]
		public async Task Null_Input_Returns_None()
		{
			// Arrange
			Task<int?> value = Task.FromResult<int?>(null);

			// Act
			var r0 = await ReturnAsync(value);
			var r1 = await ReturnAsync(() => value, DefaultHandler);
			var r2 = await ReturnAsync(value, false);
			var r3 = await ReturnAsync(() => value, false, DefaultHandler);

			// Assert
			var n0 = r0.AssertNone();
			Assert.IsType<NullValueMsg>(n0);
			var n1 = r1.AssertNone();
			Assert.IsType<NullValueMsg>(n1);
			var n2 = r2.AssertNone();
			Assert.IsType<AllowNullWasFalseMsg>(n2);
			var n3 = r3.AssertNone();
			Assert.IsType<AllowNullWasFalseMsg>(n3);
		}

		[Fact]
		public async Task Null_Input_Returns_Some_If_AllowNull_Is_True()
		{
			// Arrange
			Task<int?> value = Task.FromResult<int?>(null);

			// Act
			var r0 = await ReturnAsync(value, true);
			var r1 = await ReturnAsync(() => value, true, DefaultHandler);

			// Assert
			var s0 = r0.AssertSome();
			Assert.Null(s0);
			var s1 = r1.AssertSome();
			Assert.Null(s1);
		}

		[Theory]
		[InlineData(18)]
		[InlineData("foo")]
		public async Task Value_Input_Returns_Some<T>(T input)
		{
			// Arrange
			var value = Task.FromResult<T?>(input);

			// Act
			var r0 = await ReturnAsync(value);
			var r1 = await ReturnAsync(() => value, DefaultHandler);
			var r2 = await ReturnAsync(() => value, false, DefaultHandler);

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
		public async Task Value_Input_Returns_Some_Nullable<T>(T? input)
		{
			// Arrange
			var value = Task.FromResult(input);

			// Act
			var r0 = await ReturnAsync(value, true);
			var r1 = await ReturnAsync(() => value, true, DefaultHandler);

			// Assert
			var s0 = r0.AssertSome();
			Assert.Equal(input, s0);
			var s1 = r1.AssertSome();
			Assert.Equal(input, s1);
		}

		[Fact]
		public async Task Runs_Function_Returns_Some()
		{
			// Arrange
			var value = Rnd.Int;
			var get = Substitute.For<Func<Task<string>>>();
			Task<string> getNotNull() => get();
			async Task<string?> getNullable() => await get();

			// Act
			await ReturnAsync(getNotNull, DefaultHandler);
			await ReturnAsync(getNullable, false, DefaultHandler);

			// Assert
			await get.Received(2).Invoke();
		}
	}
}
