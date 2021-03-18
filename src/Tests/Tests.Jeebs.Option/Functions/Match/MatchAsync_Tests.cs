// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests
{
	public class MatchAsync_Tests
	{
		[Fact]
		public async Task If_Unknown_Option_Throws_UnknownOptionException()
		{
			// Arrange
			var option = new FakeOption();
			var some = Substitute.For<Func<int, Task<string>>>();
			var none = Substitute.For<Func<IMsg, Task<string>>>();

			// Act
			Task a0() => MatchAsync(option, some, none);
			Task a1() => MatchAsync(option.AsTask, some, none);

			// Assert
			await Assert.ThrowsAsync<UnknownOptionException>(a0);
			await Assert.ThrowsAsync<UnknownOptionException>(a1);
		}

		[Fact]
		public async Task If_Some_Runs_Some()
		{
			// Arrange
			var value = Rnd.Int;
			var option = Return(value);
			var some = Substitute.For<Func<int, Task<string>>>();
			var none = Substitute.For<Func<IMsg, Task<string>>>();

			// Act
			await MatchAsync(option, some, none);
			await MatchAsync(option.AsTask, some, none);

			// Assert
			await some.Received(2).Invoke(value);
		}

		[Fact]
		public async Task If_None_Gets_None()
		{
			// Arrange
			var option = None<int>(true);
			var value = Rnd.Str;
			var some = Substitute.For<Func<int, Task<string>>>();

			// Act
			var r0 = await MatchAsync(option, some, _ => Task.FromResult(value));
			var r1 = await MatchAsync(option.AsTask, some, _ => Task.FromResult(value));

			// Assert
			Assert.Equal(value, r0);
			Assert.Equal(value, r1);
		}

		[Fact]
		public async Task If_None_Runs_None()
		{
			// Arrange
			var option = None<int>(true);
			var some = Substitute.For<Func<int, Task<string>>>();
			var none = Substitute.For<Func<IMsg, Task<string>>>();

			// Act
			await MatchAsync(option, some, none);
			await MatchAsync(option.AsTask, some, none);

			// Assert
			await none.Received(2).Invoke(Arg.Any<IMsg>());
		}

		[Fact]
		public async Task If_None_With_Reason_Runs_None_Passes_Reason()
		{
			// Arrange
			var msg = new TestMsg();
			var option = None<int>(msg);
			var some = Substitute.For<Func<int, Task<string>>>();
			var none = Substitute.For<Func<IMsg, Task<string>>>();

			// Act
			await MatchAsync(option, some, none);
			await MatchAsync(option.AsTask, some, none);

			// Assert
			await none.Received(2).Invoke(msg);
		}

		public class FakeOption : Option<int> { }

		public class TestMsg : IMsg { }
	}
}
