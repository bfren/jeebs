// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using NSubstitute;
using Xunit;

namespace JeebsF.OptionExtensions_Tests
{
	public class AuditAsync_Tests
	{
		[Fact]
		public async Task Runs_Audit_And_Returns_Original_Option()
		{
			// Arrange
			var option = OptionF.True;
			var task = Task.FromResult(option);
			var audit = Substitute.For<Func<Option<bool>, Task>>();

			// Act
			var r0 = await OptionExtensions.DoAuditAsync(task, audit);
			var r1 = await task.AuditAsync(v => { audit(v); });
			var r2 = await task.AuditAsync(audit);

			// Assert
			await audit.Received(3).Invoke(option);
			Assert.Same(option, r0);
			Assert.Same(option, r1);
			Assert.Same(option, r2);
		}

		[Fact]
		public async Task Catches_Exception_And_Returns_Original_Option()
		{
			// Arrange
			var option = OptionF.Return(JeebsF.Rnd.Int);
			var task = Task.FromResult(option);

			void actionThrow(Option<int> _) => throw new Exception();
			Task funcThrow(Option<int> _) => throw new Exception();

			// Act
			var r0 = await OptionExtensions.DoAuditAsync(task, _ => throw new Exception());
			var r1 = await task.AuditAsync(actionThrow);
			var r2 = await task.AuditAsync(funcThrow);

			// Assert
			Assert.Same(option, r0);
			Assert.Same(option, r1);
			Assert.Same(option, r2);
		}

		public class FakeOption : Option<int> { }

		public class TestMsg : IMsg { }
	}
}
