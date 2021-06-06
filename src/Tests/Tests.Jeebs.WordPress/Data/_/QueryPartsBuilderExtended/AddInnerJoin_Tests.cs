// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;
using static Jeebs.WordPress.Data.QueryPartsBuilderExtended_Tests.QueryPartsBuilderExtended;

namespace Jeebs.WordPress.Data.QueryPartsBuilderExtended_Tests
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
