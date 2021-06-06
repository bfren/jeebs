// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryPartsBuilderWithEntity_Tests
{
	public class IdColumn_Tests : QueryPartsBuilderWithEntity_Tests
	{
		[Fact]
		public void Returns_Map_IdColumn()
		{
			// Arrange
			var (builder, v) = Setup();
			var idColumn = Substitute.For<IMappedColumn>();
			v.Map.IdColumn.Returns(idColumn);

			// Act
			var result = builder.IdColumn;

			// Assert
			Assert.Same(idColumn, result);
		}
	}
}
