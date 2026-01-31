// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Wrap.Exceptions;

namespace Jeebs.Data.Query.QueryPartsBuilderWithEntity_Tests;

public class Map_Tests : QueryPartsBuilderWithEntity_Tests
{
	[Fact]
	public void Calls_Mapper_GetTableMapFor()
	{
		// Arrange
		var (builder, v) = Setup();

		// Act
		_ = builder.Map.Value;

		// Assert
		v.Mapper.Received().GetTableMapFor<TestEntity>();
	}

	[Fact]
	public void Entity_Not_Mapped_Throws_MsgException_With_Msg()
	{
		// Arrange
		var (builder, v) = Setup();
		v.Mapper.GetTableMapFor<TestEntity>().Returns(FailGen.Create());

		// Act
		var result = Record.Exception(() => builder.Map.Value);

		// Assert
		Assert.IsType<FailureException>(result);
	}
}
