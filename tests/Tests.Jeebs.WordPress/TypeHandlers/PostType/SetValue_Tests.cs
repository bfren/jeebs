// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.WordPress.Enums;
using Base = Jeebs.WordPress.Enums.PostType_Tests.Parse_Tests;

namespace Jeebs.WordPress.TypeHandlers.PostTypeTypeHandler_Tests;

public class SetValue_Tests
{
	[Theory]
	[MemberData(nameof(Base.Returns_Correct_PostType_Data), MemberType = typeof(Base))]
	public void Sets_Value_To_PostType_Name(string expected, PostType input)
	{
		// Arrange
		var handler = new PostTypeTypeHandler();
		var parameter = Substitute.For<IDbDataParameter>();

		// Act
		handler.SetValue(parameter, input);

		// Assert
		parameter.Received().Value = expected;
	}
}
