using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.RExtensions_Tests
{
	public partial class AuditSwitchAsync_Tests : IAudit_AuditSwitch
	{
		[Fact]
		public void No_Input_Returns_Original_Result()
		{
			// Arrange
			var chain = Chain.Create();

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
