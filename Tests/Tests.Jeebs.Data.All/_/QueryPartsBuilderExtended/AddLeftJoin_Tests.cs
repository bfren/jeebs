using Xunit;
using static Jeebs.Data.QueryPartsBuilderExtended_Tests.QueryPartsBuilderExtended;

namespace Jeebs.Data.QueryPartsBuilderExtended_Tests
{
	public class AddLeftJoin_Tests
	{
		[Fact]
		public void Calls_AddLeftJoin_With_Escape_True()
		{
			CreatesNewListEscapesAndAddsJoin(
				p => p.LeftJoin,
				(b, f, o, e) => b.AddLeftJoin(f, o, e)
			);
		}
	}
}
