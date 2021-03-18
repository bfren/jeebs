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
	public class AuditSwitchAsync_Tests
	{
		[Fact]
		public async Task Null_Args_Returns_Original_Option()
		{
			// Arrange
			var option = new FakeOption();

			// Act
			var r0 = await AuditSwitchAsync(option, null, null);
			var r1 = await AuditSwitchAsync(option.AsTask, null, null);

			// Assert
			Assert.Same(option, r0);
			Assert.Same(option, r1);
		}

		[Fact]
		public async Task If_Unknown_Option_Throws_UnknownOptionException()
		{
			// Arrange
			var option = new FakeOption();
			var some = Substitute.For<Func<int, Task>>();
			var none = Substitute.For<Func<IMsg, Task>>();

			// Act
			Task r0() => AuditSwitchAsync(option, some, null);
			Task r1() => AuditSwitchAsync(option, null, none);
			Task r2() => AuditSwitchAsync(option.AsTask, some, null);
			Task r3() => AuditSwitchAsync(option.AsTask, null, none);

			// Assert
			await Assert.ThrowsAsync<UnknownOptionException>(r0);
			await Assert.ThrowsAsync<UnknownOptionException>(r1);
			await Assert.ThrowsAsync<UnknownOptionException>(r2);
			await Assert.ThrowsAsync<UnknownOptionException>(r3);
		}

		[Fact]
		public async Task Some_Runs_Some_And_Returns_Original_Option()
		{
			// Arrange
			var value = Rnd.Int;
			var option = Return(value);
			var some = Substitute.For<Func<int, Task>>();

			// Act
			var r0 = await AuditSwitchAsync(option, some, null);
			var r1 = await AuditSwitchAsync(option.AsTask, some, null);

			// Assert
			await some.Received(2).Invoke(value);
			Assert.Same(option, r0);
			Assert.Same(option, r1);
		}

		[Fact]
		public async Task None_Runs_None_And_Returns_Original_Option()
		{
			// Arrange
			var msg = new TestMsg();
			var option = None<int>(msg);
			var none = Substitute.For<Func<IMsg, Task>>();

			// Act
			var r0 = await AuditSwitchAsync(option, null, none);
			var r1 = await AuditSwitchAsync(option.AsTask, null, none);

			// Assert
			await none.Received(2).Invoke(msg);
			Assert.Same(option, r0);
			Assert.Same(option, r1);
		}

		[Fact]
		public async Task Catches_Exception_And_Returns_Original_Option()
		{
			// Arrange
			var some = Return(Rnd.Int);
			var none = None<int>(true);
			var exception = new Exception();

			Task someThrow(int _) => throw exception!;
			Task noneThrow(IMsg _) => throw exception!;

			// Act
			var r0 = await AuditSwitchAsync(some, someThrow, null);
			var r1 = await AuditSwitchAsync(none, null, noneThrow);
			var r2 = await AuditSwitchAsync(some.AsTask, someThrow, null);
			var r3 = await AuditSwitchAsync(none.AsTask, null, noneThrow);

			// Assert
			Assert.Same(some, r0);
			Assert.Same(none, r1);
			Assert.Same(some, r2);
			Assert.Same(none, r3);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}
