// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests
{
	public class Audit_Tests
	{
		[Fact]
		public void Runs_Audit_And_Returns_Original_Option()
		{
			// Arrange
			var some = True;
			var none = None<bool>(true);
			var audit = Substitute.For<Action<Option<bool>>>();

			// Act
			var r0 = Audit(some, audit);
			var r1 = Audit(none, audit);

			// Assert
			audit.Received().Invoke(some);
			audit.Received().Invoke(none);
			Assert.Same(some, r0);
			Assert.Same(none, r1);
		}

		[Fact]
		public void Catches_Exception_And_Returns_Original_Option()
		{
			// Arrange
			var some = True;
			var none = None<bool>(true);
			static void throwException(Option<bool> _) => throw new Exception();

			// Act
			var r0 = Audit(some, throwException);
			var r1 = Audit(none, throwException);

			// Assert
			Assert.Same(some, r0);
			Assert.Same(none, r1);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}
