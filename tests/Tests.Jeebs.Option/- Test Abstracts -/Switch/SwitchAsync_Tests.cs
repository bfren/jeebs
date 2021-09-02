// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs_Tests
{
	public abstract class SwitchAsync_Tests
	{
		public abstract Task Test00_If_Unknown_Option_Throws_UnknownOptionException();

		protected static async Task Test00(Func<Option<int>, Task<string>> act)
		{
			// Arrange
			var option = new FakeOption();

			// Act
			async Task<string> action() => await act(option);

			// Assert
			await Assert.ThrowsAsync<UnknownOptionException>(action);
		}

		public abstract Task Test01_If_None_Runs_None_Func_With_Reason();

		protected static async Task Test01(Func<Option<int>, Func<IMsg, Task<string>>, Task<string>> act)
		{
			// Arrange
			var reason = new TestMsg();
			var option = None<int>(reason);
			var none = Substitute.For<Func<IMsg, Task<string>>>();

			// Act
			var result = await act(option, none);

			// Assert
			await none.Received().Invoke(reason);
		}

		public abstract Task Test02_If_Some_Runs_Some_Func_With_Value();

		protected static async Task Test02(Func<Option<int>, Func<int, Task<string>>, Task<string>> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Some(value);
			var some = Substitute.For<Func<int, Task<string>>>();

			// Act
			var result = await act(option, some);

			// Assert
			await some.Received().Invoke(value);
		}

		public record class FakeOption : Option<int> { }

		public record class TestMsg : IMsg { }
	}
}
