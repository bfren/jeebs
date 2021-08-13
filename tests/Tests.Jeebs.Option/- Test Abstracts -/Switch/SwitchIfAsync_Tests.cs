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
		public abstract Task Test00_Unknown_Option_Throws_UnknownOptionException();

		protected static async Task Test00(Func<Task<Option<int>>, Func<int, bool>, Task<Option<int>>> act)
		{
			// Arrange
			var option = new FakeOption();
			var check = Substitute.For<Func<int, bool>>();

			// Act
			Task action() => act(option.AsTask, check);

			// Assert
			await Assert.ThrowsAsync<UnknownOptionException>(action);
		}

		public abstract Task Test01_None_Returns_Original_None();

		protected static async Task Test01(Func<Task<Option<int>>, Func<int, bool>, Task<Option<int>>> act)
		{
			// Arrange
			var option = Create.None<int>();
			var check = Substitute.For<Func<int, bool>>();

			// Act
			var result = await act(option.AsTask, check);

			// Assert
			result.AssertNone();
			Assert.Same(option, result);
		}

		public abstract Task Test02_Check_Func_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg();

		protected static async Task Test02(Func<Task<Option<int>>, Func<int, bool>, Task<Option<int>>> act)
		{
			// Arrange
			var option = Return(F.Rnd.Int);
			static bool check(int _) => throw new Exception();

			// Act
			var result = await act(option.AsTask, check);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<SwitchIfFuncExceptionMsg>(none);
		}

		public abstract Task Test03_Check_Returns_True_And_IfTrue_Is_Null_Returns_Original_Option();

		protected static async Task Test03(Func<Task<Option<int>>, Func<int, bool>, Task<Option<int>>> act)
		{
			// Arrange
			var option = Return(F.Rnd.Int);
			var check = Substitute.For<Func<int, bool>>();
			check.Invoke(Arg.Any<int>()).Returns(true);

			// Act
			var result = await act(option.AsTask, check);

			// Assert
			Assert.Same(option, result);
		}

		public abstract Task Test04_Check_Returns_False_And_IfFalse_Is_Null_Returns_Original_Option();

		protected static async Task Test04(Func<Task<Option<int>>, Func<int, bool>, Task<Option<int>>> act)
		{
			// Arrange
			var option = Return(F.Rnd.Int);
			var check = Substitute.For<Func<int, bool>>();
			check.Invoke(Arg.Any<int>()).Returns(false);

			// Act
			var result = await act(option.AsTask, check);

			// Assert
			Assert.Same(option, result);
		}

		public abstract Task Test05_Check_Returns_True_And_IfTrue_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg();

		protected static async Task Test05(Func<Task<Option<int>>, Func<int, bool>, Func<int, None<int>>, Task<Option<int>>> act)
		{
			// Arrange
			var option = Return(F.Rnd.Int);
			var check = Substitute.For<Func<int, bool>>();
			check.Invoke(Arg.Any<int>()).Returns(true);
			static None<int> ifTrue(int _) => throw new Exception();

			// Act
			var result = await act(option.AsTask, check, ifTrue);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<SwitchIfFuncExceptionMsg>(none);
		}

		public abstract Task Test06_Check_Returns_False_And_IfFalse_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg();

		protected static async Task Test06(Func<Task<Option<int>>, Func<int, bool>, Func<int, None<int>>, Task<Option<int>>> act)
		{
			// Arrange
			var option = Return(F.Rnd.Int);
			var check = Substitute.For<Func<int, bool>>();
			check.Invoke(Arg.Any<int>()).Returns(false);
			static None<int> ifFalse(int _) => throw new Exception();

			// Act
			var result = await act(option.AsTask, check, ifFalse);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<SwitchIfFuncExceptionMsg>(none);
		}

		public abstract Task Test07_Check_Returns_True_Runs_IfTrue_Returns_Value();

		protected static async Task Test07(Func<Task<Option<int>>, Func<int, bool>, Func<int, Option<int>>, Task<Option<int>>> act)
		{
			// Arrange
			var v0 = F.Rnd.Int;
			var v1 = F.Rnd.Int;
			var option = Return(v0);
			var check = Substitute.For<Func<int, bool>>();
			check.Invoke(v0).Returns(true);
			var ifTrue = Substitute.For<Func<int, Option<int>>>();
			ifTrue.Invoke(v0).Returns(Return(v0 + v1));

			// Act
			var result = await act(option.AsTask, check, ifTrue);

			// Assert
			ifTrue.Received().Invoke(v0);
			var some = result.AssertSome();
			Assert.Equal(v0 + v1, some);
		}

		public abstract Task Test08_Check_Returns_False_Runs_IfFalse_Returns_Value();

		protected static async Task Test08(Func<Task<Option<int>>, Func<int, bool>, Func<int, None<int>>, Task<Option<int>>> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);
			var check = Substitute.For<Func<int, bool>>();
			check.Invoke(value).Returns(false);
			var ifFalse = Substitute.For<Func<int, None<int>>>();
			ifFalse.Invoke(value).Returns(None<int, TestMsg>());

			// Act
			var result = await act(option.AsTask, check, ifFalse);

			// Assert
			ifFalse.Received().Invoke(value);
			var none = result.AssertNone();
			Assert.IsType<TestMsg>(none);
		}

		public record class FakeOption : Option<int> { }

		public sealed record class TestMsg : IMsg { }
	}
}
