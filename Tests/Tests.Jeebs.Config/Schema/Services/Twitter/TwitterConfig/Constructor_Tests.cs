// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Config.TwitterConfig_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Implements_ServiceConfig()
		{
			// Arrange
			var config = new TwitterConfig();

			// Act

			// Assert
			Assert.IsAssignableFrom<IServiceConfig>(config);
		}
	}
}
