// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;
using static Jeebs.WordPress.Data.QueryPartsBuilderExtended_Tests.QueryPartsBuilderExtended;

namespace Jeebs.WordPress.Data.QueryPartsBuilderExtended_Tests
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
