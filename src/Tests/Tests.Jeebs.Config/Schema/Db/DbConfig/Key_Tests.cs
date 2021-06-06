// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
