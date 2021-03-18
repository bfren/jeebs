// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs.Option_Tests
{
	public class AuditSwitch_Tests
	{
		[Fact]
		public void If_Unknown_Option_Throws_UnknownOptionException()
		{
			// Arrange
			var option = new FakeOption();
			var some = Substitute.For<Action<int>>();
			var none = Substitute.For<Action<IMsg?>>();

			// Act
			void r0() => option.AuditSwitch(some);
			void r1() => option.AuditSwitch(none);
			void r2() => option.AuditSwitch(some, none);

			// Assert
			Assert.Throws<UnknownOptionException>(r0);
			Assert.Throws<UnknownOptionException>(r1);
			Assert.Throws<UnknownOptionException>(r2);
		}

		[Fact]
		public void Some_Runs_Some_And_Returns_Original_Option()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);
			var some = Substitute.For<Action<int>>();

			// Act
			var r0 = option.AuditSwitch(some);
			var r1 = option.AuditSwitch(some: some, none: Substitute.For<Action<IMsg?>>());

			// Assert
			some.Received(2).Invoke(value);
			Assert.Same(option, r0);
			Assert.Same(option, r1);
		}

		[Fact]
		public void None_Runs_None_And_Returns_Original_Option()
		{
			// Arrange
			var msg = new TestMsg();
			var option = None<int>(msg);
			var none = Substitute.For<Action<IMsg?>>();

			// Act
			var r0 = option.AuditSwitch(none);
			var r1 = option.AuditSwitch(Substitute.For<Action<int>>(), none);

			// Assert
			none.Received(2).Invoke(msg);
			Assert.Same(option, r0);
			Assert.Same(option, r1);
		}

		[Fact]
		public void Catches_Exception_And_Returns_Original_Option()
		{
			// Arrange
			var some = Return(F.Rnd.Int);
			var none = None<int>(true);
			var exception = new Exception();

			void someThrow(int _) => throw exception!;
			void noneThrow(IMsg? _) => throw exception!;

			// Act
			var r0 = some.AuditSwitch(someThrow);
			var r1 = none.AuditSwitch(noneThrow);
			var r2 = some.AuditSwitch(someThrow, noneThrow);
			var r3 = none.AuditSwitch(someThrow, noneThrow);

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
