using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.AuditTests
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
