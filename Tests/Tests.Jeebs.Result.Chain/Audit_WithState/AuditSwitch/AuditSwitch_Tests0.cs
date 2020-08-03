using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.AuditTests.WithState
{
	public partial class AuditSwitch_Tests : IAudit_AuditSwitch_WithState
	{
		[Fact]
		public void No_Input_Returns_Original_Result()
		{
			// Arrange
			const int state = 7;
			var chain = Chain.Create(state);

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
