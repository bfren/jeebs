// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests
{
	public class AuditSwitch_Tests
	{
		[Fact]
		public void Null_Args_Returns_Original_Option()
		{
			// Arrange
			var option = new FakeOption();

			// Act
			var result = AuditSwitch(option, null, null);

			// Assert
			Assert.Same(option, result);
		}

		[Fact]
		public void If_Unknown_Option_Throws_UnknownOptionException()
		{
			// Arrange
			var option = new FakeOption();
			var some = Substitute.For<Action<int>>();
			var none = Substitute.For<Action<IMsg>>();

			// Act
			void r0() => AuditSwitch(option, some, null);
			void r1() => AuditSwitch(option, null, none);

			// Assert
			Assert.Throws<UnknownOptionException>(r0);
			Assert.Throws<UnknownOptionException>(r1);
		}

		[Fact]
		public void Some_Runs_Some_And_Returns_Original_Option()
		{
			// Arrange
			var value = Rnd.Int;
			var option = Return(value);
			var some = Substitute.For<Action<int>>();

			// Act
			var result = AuditSwitch(option, some, null);

			// Assert
			some.Received().Invoke(value);
			Assert.Same(option, result);
		}

		[Fact]
		public void None_Runs_None_And_Returns_Original_Option()
		{
			// Arrange
			var msg = new TestMsg();
			var option = None<int>(msg);
			var none = Substitute.For<Action<IMsg>>();

			// Act
			var result = AuditSwitch(option, null, none);

			// Assert
			none.Received().Invoke(msg);
			Assert.Same(option, result);
		}

		[Fact]
		public void Catches_Exception_And_Returns_Original_Option()
		{
			// Arrange
			var some = Return(Rnd.Int);
			var none = None<int>(true);
			var exception = new Exception();

			void someThrow(int _) => throw exception!;
			void noneThrow(IMsg _) => throw exception!;

			// Act
			var r0 = AuditSwitch(some, someThrow, null);
			var r1 = AuditSwitch(none, null, noneThrow);

			// Assert
			Assert.Same(some, r0);
			Assert.Same(none, r1);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}
