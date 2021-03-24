// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

namespace Jeebs_Tests
{
	public abstract class SwitchIf_Tests
	{
		public abstract void Test00_If_Unknown_Option_Throws_UnknownOptionException();

		protected static void Test00(Func<Option<int>, Func<int, bool>, Func<int, None<int>>, Option<int>> act)
		{
			// Arrange
			var option = new FakeOption();
			var check = Substitute.For<Func<int, bool>>();
			var ifFalse = Substitute.For<Func<int, None<int>>>();

			// Act
			void action() => act(option, check, ifFalse);

			// Assert
			Assert.Throws<UnknownOptionException>(action);
		}

		public abstract void Test01_Check_Func_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg();

		protected static void Test01(Func<Option<int>, Func<int, bool>, Func<int, None<int>>, Option<int>> act)
		{
			// Arrange
			var option = Return(F.Rnd.Int);
			static bool check(int _) => throw new Exception();
			var ifFalse = Substitute.For<Func<int, None<int>>>();

			// Act
			var result = act(option, check, ifFalse);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<SwitchIfFuncExceptionMsg>(none);
		}

		public abstract void Test02_IfFalse_Func_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg();

		protected static void Test02(Func<Option<int>, Func<int, bool>, Func<int, None<int>>, Option<int>> act)
		{
			// Arrange
			var option = Return(F.Rnd.Int);
			var check = Substitute.For<Func<int, bool>>();
			static None<int> ifFalse(int _) => throw new Exception();

			// Act
			var result = act(option, check, ifFalse);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<SwitchIfFuncExceptionMsg>(none);
		}

		public abstract void Test03_If_None_Returns_Original_None();

		protected static void Test03(Func<Option<int>, Func<int, bool>, Func<int, None<int>>, Option<int>> act)
		{
			// Arrange
			var option = Create.EmptyNone<int>();
			var check = Substitute.For<Func<int, bool>>();
			var ifFalse = Substitute.For<Func<int, None<int>>>();

			// Act
			var result = act(option, check, ifFalse);

			// Assert
			result.AssertNone();
			Assert.Same(option, result);
		}

		public abstract void Test04_If_Some_And_Check_Is_False_Runs_IfFalse_Returns_None();

		protected static void Test04(Func<Option<int>, Func<int, bool>, Func<int, None<int>>, Option<int>> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);
			var check = Substitute.For<Func<int, bool>>();
			check.Invoke(Arg.Any<int>()).Returns(false);
			var ifFalse = Substitute.For<Func<int, None<int>>>();
			ifFalse.Invoke(Arg.Any<int>()).Returns(None<int, TestMsg>());

			// Act
			var result = act(option, check, ifFalse);

			// Assert
			check.Received().Invoke(value);
			ifFalse.Received().Invoke(value);
			var none = result.AssertNone();
			Assert.IsType<TestMsg>(none);
		}

		public abstract void Test05_If_Some_And_Check_Is_True_Returns_Original_Some();

		protected static void Test05(Func<Option<int>, Func<int, bool>, Func<int, None<int>>, Option<int>> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);
			var check = Substitute.For<Func<int, bool>>();
			check.Invoke(Arg.Any<int>()).Returns(true);
			var ifFalse = Substitute.For<Func<int, None<int>>>();

			// Act
			var result = act(option, check, ifFalse);

			// Assert
			result.AssertSome();
			Assert.Same(option, result);
			check.Received().Invoke(value);
			ifFalse.DidNotReceiveWithAnyArgs().Invoke(Arg.Any<int>());
		}

		public class FakeOption : Option<int> { }

		public sealed record TestMsg : IMsg { }
	}
}
