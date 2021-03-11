// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Option_Tests
{
	public class DoAuditAsync_Tests : IDisposable
	{
		[Fact]
		public async Task Runs_Audit_And_Returns_Original_Option()
		{
			// Arrange
			var option = Option.True;
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
		public async Task Handles_Exception_And_Returns_Original_Option()
		{
			// Arrange
			var option = Option.Wrap(F.Rnd.Int);
			var exception = new Exception();
			var handler = Substitute.For<Action<Exception>>();
			Option.LogAuditExceptions = handler;

			// Act
			var r0 = await option.DoAuditAsync(_ => throw exception);
			var r1 = await option.AuditAsync(_ => throw exception);

			// Assert
			handler.Received(2).Invoke(exception);
			Assert.Same(option, r0);
			Assert.Same(option, r1);
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
			Option.LogAuditExceptions = null;
		}

		public class FakeOption : Option<int> { }

		public class TestMsg : IMsg { }
	}
}
