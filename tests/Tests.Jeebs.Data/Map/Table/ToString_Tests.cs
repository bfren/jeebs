// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Map.Table_Tests;

public class ToString_Tests
{
	[Fact]
	public void Returns_Name()
	{
		// Arrange
		var name = Rnd.Str;
		var schema = Rnd.Str;
		var t0 = Substitute.ForPartsOf<Table>(new TableName(schema, name));
		var t1 = Substitute.ForPartsOf<Table>(name);
		var t2 = Substitute.ForPartsOf<Table>(schema, name);

		// Act
		var r0 = t0.ToString();
		var r1 = t1.ToString();
		var r2 = t2.ToString();

		// Assert
		Assert.Equal($"{schema}{TableName.SchemaSeparator}{name}", r0);
		Assert.Equal($"{name}", r1);
		Assert.Equal($"{schema}{TableName.SchemaSeparator}{name}", r2);
	}
}
