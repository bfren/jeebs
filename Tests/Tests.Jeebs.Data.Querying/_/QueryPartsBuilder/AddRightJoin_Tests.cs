using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;
using static Jeebs.Data.Querying.QueryPartsBuilder_Tests.QueryPartsBuilder;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public class AddRightJoin_Tests
	{


		[Fact]
		public void Escape_True_Calls_Adapter_Escape_Methods()
		{
			EscapeTrueCallsAdapterEscapeMethods(
				(builder, table, on, equals, escape) => builder.AddRightJoin(table, on, equals, escape)
			);
		}

		[Fact]
		public void Escape_False_Calls_Adapter_Join()
		{
			EscapeFalseCallsAdapterJoin(
				(builder, table, on, equals, escape) => builder.AddRightJoin(table, on, equals, escape)
			);
		}
	}
}
