// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
