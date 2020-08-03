using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.LinkTests
{
	public partial class Wrap_Tests : ILink_Wrap
	{
		[Fact]
		public void Value_Input_When_IOk_Wraps_Value()
		{
			// Arrange
			const int value = 18;
			var r = Chain.Create();

			// Act
			var next = r.Link().Wrap(value);

			// Assert
			var okV = Assert.IsAssignableFrom<IOkV<int>>(next);
			Assert.Equal(value, okV.Value);
		}

		[Fact]
		public void Value_Input_When_IError_Returns_IError()
		{
			// Arrange
			const int value = 18;
			var r = Chain.Create().Error();

			// Act
			var next = r.Link().Wrap(value);

			// Assert
			Assert.IsAssignableFrom<IError<int>>(next);
		}
	}
}
