using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.UnwrapTests
{
	public partial class UnwrapTests
	{
		[Fact]
		public void Other_Input_Same_Type_Returns_Input()
		{
			// Arrange
			const int value = 18;
			var chain = Chain.CreateV(value);

			// Act
			var result = chain.Link().Unwrap<int>();

			// Assert
			var okV = Assert.IsAssignableFrom<IOkV<int>>(result);
			Assert.Equal(value, okV.Value);
		}
	}
}
