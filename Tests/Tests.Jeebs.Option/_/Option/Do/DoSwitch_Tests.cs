﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using NSubstitute;
using Xunit;

namespace Jeebs.Option_Tests
{
	public class DoSwitch_Tests
	{
		[Fact]
		public void If_Unknown_Option_Throws_UnknownOptionException()
		{
			// Arrange
			var option = new FakeOption();
			var some = Substitute.For<Action<int>>();
			var none = Substitute.For<Action<IMsg?>>();

			// Act
			Action action = () => option.DoSwitch(some, none);

			// Assert
			Assert.Throws<Jx.Option.UnknownOptionException>(action);
		}

		[Fact]
		public void If_Some_Runs_Some()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Option.Wrap(value);
			var some = Substitute.For<Action<int>>();
			var none = Substitute.For<Action>();

			// Act
			option.DoSwitch(
				some: some,
				none: _ => none()
			);

			option.Switch(
				some: some,
				none: none
			);

			option.Switch(
				some: some,
				none: _ => none()
			);

			// Assert
			some.Received(3).Invoke(value);
			none.DidNotReceive().Invoke();
		}

		[Fact]
		public void If_None_Without_Reason_Runs_None()
		{
			// Arrange
			var option = Option.None<int>(true);
			var some = Substitute.For<Action<int>>();
			var none = Substitute.For<Action>();

			// Act
			option.DoSwitch(
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
			var option = Option.None<int>(reason);
			var some = Substitute.For<Action<int>>();
			var none = Substitute.For<Action<IMsg?>>();

			// Act
			option.DoSwitch(
				some: some,
				none: none
			);

			option.Switch(
				some: some,
				none: none
			);

			// Assert
			some.DidNotReceive().Invoke(Arg.Any<int>());
			none.Received(2).Invoke(Arg.Is(reason));
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}
