// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs_Tests
{
	public abstract class AuditAsync_Tests
	{
		public abstract Task Test00_Some_Runs_Audit_Action_And_Returns_Original_Option();

		protected static async Task Test00(Func<Option<bool>, Action<Option<bool>>, Task<Option<bool>>> act)
		{
			// Arrange
			var option = True;
			var audit = Substitute.For<Action<Option<bool>>>();

			// Act
			var result = await act(option, audit);

			// Assert
			audit.Received().Invoke(option);
			Assert.Same(option, result);
		}

		public abstract Task Test01_None_Runs_Audit_Action_And_Returns_Original_Option();

		protected static async Task Test01(Func<Option<bool>, Action<Option<bool>>, Task<Option<bool>>> act)
		{
			// Arrange
			var option = None<bool>(true);
			var audit = Substitute.For<Action<Option<bool>>>();

			// Act
			var result = await act(option, audit);

			// Assert
			audit.Received().Invoke(option);
			Assert.Same(option, result);
		}

		public abstract Task Test02_Some_Runs_Audit_Func_And_Returns_Original_Option();

		protected static async Task Test02(Func<Option<bool>, Func<Option<bool>, Task>, Task<Option<bool>>> act)
		{
			// Arrange
			var option = True;
			var audit = Substitute.For<Func<Option<bool>, Task>>();

			// Act
			var result = await act(option, audit);

			// Assert
			await audit.Received().Invoke(option);
			Assert.Same(option, result);
		}

		public abstract Task Test03_None_Runs_Audit_Func_And_Returns_Original_Option();

		protected static async Task Test03(Func<Option<bool>, Func<Option<bool>, Task>, Task<Option<bool>>> act)
		{
			// Arrange
			var option = None<bool>(true);
			var audit = Substitute.For<Func<Option<bool>, Task>>();

			// Act
			var result = await act(option, audit);

			// Assert
			await audit.Received().Invoke(option);
			Assert.Same(option, result);
		}

		public abstract Task Test04_Some_Runs_Audit_Action_Catches_Exception_And_Returns_Original_Option();

		protected static async Task Test04(Func<Option<bool>, Action<Option<bool>>, Task<Option<bool>>> act)
		{
			// Arrange
			var option = True;
			static void throwException(Option<bool> _) => throw new Exception();

			// Act
			var result = await act(option, throwException);

			// Assert
			Assert.Same(option, result);
		}

		public abstract Task Test05_None_Runs_Audit_Action_Catches_Exception_And_Returns_Original_Option();

		protected static async Task Test05(Func<Option<bool>, Action<Option<bool>>, Task<Option<bool>>> act)
		{
			// Arrange
			var option = None<bool>(true);
			static void throwException(Option<bool> _) => throw new Exception();

			// Act
			var result = await act(option, throwException);

			// Assert
			Assert.Same(option, result);
		}

		public abstract Task Test06_Some_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Option();

		protected static async Task Test06(Func<Option<bool>, Func<Option<bool>, Task>, Task<Option<bool>>> act)
		{
			// Arrange
			var option = True;
			static Task throwException(Option<bool> _) => throw new Exception();

			// Act
			var result = await act(option, throwException);

			// Assert
			Assert.Same(option, result);
		}

		public abstract Task Test07_None_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Option();

		protected static async Task Test07(Func<Option<bool>, Func<Option<bool>, Task>, Task<Option<bool>>> act)
		{
			// Arrange
			var option = None<bool>(true);
			static Task throwException(Option<bool> _) => throw new Exception();

			// Act
			var result = await act(option, throwException);

			// Assert
			Assert.Same(option, result);
		}
	}
}
