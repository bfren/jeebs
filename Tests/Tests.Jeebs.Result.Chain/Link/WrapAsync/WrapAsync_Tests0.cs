using Xunit;

namespace Jeebs.Link_Tests
{
	public partial class WrapAsync_Tests : ILink_Wrap
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
