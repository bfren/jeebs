// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using NSubstitute;
using Xunit;
using static Jeebs.WordPress.Data.QueryPartsBuilderExtended_Tests.QueryPartsBuilderExtended;

namespace Jeebs.WordPress.Data.QueryPartsBuilderExtended_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_Properties_And_Escapes_Table_Name()
		{
			// Arrange
			var adapter = Substitute.For<IAdapter>();
			var table = new FooTable();

			// Act
			var result = new Builder(adapter, table);

			// Assert
			Assert.Equal(adapter, result.Adapter);
			adapter.Received().EscapeTable(table);
		}
	}
}
