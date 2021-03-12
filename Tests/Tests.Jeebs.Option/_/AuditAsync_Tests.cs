// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs.Option_Tests
{
	public class AuditAsync_Tests
	{
		[Fact]
		public async Task Runs_Audit_And_Returns_Original_Option()
		{
			// Arrange
			var option = True;
			var audit = Substitute.For<Func<Option<bool>, Task>>();

			// Act
			var r0 = await option.DoAuditAsync(audit);
			var r1 = await option.AuditAsync(audit);

			// Assert
			await audit.Received(2).Invoke(option);
			Assert.Same(option, r0);
			Assert.Same(option, r1);
		}

		[Fact]
		public async Task Catches_Exception_And_Returns_Original_Option()
		{
			// Arrange
			var option = Return(F.Rnd.Int);

			// Act
			var r0 = await option.DoAuditAsync(_ => throw new Exception());
			var r1 = await option.AuditAsync(_ => throw new Exception());

			// Assert
			Assert.Same(option, r0);
			Assert.Same(option, r1);
		}

		public class FakeOption : Option<int> { }

		public class TestMsg : IMsg { }
	}
}
