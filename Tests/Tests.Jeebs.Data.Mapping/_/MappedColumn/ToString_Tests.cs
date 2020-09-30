using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.MappedColumn_Tests
{
	public class ToString_Tests
	{
		[Fact]
		public void Returns_Name()
		{
			// Arrange
			var name = F.StringF.Random(6);
			var prop = Substitute.For<PropertyInfo>();
			prop.Name.Returns(F.StringF.Random(6));
			var column = new MappedColumn(Arg.Any<string>(), name, prop);

			// Act
			var result = column.ToString();

			// Assert
			Assert.Equal(name, result);
		}
	}
}
