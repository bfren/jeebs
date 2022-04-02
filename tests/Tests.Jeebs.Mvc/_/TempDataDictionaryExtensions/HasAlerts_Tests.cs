// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Jeebs.Mvc.TempDataDictionaryExtensions_Tests;

public class HasAlerts_Tests
{
	[Fact]
	public void Returns_False_With_No_Alert()
	{
		// Arrange
		var td = Substitute.For<ITempDataDictionary>();

		// Act
		var result = td.HasAlerts();

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Returns_True_With_Alert()
	{
		// Arrange
		var td = Substitute.For<ITempDataDictionary>();
		td.Peek(TempDataDictionaryExtensions.AlertsKey).Returns(true);

		// Act
		var result = td.HasAlerts();

		// Assert
		Assert.True(result);
	}
}
