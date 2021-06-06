// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
			var none = Create.EmptyNone<int>();

			// Act
			var result = none.AsTask;

			// Assert
			Assert.IsType<Task<Option<int>>>(result);
		}
	}
}
