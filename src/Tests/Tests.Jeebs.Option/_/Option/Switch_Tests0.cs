// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs.Option_Tests
{
	public class Switch_Tests0
	{
		[Fact]
		public void If_Unknown_Option_Throws_UnknownOptionException()
		{
			// Arrange
			var option = new FakeOption();
			var some = Substitute.For<Action<int>>();
			var none = Substitute.For<Action<IMsg>>();

			// Act
			void action() => option.Switch(some, none);

			// Assert
			Assert.Throws<UnknownOptionException>(action);
		}

		[Fact]
		public void If_Some_Runs_Some()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);
			var some = Substitute.For<Action<int>>();
			var none = Substitute.For<Action>();

			// Act
			option.Switch(
				some: some,
				none: none
			);

			option.Switch(
				some: some,
				none: _ => none()
			);

			// Assert
			some.Received(2).Invoke(value);
			none.DidNotReceive().Invoke();
		}

		[Fact]
		public void If_None_Without_Reason_Runs_None()
		{
			// Arrange
			var option = None<int>(true);
			var some = Substitute.For<Action<int>>();
			var none = Substitute.For<Action>();

			// Act
			option.Switch(
				some: some,
				none: _ => none()
			);

			option.Switch(
				some: some,
				none: none
			);

			// Assert
			some.DidNotReceive().Invoke(Arg.Any<int>());
			none.Received(2).Invoke();
		}

		[Fact]
		public void If_None_With_Reason_Runs_None_With_Reason()
		{
			// Arrange
			var reason = new TestMsg();
			var option = None<int>(reason);
			var some = Substitute.For<Action<int>>();
			var none = Substitute.For<Action<IMsg>>();

			// Act
			option.Switch(
				some: some,
				none: none
			);

			// Assert
			some.DidNotReceive().Invoke(Arg.Any<int>());
			none.Received().Invoke(Arg.Is(reason));
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}
