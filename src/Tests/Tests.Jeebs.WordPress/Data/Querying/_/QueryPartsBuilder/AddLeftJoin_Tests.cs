// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using Xunit;
using static Jeebs.WordPress.Data.Querying.QueryPartsBuilder_Tests.QueryPartsBuilder;

namespace Jeebs.WordPress.Data.Querying.QueryPartsBuilder_Tests
{
	public class AddLeftJoin_Tests
	{
		[Fact]
		public void Creates_New_List_And_Adds_Join()
		{
			// Arrange
			var (builder, _) = GetQueryPartsBuilder();

			// Act
			Assert.Null(builder.Parts.LeftJoin);
			builder.AddLeftJoin(F.Rnd.Str, F.Rnd.Str, (F.Rnd.Str, F.Rnd.Str), false);

			// Assert
			var join = Assert.IsType<List<(string, string, string)>>(builder.Parts.LeftJoin);
			Assert.Single(join);
		}

		[Fact]
		public void Escape_True_Calls_Adapter_Escape_Methods()
		{
			EscapeTrueCallsAdapterEscapeMethods(
				(builder, table, on, equals, escape) => builder.AddLeftJoin(table, on, equals, escape)
			);
		}

		[Fact]
		public void Escape_False_Calls_Adapter_Join()
		{
			EscapeFalseCallsAdapterJoin(
				(builder, table, on, equals, escape) => builder.AddLeftJoin(table, on, equals, escape)
			);
		}
	}
}
