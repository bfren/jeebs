﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Option_Tests
{
	public class DoAuditSwitch_Tests
	{
		[Fact]
		public void Null_Args_Returns_Original_Option()
		{
			// Arrange
			var option = new FakeOption();

			// Act
			var result = option.DoAuditSwitch();

			// Assert
			Assert.Same(option, result);
		}

		[Fact]
		public void If_Unknown_Option_Throws_UnknownOptionException()
		{
			// Arrange
			var option = new FakeOption();
			var some = Substitute.For<Func<int, Task>>();
			var none = Substitute.For<Func<IMsg?, Task>>();

			// Act
			void result() => option.DoAuditSwitchAsync(some, none);

			// Assert
			Assert.Throws<Jx.Option.UnknownOptionException>(result);
		}

		[Fact]
		public void Some_Runs_Some_And_Returns_Original_Option()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Option.Wrap(value);
			var some = Substitute.For<Action<int>>();

			// Act
			var r0 = option.DoAuditSwitch(some: some);
			var r1 = option.AuditSwitch(some: some);
			var r2 = option.AuditSwitch(some: some, none: Substitute.For<Action<IMsg?>>());

			// Assert
			some.Received(3).Invoke(value);
			Assert.Same(option, r0);
			Assert.Same(option, r1);
			Assert.Same(option, r2);
		}

		[Fact]
		public void None_Runs_None_And_Returns_Original_Option()
		{
			// Arrange
			var msg = new TestMsg();
			var option = Option.None<int>(msg);
			var none = Substitute.For<Action<IMsg?>>();

			// Act
			var r0 = option.DoAuditSwitch(none: none);
			var r1 = option.AuditSwitch(none: none);
			var r2 = option.AuditSwitch(some: Substitute.For<Action<int>>(), none: none);

			// Assert
			none.Received(3).Invoke(msg);
			Assert.Same(option, r0);
			Assert.Same(option, r1);
			Assert.Same(option, r2);
		}

		[Fact]
		public void Handles_Exception_And_Returns_Original_Option()
		{
			// Arrange
			var o0 = Option.Wrap(F.Rnd.Int);
			var o1 = Option.None<int>(true);
			var exception = new Exception();

			void someThrow(int _) => throw exception!;
			void noneThrow(IMsg? _) => throw exception!;

			// Act
			var r0 = o0.DoAuditSwitch(some: someThrow);
			var r1 = o0.AuditSwitch(some: someThrow);
			var r2 = o1.DoAuditSwitch(none: noneThrow);
			var r3 = o1.AuditSwitch(none: noneThrow);
			var r4 = o0.AuditSwitch(some: someThrow, none: noneThrow);
			var r5 = o1.AuditSwitch(some: someThrow, none: noneThrow);

			// Assert
			Assert.Same(o0, r0);
			Assert.Same(o0, r1);
			Assert.Same(o1, r2);
			Assert.Same(o1, r3);
			Assert.Same(o0, r4);
			Assert.Same(o1, r5);
		}

		public class FakeOption : Option<int> { }

		public class TestMsg : IMsg { }
	}
}
