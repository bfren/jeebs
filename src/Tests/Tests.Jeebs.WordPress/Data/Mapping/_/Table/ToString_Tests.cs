// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
