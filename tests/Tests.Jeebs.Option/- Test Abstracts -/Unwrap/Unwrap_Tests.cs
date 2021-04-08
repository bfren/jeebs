// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs_Tests
{
	public abstract class Unwrap_Tests
	{
		public abstract void Test00_None_Runs_IfNone_Func_Returns_Value();

		protected static void Test00(Func<Option<int>, Func<int>, int> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Create.EmptyNone<int>();
			var ifNone = Substitute.For<Func<int>>();
			ifNone.Invoke().Returns(value);

			// Act
			var result = act(option, ifNone);

			// Assert
			ifNone.Received().Invoke();
			Assert.Equal(value, result);
		}

		public abstract void Test01_None_With_Reason_Runs_IfNone_Func_Passes_Reason_Returns_Value();

		protected static void Test01(Func<Option<int>, Func<IMsg, int>, int> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var msg = Substitute.For<IMsg>();
			var option = None<int>(msg);
			var ifNone = Substitute.For<Func<IMsg, int>>();
			ifNone.Invoke(msg).Returns(value);

			// Act
			var result = act(option, ifNone);

			// Assert
			ifNone.Received().Invoke(msg);
			Assert.Equal(value, result);
		}

		public abstract void Test02_Some_Returns_Value();

		protected static void Test02(Func<Option<int>, int> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);

			// Act
			var result = act(option);

			// Assert
			Assert.Equal(value, result);
		}
	}
}
