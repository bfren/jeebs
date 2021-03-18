// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Config.DbConfig_Tests
{
	public class Key_Tests
	{
		[Fact]
		public void Returns_Db_Key()
		{
			// Arrange

			// Act
			const string result = DbConfig.Key;

			// Assert
			Assert.Equal(JeebsConfig.Key + ":db", result);
		}
	}
}
