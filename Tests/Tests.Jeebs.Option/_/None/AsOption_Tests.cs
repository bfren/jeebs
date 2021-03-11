// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Xunit;

namespace Jeebs.OptionNone_Tests
{
	public class AsOption_Tests
	{
		[Fact]
		public void Returns_None_As_Generic_Option()
		{
			// Arrange
			var none = Option.None<int>(true);

			// Act
			var result = Task.FromResult(none.AsOption);

			// Assert
			Assert.IsType<Task<Option<int>>>(result);
		}
	}
}
