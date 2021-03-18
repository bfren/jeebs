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
	public class Match_Tests
	{
		[Fact]
		public void If_Unknown_Option_Throws_UnknownOptionException()
		{
			// Arrange
			var option = new FakeOption();
			var some = Substitute.For<Func<int, string>>();
			var none = Substitute.For<Func<IMsg, string>>();

			// Act
			void action() => Match(option, some, none);

			// Assert
			Assert.Throws<UnknownOptionException>(action);
		}

		[Fact]
		public void If_Some_Runs_Some()
		{
			// Arrange
			var value = Rnd.Int;
			var option = Return(value);
			var some = Substitute.For<Func<int, string>>();
			var none = Substitute.For<Func<IMsg, string>>();

			// Act
			Match(option, some, none);

			// Assert
			some.Received().Invoke(value);
		}

		[Fact]
		public void If_None_Gets_None()
		{
			// Arrange
			var option = None<int>(true);
			var value = Rnd.Str;
			var some = Substitute.For<Func<int, string>>();

			// Act
			var result = Match(option, some, _ => value);

			// Assert
			Assert.Equal(value, result);
		}

		[Fact]
		public void If_None_Runs_None()
		{
			// Arrange
			var option = None<int>(true);
			var some = Substitute.For<Func<int, string>>();
			var none = Substitute.For<Func<IMsg, string>>();

			// Act
			Match(option, some, none);

			// Assert
			none.Received().Invoke(Arg.Any<IMsg>());
		}

		[Fact]
		public void If_None_With_Reason_Runs_None_Passes_Reason()
		{
			// Arrange
			var msg = new TestMsg();
			var option = None<int>(msg);
			var some = Substitute.For<Func<int, string>>();
			var none = Substitute.For<Func<IMsg, string>>();

			// Act
			Match(option, some, none);

			// Assert
			none.Received().Invoke(msg);
		}

		public class FakeOption : Option<int> { }

		public class TestMsg : IMsg { }
	}
}
