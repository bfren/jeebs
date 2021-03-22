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
	public class SwitchIfAsync_Tests
	{
		[Fact]
		public async Task If_Unknown_Option_Throws_UnknownOptionException()
		{
			// Arrange
			var option = new FakeOption().AsTask;
			var check = Substitute.For<Func<int, bool>>();
			var ifFalse = Substitute.For<Func<int, None<int>>>();

			// Act
			Task action() => SwitchIfAsync(option, check, ifFalse);

			// Assert
			await Assert.ThrowsAsync<UnknownOptionException>(action);
		}

		[Fact]
		public async Task If_None_Returns_Original_None()
		{
			// Arrange
			var option = Create.EmptyNone<int>();
			var check = Substitute.For<Func<int, bool>>();
			var ifFalse = Substitute.For<Func<int, None<int>>>();

			// Act
			var result = await SwitchIfAsync(option.AsTask, check, ifFalse);

			// Assert
			result.AssertNone();
			Assert.Same(option, result);
		}

		[Fact]
		public async Task Check_Func_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg()
		{
			// Arrange
			var option = Return(Rnd.Int).AsTask;
			static bool check(int _) => throw new Exception();
			var ifFalse = Substitute.For<Func<int, None<int>>>();

			// Act
			var result = await SwitchIfAsync(option, check, ifFalse);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<SwitchIfFuncExceptionMsg>(none);
		}

		[Fact]
		public async Task IfFalse_Func_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg()
		{
			// Arrange
			var option = Return(Rnd.Int).AsTask;
			var check = Substitute.For<Func<int, bool>>();
			static None<int> ifFalse(int _) => throw new Exception();

			// Act
			var result = await SwitchIfAsync(option, check, ifFalse);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<SwitchIfFuncExceptionMsg>(none);
		}

		[Fact]
		public async Task Runs_Check_Returns_True_Returns_Original_Some()
		{
			// Arrange
			var value = Rnd.Int;
			var option = Return(value);
			var check = Substitute.For<Func<int, bool>>();
			check.Invoke(Arg.Any<int>()).Returns(true);
			var ifFalse = Substitute.For<Func<int, None<int>>>();

			// Act
			var result = await SwitchIfAsync(option.AsTask, check, ifFalse);

			// Assert
			result.AssertSome();
			Assert.Same(option, result);
			check.Received().Invoke(value);
			ifFalse.DidNotReceiveWithAnyArgs().Invoke(Arg.Any<int>());
		}

		[Fact]
		public async Task Runs_Check_Returns_False_Runs_IfFalse_Returns_None()
		{
			// Arrange
			var value = Rnd.Int;
			var option = Return(value).AsTask;
			var check = Substitute.For<Func<int, bool>>();
			check.Invoke(Arg.Any<int>()).Returns(false);
			var ifFalse = Substitute.For<Func<int, None<int>>>();
			ifFalse.Invoke(Arg.Any<int>()).Returns(None<int, TestMsg>());

			// Act
			var result = await SwitchIfAsync(option, check, ifFalse);

			// Assert
			check.Received().Invoke(value);
			ifFalse.Received().Invoke(value);
			var none = result.AssertNone();
			Assert.IsType<TestMsg>(none);
		}

		public class FakeOption : Option<int> { }

		public sealed record TestMsg : IMsg { }
	}
}
