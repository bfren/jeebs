// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using NSubstitute;
using Xunit;

namespace Jeebs.Option_Tests
{
	public class Map_Tests
	{
		[Fact]
		public void Some_Runs_Map_Function()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Option.Wrap(value);
			var map = Substitute.For<Func<int, string>>();

			// Act
			var result = option.Map(map);

			// Assert
			Assert.IsType<Some<string>>(result);
			map.Received().Invoke(value);
		}

		[Fact]
		public void None_Returns_None_Keeps_Reason()
		{
			// Arrange
			var option = Option.None<int>().AddReason<TestMsg>();
			var map = Substitute.For<Func<int, string>>();

			// Act
			var result = option.Map(map);

			// Assert
			var none = Assert.IsType<None<string>>(result);
			Assert.True(none.Reason is TestMsg);
			map.DidNotReceive().Invoke(Arg.Any<int>());
		}

		public class TestMsg : IMsg { }
	}
}
