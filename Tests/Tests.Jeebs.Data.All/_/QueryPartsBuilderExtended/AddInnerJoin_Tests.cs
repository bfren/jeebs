// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;
using static Jeebs.Data.QueryPartsBuilderExtended_Tests.QueryPartsBuilderExtended;

namespace Jeebs.Data.QueryPartsBuilderExtended_Tests
{
	public class AddInnerJoin_Tests
	{
		[Fact]
		public void Calls_AddInnerJoin_With_Escape_True()
		{
			CreatesNewListEscapesAndAddsJoin(
				p => p.InnerJoin,
				(b, f, o, e) => b.AddInnerJoin(f, o, e)
			);
		}
	}
}
