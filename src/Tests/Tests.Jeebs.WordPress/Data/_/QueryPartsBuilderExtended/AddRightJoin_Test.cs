// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;
using static Jeebs.WordPress.Data.QueryPartsBuilderExtended_Tests.QueryPartsBuilderExtended;

namespace Jeebs.WordPress.Data.QueryPartsBuilderExtended_Tests
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
