// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryPartsBuilderWithEntity_Tests
{
	public class Table_Tests : QueryPartsBuilderWithEntity_Tests
	{
		[Fact]
		public void Returns_Map_Table()
		{
			// Arrange
			var (builder, v) = Setup();
			var table = Substitute.For<ITable>();
			v.Map.Table.Returns(table);

			// Act
			var result = builder.Table;

			// Assert
			Assert.Same(table, result);
		}
	}
}
