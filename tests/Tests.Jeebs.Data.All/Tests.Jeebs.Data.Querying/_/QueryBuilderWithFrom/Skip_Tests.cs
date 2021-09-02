// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryBuilderWithFrom_Tests;

public class Skip_Tests
{
	[Fact]
	public void Sets_Skip()
	{
		// Arrange
		var table = Substitute.For<ITable>();
		var builder = new QueryBuilderWithFrom(table);
		var value = F.Rnd.Ulng;

		// Act
		var result = builder.Skip(value);

		// Assert
		var from = Assert.IsType<QueryBuilderWithFrom>(result);
		Assert.Equal(value, from.Parts.Skip);
	}
}
