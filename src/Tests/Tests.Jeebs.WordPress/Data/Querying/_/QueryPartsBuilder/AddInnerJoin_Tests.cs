// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;
using static Jeebs.WordPress.Data.Querying.QueryPartsBuilder_Tests.QueryPartsBuilder;

namespace Jeebs.WordPress.Data.Querying.QueryPartsBuilder_Tests
{
	public class AddInnerJoin_Tests
	{
		[Fact]
		public void Creates_New_List_And_Adds_Join()
		{
			CreatesNewListAndAddsJoin(
				p => p.InnerJoin,
				(builder, table, on, equals) => builder.AddInnerJoin(table, on, equals)
			);
		}

		[Fact]
		public void Escape_True_Calls_Adapter_Escape_Methods()
		{
			EscapeTrueCallsAdapterEscapeMethods(
				(builder, table, on, equals, escape) => builder.AddInnerJoin(table, on, equals, escape)
			);
		}

		[Fact]
		public void Escape_False_Calls_Adapter_Join()
		{
			EscapeFalseCallsAdapterJoin(
				(builder, table, on, equals, escape) => builder.AddInnerJoin(table, on, equals, escape)
			);
		}
	}
}
