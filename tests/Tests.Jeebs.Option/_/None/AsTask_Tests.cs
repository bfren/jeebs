// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Xunit;

namespace Jeebs.None_Tests
{
	public class AsTask_Tests
	{
		[Fact]
		public void Returns_None_As_Generic_Option()
		{
			// Arrange
			var none = Create.None<int>();

			// Act
			var result = none.AsTask;

			// Assert
			Assert.IsType<Task<Option<int>>>(result);
		}
	}
}
