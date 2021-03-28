// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using NSubstitute;
using Xunit;
using static Jeebs.WordPress.Data.Querying.QueryPartsBuilder_Tests.QueryPartsBuilder;

namespace Jeebs.WordPress.Data.Querying.QueryPartsBuilder_Tests
{
	public class AddJoin_Tests
	{
		[Fact]
		public void Null_List_Creates_New_List()
		{
			// Arrange
			var (builder, _) = GetQueryPartsBuilder();

			// Act
			var result = builder.AddJoin(null, F.Rnd.Str, F.Rnd.Str, (F.Rnd.Str, F.Rnd.Str), false);

			// Assert
			Assert.NotNull(result);
		}

		[Theory]
		[InlineData(true)]
		[InlineData(false)]
		public void Calls_List_Add(bool escape)
		{
			// Arrange
			var (builder, _) = GetQueryPartsBuilder();
			var list = Substitute.For<IList<(string table, string on, string equals)>>();

			// Act
			builder.AddJoin(list, F.Rnd.Str, F.Rnd.Str, (F.Rnd.Str, F.Rnd.Str), escape);

			// Assert
			list.Received().Add(Arg.Any<(string, string, string)>());
		}

		[Fact]
		public void Escape_True_Calls_Adapter_Escape_Methods()
		{
			var list = Substitute.For<IList<(string table, string on, string equals)>>();
			EscapeTrueCallsAdapterEscapeMethods(
				(builder, table, on, equals, escape) => builder.AddJoin(list, table, on, equals, escape)
			);
		}

		[Fact]
		public void Escape_False_Calls_Adapter_Join()
		{
			var list = Substitute.For<IList<(string table, string on, string equals)>>();
			EscapeFalseCallsAdapterJoin(
				(builder, table, on, equals, escape) => builder.AddJoin(list, table, on, equals, escape)
			);
		}
	}
}
