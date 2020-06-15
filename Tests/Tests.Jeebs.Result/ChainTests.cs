using System;
using System.Threading.Tasks;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result
{
	public class ChainTests
	{
		[Fact]
		public void R_Chain_Returns_Ok()
		{
			// Arrange

			// Act
			var r = R.Chain;

			// Assert
			Assert.IsType<Ok>(r);
		}

		[Fact]
		public async Task R_ChainAsync_Returns_Ok()
		{
			// Arrange

			// Act
			var r = await R.ChainAsync;

			// Assert
			Assert.IsType<Ok>(r);
		}
	}
}
