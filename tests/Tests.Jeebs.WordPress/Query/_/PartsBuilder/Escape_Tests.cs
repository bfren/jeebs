// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.WordPress.Query.PartsBuilder_Tests;

public class Escape_Tests : PartsBuilder_Tests
{
	[Fact]
	public void Escape_Table_Calls_Client_Escape()
	{
		// Arrange
		var (builder, v) = Setup();

		// Act
		builder.EscapeTest(v.Table);

		// Assert
		v.Client.Received().Escape(v.Table);
	}

	[Fact]
	public void Escape_Column_Calls_Client_Escape()
	{
		// Arrange
		var (options, v) = Setup();

		// Act
		options.EscapeTest(v.Table, t => t.Id);

		// Assert
		v.Client.Received().EscapeWithTable(Arg.Any<IColumn>());
	}
}
