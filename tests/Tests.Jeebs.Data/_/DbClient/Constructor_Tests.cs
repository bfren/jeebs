// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
