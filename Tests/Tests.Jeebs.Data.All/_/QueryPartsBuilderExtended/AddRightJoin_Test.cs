using Xunit;
using static Jeebs.Data.QueryPartsBuilderExtended_Tests.QueryPartsBuilderExtended;

namespace Jeebs.Data.QueryPartsBuilderExtended_Tests
{
	public class AddRightJoin_Test
	{
		[Fact]
		public void Calls_AddRightJoin_With_Escape_True()
		{
			CreatesNewListEscapesAndAddsJoin(
				p => p.RightJoin,
				(b, f, o, e) => b.AddRightJoin(f, o, e)
			);
		}
	}
}
