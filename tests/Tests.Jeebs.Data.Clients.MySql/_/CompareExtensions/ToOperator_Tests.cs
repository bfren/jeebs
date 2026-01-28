// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Exceptions;

namespace Jeebs.Data.Clients.MySql.CompareExtensions_Tests;

public class ToOperator_Tests
{
	[Fact]
	public void Equal_Returns_Equals_Sign()
	{
		// Arrange
		var value = Compare.Equal;

		// Act
		var result = value.ToOperator();

		// Assert
		Assert.Equal("=", result);
	}

	[Fact]
	public void NotEqual_Returns_NotEquals_Sign()
	{
		// Arrange
		var value = Compare.NotEqual;

		// Act
		var result = value.ToOperator();

		// Assert
		Assert.Equal("!=", result);
	}

	[Fact]
	public void Like_Returns_Like()
	{
		// Arrange
		var value = Compare.Like;

		// Act
		var result = value.ToOperator();

		// Assert
		Assert.Equal("LIKE", result);
	}

	[Fact]
	public void LessThan_Returns_LessThan_Sign()
	{
		// Arrange
		var value = Compare.LessThan;

		// Act
		var result = value.ToOperator();

		// Assert
		Assert.Equal("<", result);
	}

	[Fact]
	public void LessThanOrEqual_Returns_LessThanOrEqual_Sign()
	{
		// Arrange
		var value = Compare.LessThanOrEqual;

		// Act
		var result = value.ToOperator();

		// Assert
		Assert.Equal("<=", result);
	}

	[Fact]
	public void MoreThan_Returns_MoreThan_Sign()
	{
		// Arrange
		var value = Compare.MoreThan;

		// Act
		var result = value.ToOperator();

		// Assert
		Assert.Equal(">", result);
	}

	[Fact]
	public void MoreThanOrEqual_Returns_MoreThanOrEqual_Sign()
	{
		// Arrange
		var value = Compare.MoreThanOrEqual;

		// Act
		var result = value.ToOperator();

		// Assert
		Assert.Equal(">=", result);
	}

	[Fact]
	public void In_Returns_In()
	{
		// Arrange
		var value = Compare.In;

		// Act
		var result = value.ToOperator();

		// Assert
		Assert.Equal("IN", result);
	}

	[Fact]
	public void NotIn_Returns_Not_In()
	{
		// Arrange
		var value = Compare.NotIn;

		// Act
		var result = value.ToOperator();

		// Assert
		Assert.Equal("NOT IN", result);
	}

	[Theory]
	[InlineData(10)]
	[InlineData(20)]
	[InlineData(30)]
	public void Other_Throws_UnrecognisedSearchOperatorException(int input)
	{
		// Arrange
		var value = (Compare)input;

		// Act
		var result = Record.Exception(() => value.ToOperator());

		// Assert
		Assert.IsType<UnrecognisedSearchOperatorException>(result);
	}
}
