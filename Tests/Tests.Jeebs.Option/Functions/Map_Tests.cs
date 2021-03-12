// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using NSubstitute;
using Xunit;
using static JeebsF.OptionF;

namespace JeebsF.OptionF_Tests
{
	public class Map_Tests
	{
		[Fact]
		public void Exception_Thrown_Calls_Handler()
		{
			// Arrange
			var option = Return(Rnd.Str);
			var handler = Substitute.For<Handler>();
			var exception = new Exception();

			// Act
			var result = Map<int>(() => throw exception, handler);

			// Assert
			Assert.IsType<None<int>>(result);
			handler.Received().Invoke(exception);
		}

		[Fact]
		public void Runs_Map_Function()
		{
			// Arrange
			var value = Rnd.Int;
			var option = Return(value);
			var map = Substitute.For<Func<string>>();

			// Act
			Map(map, null);

			// Assert
			map.Received(1).Invoke();
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}