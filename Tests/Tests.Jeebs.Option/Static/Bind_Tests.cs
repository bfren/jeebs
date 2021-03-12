// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using NSubstitute;
using Xunit;

namespace JeebsF.OptionStatic_Tests
{
	public class Bind_Tests
	{
		[Fact]
		public void Exception_Thrown_Calls_Handler()
		{
			// Arrange
			var option = OptionF.Return(JeebsF.Rnd.Str);
			var handler = Substitute.For<OptionF.Handler>();
			var exception = new Exception();

			// Act
			var result = OptionF.Bind<int>(() => throw exception, handler);

			// Assert
			Assert.IsType<None<int>>(result);
			handler.Received().Invoke(exception);
		}

		[Fact]
		public void Runs_Bind_Function()
		{
			// Arrange
			var value = JeebsF.Rnd.Int;
			var option = OptionF.Return(value);
			var bind = Substitute.For<Func<Option<string>>>();

			// Act
			OptionF.Bind(bind, null);

			// Assert
			bind.Received(1).Invoke();
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}