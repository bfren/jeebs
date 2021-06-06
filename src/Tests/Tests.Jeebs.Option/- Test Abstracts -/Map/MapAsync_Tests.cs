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
	public abstract class MapAsync_Tests
	{
		public abstract Task Test00_If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg();

		protected static async Task Test00(Func<Option<int>, Func<int, Task<string>>, Handler, Task<Option<string>>> act)
		{
			// Arrange
			var option = new FakeOption();
			var map = Substitute.For<Func<int, Task<string>>>();

			// Act
			var result = await act(option, map, DefaultHandler);

			// Assert
			var none = result.AssertNone();
			var msg = Assert.IsType<UnhandledExceptionMsg>(none);
			Assert.IsType<UnknownOptionException>(msg.Exception);
		}

		public abstract Task Test01_Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionMsg();

		protected static async Task Test01(Func<Option<string>, Func<string, Task<int>>, Handler, Task<Option<int>>> act)
		{
			// Arrange
			var option = Return(F.Rnd.Str);
			var exception = new Exception();
			Task<int> throwFunc(string _) => throw exception;

			// Act
			var result = await act(option, throwFunc, DefaultHandler);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<UnhandledExceptionMsg>(none);
		}

		public abstract Task Test02_Exception_Thrown_With_Handler_Calls_Handler_Returns_None();

		protected static async Task Test02(Func<Option<string>, Func<string, Task<int>>, Handler, Task<Option<int>>> act)
		{
			// Arrange
			var option = Return(F.Rnd.Str);
			var handler = Substitute.For<Handler>();
			var exception = new Exception();
			Task<int> throwFunc(string _) => throw exception;

			// Act
			var result = await act(option, throwFunc, handler);

			// Assert
			result.AssertNone();
			handler.Received().Invoke(exception);
		}

		public abstract Task Test03_If_None_Returns_None();

		protected static async Task Test03(Func<Option<int>, Func<int, Task<string>>, Handler, Task<Option<string>>> act)
		{
			// Arrange
			var option = Create.EmptyNone<int>();
			var map = Substitute.For<Func<int, Task<string>>>();

			// Act
			var result = await act(option, map, DefaultHandler);

			// Assert
			result.AssertNone();
		}

		public abstract Task Test04_If_None_With_Reason_Returns_None_With_Same_Reason();

		protected static async Task Test04(Func<Option<int>, Func<int, Task<string>>, Handler, Task<Option<string>>> act)
		{
			// Arrange
			var msg = new TestMsg();
			var option = None<int>(msg);
			var map = Substitute.For<Func<int, Task<string>>>();

			// Act
			var result = await act(option, map, DefaultHandler);

			// Assert
			var none = result.AssertNone();
			Assert.Same(msg, none);
		}

		public abstract Task Test05_If_Some_Runs_Map_Function();

		protected static async Task Test05(Func<Option<int>, Func<int, Task<string>>, Handler, Task<Option<string>>> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);
			var map = Substitute.For<Func<int, Task<string>>>();

			// Act
			await act(option, map, DefaultHandler);

			// Assert
			await map.Received().Invoke(value);
		}

		public record FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}