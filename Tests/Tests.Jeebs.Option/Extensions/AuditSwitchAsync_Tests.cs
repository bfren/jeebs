﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs.Option.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs.OptionExtensions_Tests
{
	public class AuditSwitchAsync_Tests
	{
		[Fact]
		public async Task Null_Args_Returns_Original_Option()
		{
			// Arrange
			var option = Return(F.Rnd.Int);
			var task = option.AsTask;

			// Act
			var result = await OptionExtensions.DoAuditSwitchAsync(task, null, null);

			// Assert
			Assert.Same(option, result);
		}

		[Fact]
		public async Task If_Unknown_Option_Throws_UnknownOptionException()
		{
			// Arrange
			var option = new FakeOption();
			var task = option.AsTask;
			var some = Substitute.For<Func<int, Task>>();
			var none = Substitute.For<Func<IMsg?, Task>>();

			// Act
			Task result() => OptionExtensions.DoAuditSwitchAsync(task, some, none);

			// Assert
			await Assert.ThrowsAsync<UnknownOptionException>(result);
		}

		[Fact]
		public async Task Some_Runs_Some_And_Returns_Original_Option()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);
			var task = Task.FromResult(option);
			var some = Substitute.For<Func<int, Task>>();

			// Act
			var r0 = await OptionExtensions.DoAuditSwitchAsync(task, some, null);
			var r1 = await task.AuditSwitchAsync(some: _ => { some(value); });
			var r2 = await task.AuditSwitchAsync(some: some);
			var r3 = await task.AuditSwitchAsync(some: _ => some(value), none: Substitute.For<Action<IMsg?>>());
			var r4 = await task.AuditSwitchAsync(some: some, none: Substitute.For<Func<IMsg?, Task>>());

			// Assert
			await some.Received(5).Invoke(value);
			Assert.Same(option, r0);
			Assert.Same(option, r1);
			Assert.Same(option, r2);
			Assert.Same(option, r3);
			Assert.Same(option, r4);
		}

		[Fact]
		public async Task None_Runs_None_And_Returns_Original_Option()
		{
			// Arrange
			var msg = new TestMsg();
			var option = None<int>(msg);
			var task = option.AsTask;
			var none = Substitute.For<Func<IMsg?, Task>>();

			// Act
			var r0 = await OptionExtensions.DoAuditSwitchAsync(task, null, none);
			var r1 = await task.AuditSwitchAsync(none: _ => { none(msg); });
			var r2 = await task.AuditSwitchAsync(none: none);
			var r3 = await task.AuditSwitchAsync(some: Substitute.For<Action<int>>(), none: _ => none(msg));
			var r4 = await task.AuditSwitchAsync(some: Substitute.For<Func<int, Task>>(), none: none);

			// Assert
			await none.Received(5).Invoke(msg);
			Assert.Same(option, r0);
			Assert.Same(option, r1);
			Assert.Same(option, r2);
			Assert.Same(option, r3);
			Assert.Same(option, r4);
		}

		[Fact]
		public async Task Catches_Exception_And_Returns_Original_Option()
		{
			// Arrange
			var o0 = Return(F.Rnd.Int);
			var o1 = None<int>(true);
			var t0 = o0.AsTask;
			var t1 = o1.AsTask;
			var exception = new Exception();

			void someActionThrow(int _) => throw exception!;
			Task someFuncThrow(int _) => throw exception!;
			void noneActionThrow(IMsg? _) => throw exception!;
			Task noneFuncThrow(IMsg? _) => throw exception!;

			// Act
			var r0 = await OptionExtensions.DoAuditSwitchAsync(t0, someFuncThrow, null);
			var r1 = await t0.AuditSwitchAsync(some: someActionThrow);
			var r2 = await t0.AuditSwitchAsync(some: someFuncThrow);
			var r3 = await OptionExtensions.DoAuditSwitchAsync(t1, null, noneFuncThrow);
			var r4 = await t1.AuditSwitchAsync(none: noneActionThrow);
			var r5 = await t1.AuditSwitchAsync(none: noneFuncThrow);
			var r6 = await t0.AuditSwitchAsync(some: someActionThrow, none: noneActionThrow);
			var r7 = await t0.AuditSwitchAsync(some: someFuncThrow, none: noneFuncThrow);
			var r8 = await t1.AuditSwitchAsync(some: someActionThrow, none: noneActionThrow);
			var r9 = await o1.AuditSwitchAsync(some: someFuncThrow, none: noneFuncThrow);

			// Assert
			Assert.Same(o0, r0);
			Assert.Same(o0, r1);
			Assert.Same(o0, r2);
			Assert.Same(o1, r3);
			Assert.Same(o1, r4);
			Assert.Same(o1, r5);
			Assert.Same(o0, r6);
			Assert.Same(o0, r7);
			Assert.Same(o1, r8);
			Assert.Same(o1, r9);
		}

		public class FakeOption : Option<int>
		{
			public Option<int> AsOption => this;
		}

		public record TestMsg : IMsg { }
	}
}
