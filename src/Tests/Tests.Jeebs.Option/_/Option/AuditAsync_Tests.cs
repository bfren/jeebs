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
			var result = await option.AuditAsync(audit);

			// Assert
			await audit.Received().Invoke(option);
			Assert.Same(option, result);
		}

		[Fact]
		public async Task Catches_Exception_And_Returns_Original_Option()
		{
			// Arrange
			var option = Return(F.Rnd.Int);

			// Act
			var result = await option.AuditAsync(_ => throw new Exception());

			// Assert
			Assert.Same(option, result);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}
