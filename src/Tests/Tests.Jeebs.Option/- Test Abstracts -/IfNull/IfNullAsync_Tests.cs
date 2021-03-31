// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;
using static F.OptionF;

namespace Jeebs_Tests
{
	public abstract class IfNullAsync_Tests
	{
		public abstract Task Test00_Exception_In_NullValue_Func_Returns_None_With_UnhandledExceptionMsg();

		protected static async Task Test00(Func<Option<object?>, Func<Task<Option<object?>>>, Task<Option<object?>>> act)
		{
			// Arrange
			var some = Return<object>(null, true);
			var none = None<object?, Msg.NullValueMsg>();
			var throws = Substitute.For<Func<Task<Option<object?>>>>();
			throws.Invoke().Throws<Exception>();

			// Act
			var r0 = await act(some, throws);
			var r1 = await act(none, throws);

			// Assert
			var n0 = r0.AssertNone();
			Assert.IsType<Msg.UnhandledExceptionMsg>(n0);
			var n1 = r1.AssertNone();
			Assert.IsType<Msg.UnhandledExceptionMsg>(n1);
		}

		public abstract Task Test01_Some_With_Null_Value_Runs_NullValue_Func();

		protected static async Task Test01(Func<Option<object?>, Func<Task<Option<object?>>>, Task<Option<object?>>> act)
		{
			// Arrange
			var some = Return<object>(null, true);
			var nullValue = Substitute.For<Func<Task<Option<object?>>>>();

			// Act
			await act(some, nullValue);

			// Assert
			await nullValue.Received().Invoke();
		}

		public abstract Task Test02_None_With_NullValueMsg_Runs_NullValue_Func();

		protected static async Task Test02(Func<Option<object>, Func<Task<Option<object>>>, Task<Option<object>>> act)
		{
			// Arrange
			var none = None<object, Msg.NullValueMsg>();
			var nullValue = Substitute.For<Func<Task<Option<object>>>>();

			// Act
			await act(none, nullValue);

			// Assert
			await nullValue.Received().Invoke();
		}
	}
}
