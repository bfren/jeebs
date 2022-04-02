// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;

namespace Jeebs.Data.Clients.SqlServer.SortOrderExtensions_Tests;

public class ToOperator_Tests
{
	[Fact]
	public void Ascending_Returns_ASC()
	{
		// Arrange
		var value = SortOrder.Ascending;

		// Act
		var result = value.ToOperator();

		// Assert
		Assert.Equal("ASC", result);
	}

	[Fact]
	public void Descending_Returns_DESC()
	{
		// Arrange
		var value = SortOrder.Descending;

		// Act
		var result = value.ToOperator();

		// Assert
		Assert.Equal("DESC", result);
	}

	[Fact]
	public void Other_Returns_ASC()
	{
		// Arrange
		var value = (SortOrder)Rnd.NumberF.GetInt32(min: 2, max: 1000);

		// Act
		var result = value.ToOperator();

		// Assert
		Assert.Equal("ASC", result);
	}
}
