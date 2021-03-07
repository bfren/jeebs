// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
