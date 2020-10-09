using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.Link_Tests
{
	public partial class UnwrapSingle_Tests
	{
		[Fact]
		public void Other_Input_Same_Type_Returns_Input()
		{
			// Arrange
			var value = F.Rnd.Integer;
			var chain = Chain.CreateV(value);

			// Act
			var result = chain.Link().UnwrapSingle<int>();

			// Assert
			var okV = Assert.IsAssignableFrom<IOkV<int>>(result);
			Assert.Equal(value, okV.Value);
		}
	}
}
