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
	public partial class Switch_Tests
	{
		[Fact]
		public void Return_Type_If_Unknown_Option_Throws_UnknownOptionException()
		{
			// Arrange
			var option = new FakeOption();
			var some = Substitute.For<Func<int, string>>();
			var none = Substitute.For<Func<IMsg?, string>>();

			// Act
			void action() => Switch(option, some, none);

			// Assert
			Assert.Throws<UnknownOptionException>(action);
		}

		[Fact]
		public void Return_Type_If_Some_Runs_Some()
		{
			// Arrange
			var value = Rnd.Int;
			var option = Return(value);
			var some = Substitute.For<Func<int, string>>();
			var none = Substitute.For<Func<IMsg?, string>>();

			// Act
			Switch(option, some, none);

			// Assert
			some.Received().Invoke(value);
			none.DidNotReceiveWithAnyArgs().Invoke(null);
		}

		[Fact]
		public void Return_Type_If_None_Without_Reason_Runs_None()
		{
			// Arrange
			var option = None<int>(true);
			var some = Substitute.For<Func<int, string>>();
			var none = Substitute.For<Func<IMsg?, string>>();

			// Act
			Switch(option, some, none);

			// Assert
			some.DidNotReceiveWithAnyArgs().Invoke(Arg.Any<int>());
			none.Received().Invoke(null);
		}

		[Fact]
		public void Return_Type_If_None_With_Reason_Runs_None_With_Reason()
		{
			// Arrange
			var reason = new TestMsg();
			var option = None<int>(reason);
			var some = Substitute.For<Func<int, string>>();
			var none = Substitute.For<Func<IMsg?, string>>();

			// Act
			Switch(option, some, none);

			// Assert
			some.DidNotReceiveWithAnyArgs().Invoke(Arg.Any<int>());
			none.Received().Invoke(reason);
		}
	}
}
