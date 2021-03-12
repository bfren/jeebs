// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using NSubstitute;
using Xunit;

namespace JeebsF.Option_Tests
{
	public class Audit_Tests
	{
		[Fact]
		public void Runs_Audit_And_Returns_Original_Option()
		{
			// Arrange
			var option = OptionF.True;
			var audit = Substitute.For<Action<Option<bool>>>();

			// Act
			var r0 = option.DoAudit(audit);
			var r1 = option.Audit(audit);

			// Assert
			audit.Received(2).Invoke(option);
			Assert.Same(option, r0);
			Assert.Same(option, r1);
		}

		[Fact]
		public void Catches_Exception_And_Returns_Original_Option()
		{
			// Arrange
			var option = OptionF.Return(JeebsF.Rnd.Int);
			var exception = new Exception();

			// Act
			var r0 = option.DoAudit(_ => throw exception);
			var r1 = option.Audit(_ => throw exception);

			// Assert
			Assert.Same(option, r0);
			Assert.Same(option, r1);
		}

		public class FakeOption : Option<int> { }

		public class TestMsg : IMsg { }
	}
}
