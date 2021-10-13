// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.DbClient_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Sets_Properties()
	{
		// Arrange
		var mapper = Substitute.For<IMapper>();

		// Act
		var result = Substitute.ForPartsOf<DbClient>(mapper);

		// Assert
		Assert.Same(mapper, result.MapperTest);
	}
}
