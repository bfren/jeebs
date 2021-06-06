// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
			var table = F.Rnd.Str;
			var name = F.Rnd.Str;
			var alias = F.Rnd.Str;
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
