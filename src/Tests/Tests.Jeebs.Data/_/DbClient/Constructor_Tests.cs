// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using NSubstitute;
using Xunit;

namespace Jeebs.Data.DbClient_Tests
{
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
}
