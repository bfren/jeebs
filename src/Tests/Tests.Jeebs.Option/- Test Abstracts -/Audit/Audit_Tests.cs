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
	public abstract class Audit_Tests
	{
		#region General

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

		#endregion

		#region Any

		public abstract void Test02_Some_Runs_Audit_And_Returns_Original_Option();

		protected static void Test02(Func<Option<bool>, Action<Option<bool>>, Option<bool>> act)
		{
			// Arrange
			var option = True;
			var audit = Substitute.For<Action<Option<bool>>>();

			// Act
			var result = act(option, audit);

			// Assert
			audit.Received().Invoke(option);
			Assert.Same(option, result);
		}

		public abstract void Test03_None_Runs_Audit_And_Returns_Original_Option();

		protected static void Test03(Func<Option<bool>, Action<Option<bool>>, Option<bool>> act)
		{
			// Arrange
			var option = Create.EmptyNone<bool>();
			var audit = Substitute.For<Action<Option<bool>>>();

			// Act
			var result = act(option, audit);

			// Assert
			audit.Received().Invoke(option);
			Assert.Same(option, result);
		}

		public abstract void Test04_Some_Catches_Exception_And_Returns_Original_Option();

		protected static void Test04(Func<Option<bool>, Action<Option<bool>>, Option<bool>> act)
		{
			// Arrange
			var some = True;
			static void throwException(Option<bool> _) => throw new Exception();

			// Act
			var result = act(some, throwException);

			// Assert
			Assert.Same(some, result);
		}

		public abstract void Test05_None_Catches_Exception_And_Returns_Original_Option();

		protected static void Test05(Func<Option<bool>, Action<Option<bool>>, Option<bool>> act)
		{
			// Arrange
			var none = Create.EmptyNone<bool>();
			static void throwException(Option<bool> _) => throw new Exception();

			// Act
			var result = act(none, throwException);

			// Assert
			Assert.Same(none, result);
		}

		#endregion

		#region Some / None

		public abstract void Test06_Some_Runs_Some_And_Returns_Original_Option();

		protected static void Test06(Func<Option<int>, Action<int>, Option<int>> act)
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

		public abstract void Test07_None_Runs_None_And_Returns_Original_Option();

		protected static void Test07(Func<Option<int>, Action<IMsg>, Option<int>> act)
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

		public abstract void Test08_Some_Catches_Exception_And_Returns_Original_Option();

		protected static void Test08(Func<Option<int>, Action<int>, Option<int>> act)
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

		public abstract void Test09_None_Catches_Exception_And_Returns_Original_Option();

		protected static void Test09(Func<Option<int>, Action<IMsg>, Option<int>> act)
		{
			// Arrange
			var option = Create.EmptyNone<int>();
			var exception = new Exception();
			void throwException(IMsg _) => throw exception;

			// Act
			var result = act(option, throwException);

			// Assert
			Assert.Same(option, result);
		}

		#endregion

		public record FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}
