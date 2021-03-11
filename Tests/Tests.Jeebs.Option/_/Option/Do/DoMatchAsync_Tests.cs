// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Option_Tests
{
	public class DoMatchAsync_Tests
	{
		[Fact]
		public void If_Unknown_Option_Throws_UnknownOptionException()
		{
			// Arrange
			var option = new FakeOption();
			var some = Substitute.For<Func<int, Task<string>>>();
			var none = Substitute.For<Func<IMsg?, Task<string>>>();

			// Act
			Action action = () => option.DoMatchAsync(some, none);

			// Assert
			Assert.Throws<Jx.Option.UnknownOptionException>(action);
		}

		[Fact]
		public async Task If_Some_Runs_Some()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Option.Wrap(value);
			var some = Substitute.For<Func<int, Task<string>>>();

			// Act
			await option.DoMatchAsync(
				some: some,
				none: Substitute.For<Func<IMsg?, Task<string>>>()
			);

			await option.MatchAsync(
				some: some,
				none: F.Rnd.Str
			);

			await option.MatchAsync(
				some: some,
				none: Task.FromResult(F.Rnd.Str)
			);

			await option.MatchAsync(
				some: some,
				none: Substitute.For<Func<string>>()
			);

			await option.MatchAsync(
				some: v => some(v).GetAwaiter().GetResult(),
				none: Substitute.For<Func<Task<string>>>()
			);

			await option.MatchAsync(
				some: some,
				none: Substitute.For<Func<Task<string>>>()
			);

			await option.MatchAsync(
				some: some,
				none: Substitute.For<Func<IMsg?, string>>()
			);

			await option.MatchAsync(
				some: v => some(v).GetAwaiter().GetResult(),
				none: Substitute.For<Func<IMsg?, Task<string>>>()
			);

			await option.MatchAsync(
				some: some,
				none: Substitute.For<Func<IMsg?, Task<string>>>()
			);

			// Assert
			await some.Received(9).Invoke(value);
		}

		[Fact]
		public async Task If_None_Gets_None()
		{
			// Arrange
			var option = Option.None<int>(true);
			var value = F.Rnd.Str;

			// Act
			var r0 = await option.MatchAsync(
				some: Substitute.For<Func<int, Task<string>>>(),
				none: value
			);

			var r1 = await option.MatchAsync(
				some: Substitute.For<Func<int, Task<string>>>(),
				none: Task.FromResult(value)
			);

			// Assert
			Assert.Equal(value, r0);
			Assert.Equal(value, r1);
		}

		[Fact]
		public async Task If_None_Runs_None()
		{
			// Arrange
			var option = Option.None<int>(true);
			var none = Substitute.For<Func<string>>();

			// Act
			await option.MatchAsync(
				some: Substitute.For<Func<int, Task<string>>>(),
				none: none
			);

			await option.MatchAsync(
				some: Substitute.For<Func<int, string>>(),
				none: () => Task.FromResult(none())
			);

			await option.MatchAsync(
				some: Substitute.For<Func<int, Task<string>>>(),
				none: () => Task.FromResult(none())
			);

			await option.MatchAsync(
				some: Substitute.For<Func<int, Task<string>>>(),
				none: _ => none()
			);

			await option.MatchAsync(
				some: Substitute.For<Func<int, string>>(),
				none: _ => Task.FromResult(none())
			);

			await option.MatchAsync(
				some: Substitute.For<Func<int, Task<string>>>(),
				none: _ => Task.FromResult(none())
			);

			// Assert
			none.Received(6).Invoke();
		}

		public class FakeOption : Option<int> { }
	}
}
