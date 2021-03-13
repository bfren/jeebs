// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs.Option_Tests
{
	public class MatchExtensionsAsync_Tests
	{
		[Fact]
		public async Task If_Unknown_Option_Throws_UnknownOptionException()
		{
			// Arrange
			var option = new FakeOption();
			var task = option.AsTask;
			var some = Substitute.For<Func<int, Task<string>>>();
			var none = Substitute.For<Func<IMsg?, Task<string>>>();

			// Act
			Task action() => OptionExtensions.DoMatchAsync(task, some, none);

			// Assert
			await Assert.ThrowsAsync<UnknownOptionException>(action);
		}

		[Fact]
		public async Task If_Some_Runs_Some()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);
			var task = option.AsTask;
			var some = Substitute.For<Func<int, Task<string>>>();

			// Act
			await OptionExtensions.DoMatchAsync(
				task,
				some: some,
				none: Substitute.For<Func<IMsg?, Task<string>>>()
			);

			await task.MatchAsync(
				some: v => some(v).GetAwaiter().GetResult(),
				none: F.Rnd.Str
			);

			await task.MatchAsync(
				some: some,
				none: F.Rnd.Str
			);

			await task.MatchAsync(
				some: v => some(v).GetAwaiter().GetResult(),
				none: Task.FromResult(F.Rnd.Str)
			);

			await task.MatchAsync(
				some: v => some(v).GetAwaiter().GetResult(),
				none: Substitute.For<Func<string>>()
			);

			await task.MatchAsync(
				some: some,
				none: Substitute.For<Func<string>>()
			);

			await task.MatchAsync(
				some: v => some(v).GetAwaiter().GetResult(),
				none: Substitute.For<Func<Task<string>>>()
			);

			await task.MatchAsync(
				some: some,
				none: Substitute.For<Func<Task<string>>>()
			);

			await task.MatchAsync(
				some: v => some(v).GetAwaiter().GetResult(),
				none: Substitute.For<Func<IMsg?, string>>()
			);

			await task.MatchAsync(
				some: v => some(v).GetAwaiter().GetResult(),
				none: Substitute.For<Func<IMsg?, Task<string>>>()
			);

			await task.MatchAsync(
				some: some,
				none: Substitute.For<Func<IMsg?, string>>()
			);

			await task.MatchAsync(
				some: some,
				none: Substitute.For<Func<IMsg?, Task<string>>>()
			);

			// Assert
			await some.Received(12).Invoke(value);
		}

		[Fact]
		public async Task If_None_Gets_None()
		{
			// Arrange
			var option = None<int>(true);
			var task = option.AsTask;
			var value = F.Rnd.Str;

			// Act
			var r0 = await task.MatchAsync(
				some: Substitute.For<Func<int, string>>(),
				none: value
			);

			var r1 = await task.MatchAsync(
				some: Substitute.For<Func<int, Task<string>>>(),
				none: value
			);

			var r2 = await task.MatchAsync(
				some: Substitute.For<Func<int, string>>(),
				none: Task.FromResult(value)
			);

			var r3 = await task.MatchAsync(
				some: Substitute.For<Func<int, Task<string>>>(),
				none: Task.FromResult(value)
			);

			// Assert
			Assert.Equal(value, r0);
			Assert.Equal(value, r1);
			Assert.Equal(value, r2);
			Assert.Equal(value, r3);
		}

		[Fact]
		public async Task If_None_Runs_None()
		{
			// Arrange
			var option = None<int>(true);
			var task = option.AsTask;
			var none = Substitute.For<Func<string>>();

			// Act
			await OptionExtensions.DoMatchAsync(
				task,
				some: Substitute.For<Func<int, Task<string>>>(),
				none: _ => Task.FromResult(none())
			);

			await task.MatchAsync(
				some: Substitute.For<Func<int, string>>(),
				none: none()
			);

			await task.MatchAsync(
				some: Substitute.For<Func<int, Task<string>>>(),
				none: none()
			);

			await task.MatchAsync(
				some: Substitute.For<Func<int, string>>(),
				none: Task.FromResult(none())
			);

			await task.MatchAsync(
				some: Substitute.For<Func<int, Task<string>>>(),
				none: Task.FromResult(none())
			);

			await task.MatchAsync(
				some: Substitute.For<Func<int, string>>(),
				none: none
			);

			await task.MatchAsync(
				some: Substitute.For<Func<int, Task<string>>>(),
				none: none
			);

			await task.MatchAsync(
				some: Substitute.For<Func<int, string>>(),
				none: () => Task.FromResult(none())
			);

			await task.MatchAsync(
				some: Substitute.For<Func<int, Task<string>>>(),
				none: () => Task.FromResult(none())
			);

			await task.MatchAsync(
				some: Substitute.For<Func<int, string>>(),
				none: _ => none()
			);

			await task.MatchAsync(
				some: Substitute.For<Func<int, Task<string>>>(),
				none: _ => none()
			);

			await task.MatchAsync(
				some: Substitute.For<Func<int, string>>(),
				none: _ => Task.FromResult(none())
			);

			await task.MatchAsync(
				some: Substitute.For<Func<int, Task<string>>>(),
				none: _ => Task.FromResult(none())
			);

			// Assert
			none.Received(13).Invoke();
		}

		public class FakeOption : Option<int> { }
	}
}
