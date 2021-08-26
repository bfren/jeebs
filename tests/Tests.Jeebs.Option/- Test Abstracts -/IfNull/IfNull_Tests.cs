// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;
using static F.OptionF;

namespace Jeebs_Tests
{
	public abstract class IfNull_Tests
	{
		public abstract void Test00_Exception_In_IfNull_Func_Returns_None_With_UnhandledExceptionMsg();

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

		public abstract void Test01_Some_With_Null_Value_Runs_IfNull_Func();

		protected static void Test01(Func<Option<object?>, Func<Option<object?>>, Option<object?>> act)
		{
			// Arrange
			var option = Return<object>(null, true);
			var ifNull = Substitute.For<Func<Option<object?>>>();

			// Act
			act(option, ifNull);

			// Assert
			ifNull.Received().Invoke();
		}

		public abstract void Test02_None_With_NullValueMsg_Runs_IfNull_Func();

		protected static void Test02(Func<Option<object>, Func<Option<object>>, Option<object>> act)
		{
			// Arrange
			var option = None<object, Msg.NullValueMsg>();
			var ifNull = Substitute.For<Func<Option<object>>>();

			// Act
			act(option, ifNull);

			// Assert
			ifNull.Received().Invoke();
		}

		public abstract void Test03_Some_With_Null_Value_Runs_IfNull_Func_Returns_None_With_Reason();

		protected static void Test03(Func<Option<object?>, Func<IMsg>, Option<object?>> act)
		{
			// Arrange
			var option = Return<object>(null, true);
			var ifNull = Substitute.For<Func<IMsg>>();
			var msg = new TestMsg();
			ifNull.Invoke().Returns(msg);

			// Act
			var result = act(option, ifNull);

			// Assert
			ifNull.Received().Invoke();
			var none = result.AssertNone();
			Assert.Same(msg, none);
		}

		public abstract void Test04_None_With_NullValueMsg_Runs_IfNull_Func_Returns_None_With_Reason();

		protected static void Test04(Func<Option<object>, Func<IMsg>, Option<object>> act)
		{
			// Arrange
			var option = None<object, Msg.NullValueMsg>();
			var ifNull = Substitute.For<Func<IMsg>>();
			var msg = new TestMsg();
			ifNull.Invoke().Returns(msg);

			// Act
			var result = act(option, ifNull);

			// Assert
			ifNull.Received().Invoke();
			var none = result.AssertNone();
			Assert.Same(msg, none);
		}

		public sealed record class TestMsg : IMsg;
	}
}
