// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;
using static F.OptionF;

namespace Jeebs_Tests
{
	public abstract class IfNull_Tests
	{
		public abstract void Test00_Exception_In_NullValue_Func_Returns_None_With_UnhandledExceptionMsg();

		protected static void Test00(Func<Option<object?>, Func<Option<object?>>, Option<object?>> act)
		{
			// Arrange
			var some = Return<object>(null, true);
			var none = None<object?, Msg.NullValueMsg>();
			var throws = Substitute.For<Func<Option<object?>>>();
			throws.Invoke().Throws<Exception>();

			// Act
			var r0 = act(some, throws);
			var r1 = act(none, throws);

			// Assert
			var n0 = r0.AssertNone();
			Assert.IsType<Msg.UnhandledExceptionMsg>(n0);
			var n1 = r1.AssertNone();
			Assert.IsType<Msg.UnhandledExceptionMsg>(n1);
		}

		public abstract void Test01_Some_With_Null_Value_Runs_NullValue_Func();

		protected static void Test01(Func<Option<object?>, Func<Option<object?>>, Option<object?>> act)
		{
			// Arrange
			var some = Return<object>(null, true);
			var nullValue = Substitute.For<Func<Option<object?>>>();

			// Act
			act(some, nullValue);

			// Assert
			nullValue.Received().Invoke();
		}

		public abstract void Test02_None_With_NullValueMsg_Runs_NullValue_Func();

		protected static void Test02(Func<Option<object>, Func<Option<object>>, Option<object>> act)
		{
			// Arrange
			var none = None<object, Msg.NullValueMsg>();
			var nullValue = Substitute.For<Func<Option<object>>>();

			// Act
			act(none, nullValue);

			// Assert
			nullValue.Received().Invoke();
		}
	}
}
