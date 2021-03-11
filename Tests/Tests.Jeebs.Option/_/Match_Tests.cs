// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using NSubstitute;
using Xunit;

namespace Jeebs.Option_Tests
{
	public class Match_Tests
	{
		[Fact]
		public void If_Unknown_Option_Throws_UnknownOptionException()
		{
			// Arrange
			var option = new FakeOption();
			var some = Substitute.For<Func<int, string>>();
			var none = Substitute.For<Func<IMsg?, string>>();

			// Act
			void action() => option.DoMatch(some, none);

			// Assert
			Assert.Throws<Jx.Option.UnknownOptionException>(action);
		}

		[Fact]
		public void If_Some_Runs_Some()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Option.Wrap(value);
			var some = Substitute.For<Func<int, string>>();

			// Act
			option.DoMatch(
				some: some,
				none: Substitute.For<Func<IMsg?, string>>()
			);

			option.Match(
				some: some,
				none: F.Rnd.Str
			);

			option.Match(
				some: some,
				none: Substitute.For<Func<string>>()
			);

			option.Match(
				some: some,
				none: Substitute.For<Func<IMsg?, string>>()
			);

			// Assert
			some.Received(4).Invoke(value);
		}

		[Fact]
		public void If_None_Gets_None()
		{
			// Arrange
			var option = Option.None<int>(true);
			var value = F.Rnd.Str;

			// Act
			var result = option.Match(
				some: Substitute.For<Func<int, string>>(),
				none: value
			);

			// Assert
			Assert.Equal(value, result);
		}

		[Fact]
		public void If_None_Runs_None()
		{
			// Arrange
			var option = Option.None<int>(true);
			var none = Substitute.For<Func<string>>();

			// Act
			option.Match(
				some: Substitute.For<Func<int, string>>(),
				none: none
			);

			option.Match(
				some: Substitute.For<Func<int, string>>(),
				none: _ => none()
			);

			// Assert
			none.Received(2).Invoke();
		}

		public class FakeOption : Option<int> { }
	}
}
