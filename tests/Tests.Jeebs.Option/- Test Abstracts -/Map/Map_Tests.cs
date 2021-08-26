// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

namespace Jeebs_Tests
{
	public abstract class Map_Tests
	{
		public abstract void Test00_If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg();

		protected static void Test00(Func<Option<int>, Func<int, string>, Handler, Option<string>> act)
		{
			// Arrange
			var option = new FakeOption();
			var map = Substitute.For<Func<int, string>>();

			// Act
			var result = act(option, map, DefaultHandler);

			// Assert
			var none = result.AssertNone();
			var msg = Assert.IsType<UnhandledExceptionMsg>(none);
			Assert.IsType<UnknownOptionException>(msg.Exception);
		}

		public abstract void Test01_Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionMsg();

		protected static void Test01(Func<Option<string>, Func<string, int>, Handler, Option<int>> act)
		{
			// Arrange
			var option = Return(F.Rnd.Str);
			var exception = new Exception();
			int throwFunc(string _) => throw exception;

			// Act
			var result = act(option, throwFunc, DefaultHandler);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<UnhandledExceptionMsg>(none);
		}

		public abstract void Test02_Exception_Thrown_With_Handler_Calls_Handler_Returns_None();

		protected static void Test02(Func<Option<string>, Func<string, int>, Handler, Option<int>> act)
		{
			// Arrange
			var option = Return(F.Rnd.Str);
			var handler = Substitute.For<Handler>();
			var exception = new Exception();
			int throwFunc(string _) => throw exception;

			// Act
			var result = act(option, throwFunc, handler);

			// Assert
			result.AssertNone();
			handler.Received().Invoke(exception);
		}

		public abstract void Test03_If_None_Returns_None();

		protected static void Test03(Func<Option<int>, Func<int, string>, Handler, Option<string>> act)
		{
			// Arrange
			var option = Create.None<int>();
			var map = Substitute.For<Func<int, string>>();

			// Act
			var result = act(option, map, DefaultHandler);

			// Assert
			result.AssertNone();
		}

		public abstract void Test04_If_None_With_Reason_Returns_None_With_Same_Reason();

		protected static void Test04(Func<Option<int>, Func<int, string>, Handler, Option<string>> act)
		{
			// Arrange
			var msg = new TestMsg();
			var option = None<int>(msg);
			var map = Substitute.For<Func<int, string>>();

			// Act
			var result = act(option, map, DefaultHandler);

			// Assert
			var none = result.AssertNone();
			Assert.Same(msg, none);
		}

		public abstract void Test05_If_Some_Runs_Map_Function();

		protected static void Test05(Func<Option<int>, Func<int, string>, Handler, Option<string>> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);
			var map = Substitute.For<Func<int, string>>();

			// Act
			act(option, map, DefaultHandler);

			// Assert
			map.Received().Invoke(value);
		}

		public record class FakeOption : Option<int> { }

		public record class TestMsg : IMsg { }
	}
}