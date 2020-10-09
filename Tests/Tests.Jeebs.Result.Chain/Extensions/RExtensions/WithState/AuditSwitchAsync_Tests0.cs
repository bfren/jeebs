using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.RExtensions_Tests.WithState
{
	public partial class AuditSwitchAsync_Tests : IAudit_AuditSwitch_WithState
	{
		[Fact]
		public void No_Input_Returns_Original_Result()
		{
			// Arrange
			var state = F.Rand.Integer;
			var chain = Chain.Create(state);

			// Act
			var next = chain.AuditSwitchAsync().Await();

			// Assert
			Assert.Same(chain, next);
		}

		[Fact]
		public void Unknown_Implementation_Throws_Exception()
		{
			// Arrange
			var r = Substitute.For<IR<int>>();
			static async Task audit(IOk _) => throw new Exception();

			// Act
			void a() => r.AuditSwitchAsync(isOk: audit).Await();

			// Assert
			Assert.Throws<Jx.Result.UnknownImplementationException>(a);
		}
	}
}
