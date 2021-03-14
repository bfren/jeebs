// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using NSubstitute;
using Xunit;
using static Jeebs.Data.QueryPartsBuilderExtended_Tests.QueryPartsBuilderExtended;

namespace Jeebs.Data.QueryPartsBuilderExtended_Tests
{
	public class EscapeShorthand_Tests
	{
		[Fact]
		public void With_Table_Calls_EscapeTable()
		{
			// Arrange
			var (builder, adapter, table) = GetQueryPartsBuilder();

			// Act
			builder.__(table);

			// Assert
			adapter.Received().EscapeTable(table);
		}

		[Fact]
		public void With_Table_And_Func_Calls_EscapeAndJoin()
		{
			// Arrange
			var (builder, adapter, table) = GetQueryPartsBuilder();
			var columnName = F.Rnd.Str;
			var column = Substitute.For<Func<FooTable, string>>();
			column(table)
				.Returns(columnName);

			// Act
			builder.__(table, column);

			// Assert
			column.Received().Invoke(table);
			adapter.Received().EscapeAndJoin(table, columnName);
		}
	}
}
