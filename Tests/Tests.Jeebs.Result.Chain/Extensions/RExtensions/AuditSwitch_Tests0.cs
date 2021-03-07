// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using NSubstitute;
using Xunit;

namespace Jeebs.RExtensions_Tests
{
	public partial class AuditSwitch_Tests : IAudit_AuditSwitch
	{
		[Fact]
		public void No_Input_Returns_Original_Result()
		{
			// Arrange
			var chain = Chain.Create();

			// Act
			var next = chain.AuditSwitch();

			// Assert
			Assert.Same(chain, next);
		}

		[Fact]
		public void Unknown_Implementation_Throws_Exception()
		{
			// Arrange
			var r = Substitute.For<IR<int>>();
			static void audit(IOk _) => throw new Exception();

			// Act
			void a() => r.AuditSwitch(isOk: audit);

			// Assert
			Assert.Throws<Jx.Result.UnknownImplementationException>(a);
		}
	}
}
