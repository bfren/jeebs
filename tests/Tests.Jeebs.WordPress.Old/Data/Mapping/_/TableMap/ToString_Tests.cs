// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.Mapping.TableMap_Tests
{
	public class ToString_Tests
	{
		[Fact]
		public void Returns_Name()
		{
			// Arrange
			var name = F.Rnd.Str;
			var map = new TableMap(name, Substitute.For<IMappedColumnList>(), GetColumnNames_Tests.Get().column);

			// Act
			var result = map.ToString();

			// Assert
			Assert.Equal(name, result);
		}
	}
}
