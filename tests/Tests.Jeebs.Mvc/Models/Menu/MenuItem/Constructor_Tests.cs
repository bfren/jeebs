// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Mvc.Models.MenuItem_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Action_Is_Index()
		{
			// Arrange

			// Act
			var result = new MenuItem();

			// Assert
			Assert.Equal("Index", result.Action);
		}

		[Fact]
		public void IsLink_Is_True()
		{
			// Arrange

			// Act
			var result = new MenuItem();

			// Assert
			Assert.True(result.IsLink);
		}
	}
}
