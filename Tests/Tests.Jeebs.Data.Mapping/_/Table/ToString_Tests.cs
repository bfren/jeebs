using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.Table_Tests
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
