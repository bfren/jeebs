﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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