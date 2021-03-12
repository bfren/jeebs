// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Reflection;
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
			var table = JeebsF.Rnd.Str;
			var name = JeebsF.Rnd.Str;
			var prop = Substitute.For<PropertyInfo>();
			prop.Name.Returns(JeebsF.Rnd.Str);
			var column = new MappedColumn(table, name, prop);

			// Act
			var result = column.ToString();

			// Assert
			Assert.Equal(name, result);
		}
	}
}
