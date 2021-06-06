// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
