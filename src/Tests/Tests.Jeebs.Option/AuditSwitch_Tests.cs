// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs_Tests
{
	public abstract class AuditSwitch_Tests
	{
		public abstract void Test00_Null_Args_Returns_Original_Option();

		protected static void Test00(Func<Option<int>, Option<int>> act)
		{
			// Arrange
			var option = new FakeOption();

			// Act
			var result = act(option);

			// Assert
			Assert.Same(option, result);
		}

		public abstract void Test01_If_Unknown_Option_Throws_UnknownOptionException();

		protected static void Test01(Func<Option<int>, Option<int>> act)
		{
			// Arrange
			var option = new FakeOption();

			// Act
			void result() => act(option);

			// Assert
			Assert.Throws<UnknownOptionException>(result);
		}

		public abstract void Test02_Some_Runs_Some_And_Returns_Original_Option();

		protected static void Test02(Func<Option<int>, Action<int>, Option<int>> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);
			var some = Substitute.For<Action<int>>();

			// Act
			var result = act(option, some);

			// Assert
			some.Received().Invoke(value);
			Assert.Same(option, result);
		}

		public abstract void Test03_None_Runs_None_And_Returns_Original_Option();

		protected static void Test03(Func<Option<int>, Action<IMsg>, Option<int>> act)
		{
			// Arrange
			var msg = new TestMsg();
			var option = None<int>(msg);
			var none = Substitute.For<Action<IMsg>>();

			// Act
			var result = act(option, none);

			// Assert
			none.Received().Invoke(msg);
			Assert.Same(option, result);
		}

		public abstract void Test04_Some_Catches_Exception_And_Returns_Original_Option();

		protected static void Test04(Func<Option<int>, Action<int>, Option<int>> act)
		{
			// Arrange
			var option = Return(F.Rnd.Int);
			var exception = new Exception();
			void throwException(int _) => throw exception;

			// Act
			var result = act(option, throwException);

			// Assert
			Assert.Same(option, result);
		}

		public abstract void Test05_None_Catches_Exception_And_Returns_Original_Option();

		protected static void Test05(Func<Option<int>, Action<IMsg>, Option<int>> act)
		{
			// Arrange
			var option = None<int>(true);
			var exception = new Exception();
			void throwException(IMsg _) => throw exception;

			// Act
			var result = act(option, throwException);

			// Assert
			Assert.Same(option, result);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}
