// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Option_Tests
{
	public class Bind_Tests
	{
		[Fact]
		public void Some_Runs_Bind_Returns_Some_Returns_Some()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Option.Wrap(value);
			static Option<string> bind(int x) => x.ToString();

			// Act
			var result = option.Bind(bind);

			// Assert
			var some = Assert.IsType<Some<string>>(result);
			Assert.Equal(value.ToString(), some.Value);
		}

		[Fact]
		public void Some_Runs_Bind_Returns_None_Returns_None()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Option.Wrap(value);
			static Option<string> bind(int _) => Option.None<string>(true);

			// Act
			var result = option.Bind(bind);

			// Assert
			Assert.IsType<None<string>>(result);
		}

		[Fact]
		public void None_Returns_None_Keeps_Reason()
		{
			// Arrange
			var option = Option.None<int>(new TestMsg());
			static Option<string> bind(int _) => Option.None<string>(true);

			// Act
			var result = option.Bind(bind);

			// Assert
			var none = Assert.IsType<None<string>>(result);
			Assert.True(none.Reason is TestMsg);
		}

		public class TestMsg : IMsg { }
	}
}
