// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.MappedColumn_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Properties_Set()
	{
		// Arrange
		var table = new TableName(F.Rnd.Str);
		var name = F.Rnd.Str;
		var alias = F.Rnd.Str;
		var prop = Substitute.For<PropertyInfo>();
		_ = prop.Name.Returns(alias);

		// Act
		var result = new MappedColumn(table, name, prop);

		// Assert
		Assert.Equal(table, result.TblName);
		Assert.Equal(name, result.ColName);
		Assert.Equal(alias, result.ColAlias);
		Assert.Equal(prop, result.PropertyInfo);
	}
}
