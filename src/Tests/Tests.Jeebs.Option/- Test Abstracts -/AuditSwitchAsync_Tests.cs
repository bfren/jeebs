// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs_Tests
{
	public abstract class AuditSwitchAsync_Tests
	{
		public abstract Task Test00_Null_Args_Returns_Original_Option();

		protected static async Task Test00(Func<Option<int>, Task<Option<int>>> act)
		{
			// Arrange
			var option = new FakeOption();

			// Act
			var result = await act(option);

			// Assert
			Assert.Same(option, result);
		}

		public abstract Task Test01_If_Unknown_Option_Throws_UnknownOptionException();

		protected static async Task Test01(Func<Option<int>, Task<Option<int>>> act)
		{
			// Arrange
			var option = new FakeOption();

			// Act
			Task action() => act(option);

			// Assert
			await Assert.ThrowsAsync<UnknownOptionException>(action);
		}

		public abstract Task Test02_Some_Runs_Some_Action_And_Returns_Original_Option();

		protected static async Task Test02(Func<Option<int>, Action<int>, Task<Option<int>>> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);
			var some = Substitute.For<Action<int>>();

			// Act
			var result = await act(option, some);

			// Assert
			some.Received().Invoke(value);
			Assert.Same(option, result);
		}

		public abstract Task Test03_Some_Runs_Some_Func_And_Returns_Original_Option();

		protected static async Task Test03(Func<Option<int>, Func<int, Task>, Task<Option<int>>> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);
			var some = Substitute.For<Func<int, Task>>();

			// Act
			var result = await act(option, some);

			// Assert
			await some.Received().Invoke(value);
			Assert.Same(option, result);
		}

		public abstract Task Test04_None_Runs_None_Action_And_Returns_Original_Option();

		protected static async Task Test04(Func<Option<int>, Action<IMsg>, Task<Option<int>>> act)
		{
			// Arrange
			var msg = new TestMsg();
			var option = None<int>(msg);
			var none = Substitute.For<Action<IMsg>>();

			// Act
			var result = await act(option, none);

			// Assert
			none.Received().Invoke(msg);
			Assert.Same(option, result);
		}

		public abstract Task Test05_None_Runs_None_Func_And_Returns_Original_Option();

		protected static async Task Test05(Func<Option<int>, Func<IMsg, Task>, Task<Option<int>>> act)
		{
			// Arrange
			var msg = new TestMsg();
			var option = None<int>(msg);
			var none = Substitute.For<Func<IMsg, Task>>();

			// Act
			var result = await act(option, none);

			// Assert
			await none.Received().Invoke(msg);
			Assert.Same(option, result);
		}

		public abstract Task Test06_Some_Runs_Some_Action_Catches_Exception_And_Returns_Original_Option();

		protected static async Task Test06(Func<Option<int>, Action<int>, Task<Option<int>>> act)
		{
			// Arrange
			var option = Return(F.Rnd.Int);
			var exception = new Exception();
			void throwException(int _) => throw exception;

			// Act
			var result = await act(option, throwException);

			// Assert
			Assert.Same(option, result);
		}

		public abstract Task Test07_Some_Runs_Some_Func_Catches_Exception_And_Returns_Original_Option();

		protected static async Task Test07(Func<Option<int>, Func<int, Task>, Task<Option<int>>> act)
		{
			// Arrange
			var option = Return(F.Rnd.Int);
			var exception = new Exception();
			Task throwException(int _) => throw exception;

			// Act
			var result = await act(option, throwException);

			// Assert
			Assert.Same(option, result);
		}

		public abstract Task Test08_None_Runs_None_Action_Catches_Exception_And_Returns_Original_Option();

		protected static async Task Test08(Func<Option<int>, Action<IMsg>, Task<Option<int>>> act)
		{
			// Arrange
			var option = None<int>(true);
			var exception = new Exception();
			void throwException(IMsg _) => throw exception;

			// Act
			var result = await act(option, throwException);

			// Assert
			Assert.Same(option, result);
		}

		public abstract Task Test09_None_Runs_None_Func_Catches_Exception_And_Returns_Original_Option();

		protected static async Task Test09(Func<Option<int>, Func<IMsg, Task>, Task<Option<int>>> act)
		{
			// Arrange
			var option = None<int>(true);
			var exception = new Exception();
			Task throwException(IMsg _) => throw exception;

			// Act
			var result = await act(option, throwException);

			// Assert
			Assert.Same(option, result);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}
