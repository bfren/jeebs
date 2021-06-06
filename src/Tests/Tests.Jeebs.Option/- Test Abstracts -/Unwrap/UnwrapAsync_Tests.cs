// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs_Tests
{
	public abstract class UnwrapAsync_Tests
	{
		public abstract Task Test00_None_Runs_IfNone_Func_Returns_Value();

		protected static async Task Test00(Func<Task<Option<int>>, Func<int>, Task<int>> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Create.EmptyNone<int>();
			var ifNone = Substitute.For<Func<int>>();
			ifNone.Invoke().Returns(value);

			// Act
			var result = await act(option.AsTask, ifNone);

			// Assert
			ifNone.Received().Invoke();
			Assert.Equal(value, result);
		}

		public abstract Task Test01_None_With_Reason_Runs_IfNone_Func_Passes_Reason_Returns_Value();

		protected static async Task Test01(Func<Task<Option<int>>, Func<IMsg, int>, Task<int>> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var msg = Substitute.For<IMsg>();
			var option = None<int>(msg);
			var ifNone = Substitute.For<Func<IMsg, int>>();
			ifNone.Invoke(msg).Returns(value);

			// Act
			var result = await act(option.AsTask, ifNone);

			// Assert
			ifNone.Received().Invoke(msg);
			Assert.Equal(value, result);
		}

		public abstract Task Test02_Some_Returns_Value();

		protected static async Task Test02(Func<Task<Option<int>>, Task<int>> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);

			// Act
			var result = await act(option.AsTask);

			// Assert
			Assert.Equal(value, result);
		}
	}
}
