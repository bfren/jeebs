// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
