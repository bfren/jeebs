// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.MappedColumn_Tests;

public class ToString_Tests
{
	[Fact]
	public void Returns_Name()
	{
		// Arrange
		var name = F.Rnd.Str;
		var prop = Substitute.For<PropertyInfo>();
		prop.Name.Returns(F.Rnd.Str);
		var column = new MappedColumn(F.Rnd.Str, name, prop);

		// Act
		var result = column.ToString();

		// Assert
		Assert.Equal(name, result);
	}
}
