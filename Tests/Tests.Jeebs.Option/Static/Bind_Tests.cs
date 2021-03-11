// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using NSubstitute;
using Xunit;

namespace Jeebs.OptionStatic_Tests
{
	public class Bind_Tests
	{
		[Fact]
		public void Exception_Thrown_Calls_Handler()
		{
			// Arrange
			var option = Option.Wrap(F.Rnd.Str);
			var handler = Substitute.For<Option.Handler>();
			var exception = new Exception();

			// Act
			var result = Option.Bind<int>(() => throw exception, handler);

			// Assert
			Assert.IsType<None<int>>(result);
			handler.Received().Invoke(exception);
		}

		[Fact]
		public void Runs_Bind_Function()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Option.Wrap(value);
			var bind = Substitute.For<Func<Option<string>>>();

			// Act
			Option.Bind(bind, null);

			// Assert
			bind.Received(1).Invoke();
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}