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
	public class AuditSwitchAsync_Tests
	{
		[Fact]
		public async Task If_Unknown_Option_Throws_UnknownOptionException()
		{
			// Arrange
			var option = new FakeOption();
			var some = Substitute.For<Action<int>>();
			var none = Substitute.For<Action<IMsg?>>();

			// Act
			Task r0() => option.AuditSwitchAsync(some);
			Task r1() => option.AuditSwitchAsync(none);
			Task r2() => option.AuditSwitchAsync(some, none);
			Task r3() => option.AsTask.AuditSwitchAsync(some);
			Task r4() => option.AsTask.AuditSwitchAsync(none);
			Task r5() => option.AsTask.AuditSwitchAsync(some, none);

			// Assert
			await Assert.ThrowsAsync<UnknownOptionException>(r0);
			await Assert.ThrowsAsync<UnknownOptionException>(r1);
			await Assert.ThrowsAsync<UnknownOptionException>(r2);
			await Assert.ThrowsAsync<UnknownOptionException>(r3);
			await Assert.ThrowsAsync<UnknownOptionException>(r4);
			await Assert.ThrowsAsync<UnknownOptionException>(r5);
		}

		[Fact]
		public async Task Some_Runs_Some_And_Returns_Original_Option()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);
			var some = Substitute.For<Func<int, Task>>();

			// Act
			var r0 = await option.AuditSwitchAsync(some);
			var r1 = await option.AuditSwitchAsync(some, Substitute.For<Func<IMsg?, Task>>());
			var r2 = await option.AsTask.AuditSwitchAsync(some);
			var r3 = await option.AsTask.AuditSwitchAsync(some, Substitute.For<Func<IMsg?, Task>>());

			// Assert
			await some.Received(4).Invoke(value);
			Assert.Same(option, r0);
			Assert.Same(option, r1);
			Assert.Same(option, r2);
			Assert.Same(option, r3);
		}

		[Fact]
		public async Task None_Runs_None_And_Returns_Original_Option()
		{
			// Arrange
			var msg = new TestMsg();
			var option = None<int>(msg);
			var none = Substitute.For<Func<IMsg?, Task>>();

			// Act
			var r0 = await option.AuditSwitchAsync(none);
			var r1 = await option.AuditSwitchAsync(Substitute.For<Func<int, Task>>(), none);
			var r2 = await option.AsTask.AuditSwitchAsync(none);
			var r3 = await option.AsTask.AuditSwitchAsync(Substitute.For<Func<int, Task>>(), none);

			// Assert
			await none.Received(4).Invoke(msg);
			Assert.Same(option, r0);
			Assert.Same(option, r1);
			Assert.Same(option, r2);
			Assert.Same(option, r3);
		}

		[Fact]
		public async Task Catches_Exception_And_Returns_Original_Option()
		{
			// Arrange
			var some = Return(F.Rnd.Int);
			var none = None<int>(true);
			var exception = new Exception();

			Task someThrow(int _) => throw exception!;
			Task noneThrow(IMsg? _) => throw exception!;

			// Act
			var r0 = await some.AuditSwitchAsync(someThrow);
			var r1 = await none.AuditSwitchAsync(noneThrow);
			var r2 = await some.AuditSwitchAsync(someThrow, noneThrow);
			var r3 = await none.AuditSwitchAsync(someThrow, noneThrow);
			var r4 = await some.AsTask.AuditSwitchAsync(someThrow);
			var r5 = await none.AsTask.AuditSwitchAsync(noneThrow);
			var r6 = await some.AsTask.AuditSwitchAsync(someThrow, noneThrow);
			var r7 = await none.AsTask.AuditSwitchAsync(someThrow, noneThrow);

			// Assert
			Assert.Same(some, r0);
			Assert.Same(none, r1);
			Assert.Same(some, r2);
			Assert.Same(none, r3);
			Assert.Same(some, r4);
			Assert.Same(none, r5);
			Assert.Same(some, r6);
			Assert.Same(none, r7);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}
