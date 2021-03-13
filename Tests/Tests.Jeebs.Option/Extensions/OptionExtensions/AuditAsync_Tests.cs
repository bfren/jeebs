// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs.OptionExtensions_Tests
{
	public class AuditAsync_Tests
	{
		[Fact]
		public async Task Runs_Audit_And_Returns_Original_Option()
		{
			// Arrange
			var option = True;
			var task = option.AsTask;
			var audit = Substitute.For<Func<Option<bool>, Task>>();

			// Act
			var r0 = await task.AuditAsync(v => { audit(v); });
			var r1 = await task.AuditAsync(audit);

			// Assert
			await audit.Received(3).Invoke(option);
			Assert.Same(option, r0);
			Assert.Same(option, r1);
		}

		[Fact]
		public async Task Catches_Exception_And_Returns_Original_Option()
		{
			// Arrange
			var option = Return(F.Rnd.Int);
			var task = option.AsTask;

			void actionThrow(Option<int> _) => throw new Exception();
			Task funcThrow(Option<int> _) => throw new Exception();

			// Act
			var r0 = await task.AuditAsync(actionThrow);
			var r1 = await task.AuditAsync(funcThrow);

			// Assert
			Assert.Same(option, r0);
			Assert.Same(option, r1);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}
