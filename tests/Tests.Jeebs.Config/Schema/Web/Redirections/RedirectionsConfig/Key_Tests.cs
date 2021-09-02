// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Config.RedirectionsConfig_Tests
{
	public class Key_Tests
	{
		[Fact]
		public void Returns_Redirections_Key()
		{
			// Arrange

			// Act
			const string result = RedirectionsConfig.Key;

			// Assert
			Assert.Equal(JeebsConfig.Key + ":web:redirections", result);
		}
	}
}
