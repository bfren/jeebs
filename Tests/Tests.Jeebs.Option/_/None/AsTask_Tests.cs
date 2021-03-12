// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Xunit;
using static F.OptionF;

namespace Jeebs.Option.None_Tests
{
	public class AsTask_Tests
	{
		[Fact]
		public void Returns_None_As_Generic_Option()
		{
			// Arrange
			var none = None<int>(true);

			// Act
			var result = none.AsTask;

			// Assert
			Assert.IsType<Task<Option<int>>>(result);
		}
	}
}
