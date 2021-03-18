// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs.Option_Tests
{
	public class Audit_Tests
	{
		[Fact]
		public void Runs_Audit_And_Returns_Original_Option()
		{
			// Arrange
			var option = True;
			var audit = Substitute.For<Action<Option<bool>>>();

			// Act
			var result = option.Audit(audit);

			// Assert
			audit.Received().Invoke(option);
			Assert.Same(option, result);
		}

		[Fact]
		public void Catches_Exception_And_Returns_Original_Option()
		{
			// Arrange
			var option = Return(F.Rnd.Int);
			var exception = new Exception();

			// Act
			var result = option.Audit(_ => throw exception);

			// Assert
			Assert.Same(option, result);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}
