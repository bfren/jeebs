// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs_Tests
{
	public abstract class Audit_Tests
	{
		public abstract void Test00_Some_Runs_Audit_And_Returns_Original_Option();

		protected static void Test00(Func<Option<bool>, Action<Option<bool>>, Option<bool>> act)
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

		public abstract void Test01_None_Runs_Audit_And_Returns_Original_Option();

		protected static void Test01(Func<Option<bool>, Action<Option<bool>>, Option<bool>> act)
		{
			// Arrange
			var option = None<bool>(true);
			var audit = Substitute.For<Action<Option<bool>>>();

			// Act
			var result = act(option, audit);

			// Assert
			audit.Received().Invoke(option);
			Assert.Same(option, result);
		}

		public abstract void Test02_Some_Catches_Exception_And_Returns_Original_Option();

		protected static void Test02(Func<Option<bool>, Action<Option<bool>>, Option<bool>> act)
		{
			// Arrange
			var some = True;
			static void throwException(Option<bool> _) => throw new Exception();

			// Act
			var result = act(some, throwException);

			// Assert
			Assert.Same(some, result);
		}

		public abstract void Test03_None_Catches_Exception_And_Returns_Original_Option();

		protected static void Test03(Func<Option<bool>, Action<Option<bool>>, Option<bool>> act)
		{
			// Arrange
			var none = None<bool>(true);
			static void throwException(Option<bool> _) => throw new Exception();

			// Act
			var result = act(none, throwException);

			// Assert
			Assert.Same(none, result);
		}
	}
}
