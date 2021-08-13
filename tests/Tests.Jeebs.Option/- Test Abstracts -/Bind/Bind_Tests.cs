// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

namespace Jeebs_Tests
{
	public abstract class Bind_Tests
	{
		public abstract void Test00_If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg();

		protected static void Test00(Func<Option<int>, Func<int, Option<string>>, Option<string>> act)
		{
			// Arrange
			var option = new FakeOption();
			var bind = Substitute.For<Func<int, Option<string>>>();

			// Act
			var result = act(option, bind);

			// Assert
			var none = result.AssertNone();
			var msg = Assert.IsType<UnhandledExceptionMsg>(none);
			Assert.IsType<UnknownOptionException>(msg.Exception);
		}

		public abstract void Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionMsg();

		protected static void Test01(Func<Option<int>, Func<int, Option<string>>, Option<string>> act)
		{
			// Arrange
			var option = Return(F.Rnd.Int);
			var exception = new Exception();
			Option<string> throwFunc() => throw exception;

			// Act
			var result = act(option, _ => throwFunc());

			// Assert
			var none = result.AssertNone();
			Assert.IsType<UnhandledExceptionMsg>(none);
		}

		public abstract void Test02_If_None_Gets_None();

		protected static void Test02(Func<Option<int>, Func<int, Option<string>>, Option<string>> act)
		{
			// Arrange
			var option = Create.None<int>();
			var bind = Substitute.For<Func<int, Option<string>>>();

			// Act
			var result = act(option, bind);

			// Assert
			result.AssertNone();
		}

		public abstract void Test03_If_None_With_Reason_Gets_None_With_Same_Reason();

		protected static void Test03(Func<Option<int>, Func<int, Option<string>>, Option<string>> act)
		{
			// Arrange
			var msg = new TestMsg();
			var option = None<int>(msg);
			var bind = Substitute.For<Func<int, Option<string>>>();

			// Act
			var result = act(option, bind);

			// Assert
			var none = result.AssertNone();
			Assert.Same(msg, none);
		}

		public abstract void Test04_If_Some_Runs_Bind_Function();

		protected static void Test04(Func<Option<int>, Func<int, Option<string>>, Option<string>> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);
			var bind = Substitute.For<Func<int, Option<string>>>();

			// Act
			act(option, bind);

			// Assert
			bind.Received().Invoke(value);
		}

		public record class FakeOption : Option<int> { }

		public record class TestMsg : IMsg { }
	}
}
