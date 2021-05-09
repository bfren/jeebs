// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.WordPress.Data.Query_Tests.Options_Tests
{
	public class Constructor_Tests : Options_Tests
	{
		[Fact]
		public void Sets_Properties()
		{
			// Arrange
			var (_, v) = Setup();

			// Act
			var result = new TestOptions(v.DbClient, v.WpDb, v.Table);

			// Assert
			Assert.Same(v.DbClient, result.ClientTest);
			Assert.Same(v.WpDb, result.DbTest);
			Assert.Same(v.Schema, result.TTest);
			Assert.Same(v.Table, result.TableTest);
		}
	}
}
