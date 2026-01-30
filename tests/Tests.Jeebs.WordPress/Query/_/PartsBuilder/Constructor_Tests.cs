// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Common;

namespace Jeebs.WordPress.Query.PartsBuilder_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Sets_Client()
	{
		// Arrange
		var extract = Substitute.For<IExtract>();
		var client = Substitute.For<IDbClient>();
		var schema = Substitute.For<IWpDbSchema>();

		// Act
		var result = new TestPartsBuilder(extract, client, schema);

		// Assert
		Assert.Same(client, result.ClientTest);
	}

	[Fact]
	public void Sets_Schema()
	{
		// Arrange
		var extract = Substitute.For<IExtract>();
		var client = Substitute.For<IDbClient>();
		var schema = Substitute.For<IWpDbSchema>();

		// Act
		var result = new TestPartsBuilder(extract, client, schema);

		// Assert
		Assert.Same(schema, result.TTest);
	}
}
