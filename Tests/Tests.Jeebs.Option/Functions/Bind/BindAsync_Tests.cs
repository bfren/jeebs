// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

namespace F.OptionF_Tests
{
	public class BindAsync_Tests
	{
		[Fact]
		public async Task If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = new FakeOption();
			var bind = Substitute.For<Func<int, Task<Option<string>>>>();

			// Act
			var r0 = await BindAsync(option, bind);
			var r1 = await BindAsync(option.AsTask, bind);

			// Assert
			var n0 = r0.AssertNone();
			var m0 = Assert.IsType<UnhandledExceptionMsg>(n0);
			Assert.IsType<UnknownOptionException>(m0.Exception);
			var n1 = r1.AssertNone();
			var m1 = Assert.IsType<UnhandledExceptionMsg>(n1);
			Assert.IsType<UnknownOptionException>(m1.Exception);
		}

		[Fact]
		public async Task Exception_Thrown_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = Return(Rnd.Str);
			var exception = new Exception();
			Task<Option<int>> throwFunc() => throw exception;

			// Act
			var r0 = await BindAsync(option, _ => throwFunc());
			var r1 = await BindAsync(option.AsTask, _ => throwFunc());
			var r2 = await BindAsync(throwFunc);

			// Assert
			var n0 = r0.AssertNone();
			Assert.IsType<UnhandledExceptionMsg>(n0);
			var n1 = r1.AssertNone();
			Assert.IsType<UnhandledExceptionMsg>(n1);
			var n2 = r2.AssertNone();
			Assert.IsType<UnhandledExceptionMsg>(n2);
		}

		[Fact]
		public async Task If_None_Gets_None()
		{
			// Arrange
			var option = None<int>(true);
			var bind = Substitute.For<Func<int, Task<Option<string>>>>();

			// Act
			var r0 = await BindAsync(option, bind);
			var r1 = await BindAsync(option.AsTask, bind);

			// Assert
			r0.AssertNone();
			r1.AssertNone();
		}

		[Fact]
		public async Task If_None_With_Reason_Gets_None_With_Same_Reason()
		{
			// Arrange
			var msg = new TestMsg();
			var option = None<int>(msg);
			var bind = Substitute.For<Func<int, Task<Option<string>>>>();

			// Act
			var r0 = await BindAsync(option, bind);
			var r1 = await BindAsync(option.AsTask, bind);

			// Assert
			var n0 = r0.AssertNone();
			Assert.Same(msg, n0);
			var n1 = r1.AssertNone();
			Assert.Same(msg, n1);
		}

		[Fact]
		public async Task If_Some_Runs_Bind_Function()
		{
			// Arrange
			var value = Rnd.Int;
			var option = Return(value);
			var bind = Substitute.For<Func<int, Task<Option<string>>>>();

			// Act
			await BindAsync(option, bind);
			await BindAsync(option.AsTask, bind);
			await BindAsync(() => bind(value));

			// Assert
			await bind.Received(3).Invoke(value);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}
