// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;
using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.Query_Tests.PartsBuilder_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_Client()
		{
			// Arrange
			var client = Substitute.For<IDbClient>();
			var schema = Substitute.For<IWpDbSchema>();

			// Act
			var result = new TestPartsBuilder(client, schema);

			// Assert
			Assert.Same(client, result.ClientTest);
		}

		[Fact]
		public void Sets_Schema()
		{
			// Arrange
			var client = Substitute.For<IDbClient>();
			var schema = Substitute.For<IWpDbSchema>();

			// Act
			var result = new TestPartsBuilder(client, schema);

			// Assert
			Assert.Same(schema, result.TTest);
		}
	}
}
