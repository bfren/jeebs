using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
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
