using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.Link_Tests.WithState
{
	public partial class UnwrapSingle_Tests
	{
		[Fact]
		public void Other_Input_Same_Type_Returns_Input()
		{
			// Arrange
			const int value = 18;
			const int state = 7;
			var chain = Chain.CreateV(value, state);

			// Act
			var result = chain.Link().UnwrapSingle<int>();

			// Assert
			var okV = Assert.IsAssignableFrom<IOkV<int, int>>(result);
			Assert.Equal(value, okV.Value);
			Assert.Equal(state, okV.State);
		}
	}
}
