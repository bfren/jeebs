// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Reflection;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.MappedColumn_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Properties_Set()
		{
			// Arrange
			var table = JeebsF.Rnd.Str;
			var name = JeebsF.Rnd.Str;
			var alias = JeebsF.Rnd.Str;
			var prop = Substitute.For<PropertyInfo>();
			prop.Name.Returns(alias);

			// Act
			var result = new MappedColumn(table, name, prop);

			// Assert
			Assert.Equal(table, result.Table);
			Assert.Equal(name, result.Name);
			Assert.Equal(alias, result.Alias);
			Assert.Equal(prop, result.Property);
		}
	}
}
