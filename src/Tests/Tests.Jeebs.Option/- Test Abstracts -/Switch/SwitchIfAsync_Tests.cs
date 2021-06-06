// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

namespace Jeebs_Tests
{
	public abstract class SwitchIfAsync_Tests
	{
		public abstract Task Test00_If_Unknown_Option_Throws_UnknownOptionException();

		protected static async Task Test00(Func<Task<Option<int>>, Func<int, bool>, Func<int, None<int>>, Task<Option<int>>> act)
		{
			// Arrange
			var option = new FakeOption();
			var check = Substitute.For<Func<int, bool>>();
			var ifFalse = Substitute.For<Func<int, None<int>>>();

			// Act
			Task action() => act(option.AsTask, check, ifFalse);

			// Assert
			await Assert.ThrowsAsync<UnknownOptionException>(action);
		}

		public abstract Task Test01_Check_Func_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg();

		protected static async Task Test01(Func<Task<Option<int>>, Func<int, bool>, Func<int, None<int>>, Task<Option<int>>> act)
		{
			// Arrange
			var option = Return(F.Rnd.Int);
			static bool check(int _) => throw new Exception();
			var ifFalse = Substitute.For<Func<int, None<int>>>();

			// Act
			var result = await act(option.AsTask, check, ifFalse);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<SwitchIfFuncExceptionMsg>(none);
		}

		public abstract Task Test02_IfFalse_Func_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg();

		protected static async Task Test02(Func<Task<Option<int>>, Func<int, bool>, Func<int, None<int>>, Task<Option<int>>> act)
		{
			// Arrange
			var option = Return(F.Rnd.Int);
			var check = Substitute.For<Func<int, bool>>();
			static None<int> ifFalse(int _) => throw new Exception();

			// Act
			var result = await act(option.AsTask, check, ifFalse);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<SwitchIfFuncExceptionMsg>(none);
		}

		public abstract Task Test03_If_None_Returns_Original_None();

		protected static async Task Test03(Func<Task<Option<int>>, Func<int, bool>, Func<int, None<int>>, Task<Option<int>>> act)
		{
			// Arrange
			var option = Create.EmptyNone<int>();
			var check = Substitute.For<Func<int, bool>>();
			var ifFalse = Substitute.For<Func<int, None<int>>>();

			// Act
			var result = await act(option.AsTask, check, ifFalse);

			// Assert
			result.AssertNone();
			Assert.Same(option, result);
		}

		public abstract Task Test04_If_Some_And_Check_Is_False_Runs_IfFalse_Returns_None();

		protected static async Task Test04(Func<Task<Option<int>>, Func<int, bool>, Func<int, None<int>>, Task<Option<int>>> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);
			var check = Substitute.For<Func<int, bool>>();
			check.Invoke(Arg.Any<int>()).Returns(false);
			var ifFalse = Substitute.For<Func<int, None<int>>>();
			ifFalse.Invoke(Arg.Any<int>()).Returns(None<int, TestMsg>());

			// Act
			var result = await act(option.AsTask, check, ifFalse);

			// Assert
			check.Received().Invoke(value);
			ifFalse.Received().Invoke(value);
			var none = result.AssertNone();
			Assert.IsType<TestMsg>(none);
		}

		public abstract Task Test05_If_Some_And_Check_Is_True_Returns_Original_Some();

		protected static async Task Test05(Func<Task<Option<int>>, Func<int, bool>, Func<int, None<int>>, Task<Option<int>>> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);
			var check = Substitute.For<Func<int, bool>>();
			check.Invoke(Arg.Any<int>()).Returns(true);
			var ifFalse = Substitute.For<Func<int, None<int>>>();

			// Act
			var result = await act(option.AsTask, check, ifFalse);

			// Assert
			result.AssertSome();
			Assert.Same(option, result);
			check.Received().Invoke(value);
			ifFalse.DidNotReceiveWithAnyArgs().Invoke(Arg.Any<int>());
		}

		public record FakeOption : Option<int> { }

		public sealed record TestMsg : IMsg { }
	}
}
