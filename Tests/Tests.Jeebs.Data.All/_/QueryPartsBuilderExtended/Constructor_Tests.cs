// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using NSubstitute;
using Xunit;
using static Jeebs.Data.QueryPartsBuilderExtended_Tests.QueryPartsBuilderExtended;

namespace Jeebs.Data.QueryPartsBuilderExtended_Tests
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
