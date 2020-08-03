using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs.LinkTests;
using Xunit;

namespace Jeebs.LinkTests.WithState.Async
{
	public partial class WrapAsync_Tests : ILink_Wrap_WithState
	{
		[Fact]
		public void Value_Input_When_IOk_Wraps_Value()
		{
			// Not necessary for Async
		}

		[Fact]
		public void Value_Input_When_IError_Returns_IError()
		{
			// Not necessary for Async
		}
	}
}
