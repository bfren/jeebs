// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs;
using Jeebs.Exceptions;
using Jeebs.Internals;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

namespace Jeebs_Tests
{
	public abstract class SwitchIf_Tests
	{
		public abstract void Test00_Unknown_Option_Throws_UnknownOptionException();

		protected static void Test00(Func<Option<int>, Func<int, bool>, Option<int>> act)
		{
			// Arrange
			var option = new FakeOption();
			var check = Substitute.For<Func<int, bool>>();

			// Act
			void action() => act(option, check);

			// Assert
			Assert.Throws<UnknownOptionException>(action);
		}

		public abstract void Test01_None_Returns_Original_None();

		protected static void Test01(Func<Option<int>, Func<int, bool>, Option<int>> act)
		{
			// Arrange
			var option = Create.None<int>();
			var check = Substitute.For<Func<int, bool>>();

			// Act
			var result = act(option, check);

			// Assert
			result.AssertNone();
			Assert.Same(option, result);
		}

		public abstract void Test02_Check_Func_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg();

		protected static void Test02(Func<Option<int>, Func<int, bool>, Option<int>> act)
		{
			// Arrange
			var option = Some(F.Rnd.Int);
			static bool check(int _) => throw new Exception();

			// Act
			var result = act(option, check);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<SwitchIfFuncExceptionMsg>(none);
		}

		public abstract void Test03_Check_Returns_True_And_IfTrue_Is_Null_Returns_Original_Option();

		protected static void Test03(Func<Option<int>, Func<int, bool>, Option<int>> act)
		{
			// Arrange
			var option = Some(F.Rnd.Int);
			var check = Substitute.For<Func<int, bool>>();
			check.Invoke(Arg.Any<int>()).Returns(true);

			// Act
			var result = act(option, check);

			// Assert
			Assert.Same(option, result);
		}

		public abstract void Test04_Check_Returns_False_And_IfFalse_Is_Null_Returns_Original_Option();

		protected static void Test04(Func<Option<int>, Func<int, bool>, Option<int>> act)
		{
			// Arrange
			var option = Some(F.Rnd.Int);
			var check = Substitute.For<Func<int, bool>>();
			check.Invoke(Arg.Any<int>()).Returns(false);

			// Act
			var result = act(option, check);

			// Assert
			Assert.Same(option, result);
		}

		public abstract void Test05_Check_Returns_True_And_IfTrue_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg();

		protected static void Test05(Func<Option<int>, Func<int, bool>, Func<int, None<int>>, Option<int>> act)
		{
			// Arrange
			var option = Some(F.Rnd.Int);
			var check = Substitute.For<Func<int, bool>>();
			check.Invoke(Arg.Any<int>()).Returns(true);
			static None<int> ifTrue(int _) => throw new Exception();

			// Act
			var result = act(option, check, ifTrue);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<SwitchIfFuncExceptionMsg>(none);
		}

		public abstract void Test06_Check_Returns_False_And_IfFalse_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg();

		protected static void Test06(Func<Option<int>, Func<int, bool>, Func<int, None<int>>, Option<int>> act)
		{
			// Arrange
			var option = Some(F.Rnd.Int);
			var check = Substitute.For<Func<int, bool>>();
			check.Invoke(Arg.Any<int>()).Returns(false);
			static None<int> ifFalse(int _) => throw new Exception();

			// Act
			var result = act(option, check, ifFalse);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<SwitchIfFuncExceptionMsg>(none);
		}

		public abstract void Test07_Check_Returns_True_Runs_IfTrue_Returns_Value();

		protected static void Test07(Func<Option<int>, Func<int, bool>, Func<int, Option<int>>, Option<int>> act)
		{
			// Arrange
			var v0 = F.Rnd.Int;
			var v1 = F.Rnd.Int;
			var option = Some(v0);
			var check = Substitute.For<Func<int, bool>>();
			check.Invoke(v0).Returns(true);
			var ifTrue = Substitute.For<Func<int, Option<int>>>();
			ifTrue.Invoke(v0).Returns(Some(v0 + v1));

			// Act
			var result = act(option, check, ifTrue);

			// Assert
			ifTrue.Received().Invoke(v0);
			var some = result.AssertSome();
			Assert.Equal(v0 + v1, some);
		}

		public abstract void Test08_Check_Returns_False_Runs_IfFalse_Returns_Value();

		protected static void Test08(Func<Option<int>, Func<int, bool>, Func<int, None<int>>, Option<int>> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Some(value);
			var check = Substitute.For<Func<int, bool>>();
			check.Invoke(value).Returns(false);
			var ifFalse = Substitute.For<Func<int, None<int>>>();
			ifFalse.Invoke(value).Returns(None<int, TestMsg>());

			// Act
			var result = act(option, check, ifFalse);

			// Assert
			ifFalse.Received().Invoke(value);
			var none = result.AssertNone();
			Assert.IsType<TestMsg>(none);
		}

		public record class FakeOption : Option<int> { }

		public sealed record class TestMsg : IMsg { }
	}
}
