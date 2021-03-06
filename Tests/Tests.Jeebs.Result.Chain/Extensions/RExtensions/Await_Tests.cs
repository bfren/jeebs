using System.Threading.Tasks;
using Xunit;

namespace Jeebs.RExtensions_Tests
{
	public class Await_Tests
	{
		[Fact]
		public void Awaits_Result()
		{
			// Arrange
			var chain = Chain.Create();
			static async Task<IR<bool>> f(IOk r) => await Task.FromResult(r.OkV(true));

			// Act
			var result = chain.Link().MapAsync(f).Await();

			// Assert
			var okV = Assert.IsAssignableFrom<IOkV<bool>>(result);
			Assert.True(okV.Value);
		}
	}
}
