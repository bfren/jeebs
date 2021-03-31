// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.Mapping.Table_Tests
{
	public class ToString_Tests
	{
		[Fact]
		public void Returns_Name()
		{
			// Arrange
			var name = F.Rnd.Str;
			var table = Substitute.For<Table>(name);

			// Act
			var result = table.ToString();

			// Assert
			Assert.Equal(name, result);
		}
	}
}
