// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Repository.FluentQuery_Tests;

public class Skip_Tests : FluentQuery_Tests
{
	[Fact]
	public void Value_Greater_Than_Zero__Updates_Skip()
	{
		// Arrange
		var (query, _) = Setup();
		var value = Rnd.UInt64;

		// Act
		var result = query.Skip(value);

		// Assert
		var fluent = Assert.IsType<FluentQuery<TestEntity, TestId>>(result);
		Assert.Equal(value, fluent.Parts.Skip);
	}

	[Fact]
	public void Value_Zero__Returns_Original_Query()
	{
		// Arrange
		var (query, _) = Setup();

		// Act
		var result = query.Skip(0);

		// Assert
		var fluent = Assert.IsType<FluentQuery<TestEntity, TestId>>(result);
		Assert.Same(query, fluent);
	}
}
