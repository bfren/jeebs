using System;
using System.Threading.Tasks;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result
{
	public class ChainTests_WithState : IChainTests
	{
		[Fact]
		public void R_Chain_Returns_Ok()
		{
			// Arrange
			const int state = 18;

			// Act
			var r = R.Chain.AddState(state);

			// Assert
			Assert.IsAssignableFrom<IOk<bool, int>>(r);
			Assert.Equal(state, r.State);
		}

		[Fact]
		public async Task R_ChainAsync_Returns_Ok()
		{
			// Arrange
			const int state = 18;

			// Act
			var r = R.Chain.AddState(state);

			// Assert
			Assert.IsAssignableFrom<IOk<bool, int>>(r);
			Assert.Equal(state, r.State);
		}
	}
}
