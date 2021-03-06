using System;
using NSubstitute;
using Xunit;

namespace Jeebs.Option_Tests
{
	public class Match_Tests
	{
		[Fact]
		public void If_Some_Run_Some()
		{
			// Arrange
			var value = F.Rnd.Int;
			var some = Option.Wrap(value);
			var none = Substitute.For<Func<string>>();

			// Act
			var result = some.Match(
				some: x => x.ToString(),
				none: none
			);

			// Assert
			Assert.Equal(value.ToString(), result);
			none.DidNotReceive().Invoke();
		}

		[Fact]
		public void If_None_Run_None()
		{
			// Arrange
			const string value = "18";
			var some = Substitute.For<Func<int, string>>();
			var none = Option.None<int>();

			// Act
			var result = none.Match(
				some: some,
				none: () => value
			);

			// Assert
			Assert.Equal(value, result);
			some.DidNotReceive().Invoke(Arg.Any<int>());
		}

		[Fact]
		public void If_None_Get_None()
		{
			// Arrange
			const string value = "18";
			var some = Substitute.For<Func<int, string>>();
			var none = Option.None<int>();

			// Act
			var result = none.Match(
				some: some,
				none: value
			);

			// Assert
			Assert.Equal(value, result);
			some.DidNotReceive().Invoke(Arg.Any<int>());
		}
	}
}
