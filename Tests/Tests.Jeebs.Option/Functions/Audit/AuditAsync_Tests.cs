// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests
{
	public class AuditAsync_Tests
	{
		[Fact]
		public async Task Runs_Audit_And_Returns_Original_Option()
		{
			// Arrange
			var some = True;
			var none = None<bool>(true);
			var audit = Substitute.For<Func<Option<bool>, Task>>();

			// Act
			var r0 = await AuditAsync(some, audit);
			var r1 = await AuditAsync(none, audit);
			var r2 = await AuditAsync(some.AsTask, audit);
			var r3 = await AuditAsync(none.AsTask, audit);

			// Assert
			await audit.Received(2).Invoke(some);
			await audit.Received(2).Invoke(none);
			Assert.Same(some, r0);
			Assert.Same(none, r1);
			Assert.Same(some, r2);
			Assert.Same(none, r3);
		}

		[Fact]
		public async Task Catches_Exception_And_Returns_Original_Option()
		{
			// Arrange
			var some = True;
			var none = None<bool>(true);
			static Task throwException(Option<bool> _) => throw new Exception();

			// Act
			var r0 = await AuditAsync(some, throwException);
			var r1 = await AuditAsync(none, throwException);
			var r2 = await AuditAsync(some.AsTask, throwException);
			var r3 = await AuditAsync(none.AsTask, throwException);

			// Assert
			Assert.Same(some, r0);
			Assert.Same(none, r1);
			Assert.Same(some, r2);
			Assert.Same(none, r3);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}
