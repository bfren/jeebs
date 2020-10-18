using System;
using System.Collections.Generic;
using System.Text;
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
				UserAccessToken = F.Rnd.Str,
				UserAccessSecret = F.Rnd.Str,
				ConsumerKey = F.Rnd.Str,
				ConsumerSecret = F.Rnd.Str
			};

			// Act
			var result = config.IsValid;

			// Assert
			Assert.True(result);
		}
	}
}
