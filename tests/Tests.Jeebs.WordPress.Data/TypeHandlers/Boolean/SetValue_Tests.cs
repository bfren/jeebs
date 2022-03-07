// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Data;
using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.TypeHandlers.BooleanTypeHandler_Tests;

public class SetValue_Tests
{
	public static TheoryData<string, bool, string> Valid_Column_Sets_Value_Data =>
		new()
		{
			{ "comment_approved", true, "1" },
			{ "comment_approved", false, "0" },
			{ "autoload", true, "yes" },
			{ "autoload", false, "no" },
			{ "comment_subscribe", true, "Y" },
			{ "comment_subscribe", false, "N" },
			{ "link_visible", true, "Y" },
			{ "link_visible", false, "N" }
		};

	[Theory]
	[MemberData(nameof(Valid_Column_Sets_Value_Data))]
	public void Valid_Column_Sets_Value(string column, bool value, string expected)
	{
		// Arrange
		var handler = new BooleanTypeHandler();
		var parameter = Substitute.For<IDbDataParameter>();
		_ = parameter.SourceColumn.Returns(column);

		// Act
		handler.SetValue(parameter, value);

		// Assert
		parameter.Received().Value = expected;
	}

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	public void Invalid_Column_Throws_InvalidOperationException(bool input)
	{
		// Arrange
		var handler = new BooleanTypeHandler();
		var column = F.Rnd.Str;
		var parameter = Substitute.For<IDbDataParameter>();
		_ = parameter.SourceColumn.Returns(column);

		// Act
		var action = void () => handler.SetValue(parameter, input);

		// Assert
		_ = Assert.Throws<InvalidOperationException>(action);
	}
}
