// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Config.TwitterConfig_Tests
{
	public class IsValid_Tests
	{
		[Theory]
		[InlineData(null, "secret", "key", "secret")]
		[InlineData("", "secret", "key", "secret")]
		[InlineData(" ", "secret", "key", "secret")]
		[InlineData("token", null, "key", "secret")]
		[InlineData("token", "", "key", "secret")]
		[InlineData("token", " ", "key", "secret")]
		[InlineData("token", "secret", null, "secret")]
		[InlineData("token", "secret", "", "secret")]
		[InlineData("token", "secret", " ", "secret")]
		[InlineData("token", "secret", "key", null)]
		[InlineData("token", "secret", "key", "")]
		[InlineData("token", "secret", "key", " ")]
		public void Returns_False(string userToken, string userSecret, string consumerKey, string consumerSecret)
		{
			// Arrange
			var config = new TwitterConfig
			{
				UserAccessToken = userToken,
				UserAccessSecret = userSecret,
				ConsumerKey = consumerKey,
				ConsumerSecret = consumerSecret
			};

			// Act
			var result = config.IsValid;

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void Returns_True()
		{
			// Arrange
			var config = new TwitterConfig
			{
				UserAccessToken = JeebsF.Rnd.Str,
				UserAccessSecret = JeebsF.Rnd.Str,
				ConsumerKey = JeebsF.Rnd.Str,
				ConsumerSecret = JeebsF.Rnd.Str
			};

			// Act
			var result = config.IsValid;

			// Assert
			Assert.True(result);
		}
	}
}
