// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.WordPress.Enums;
using Base = Jeebs.WordPress.Enums.PostStatus_Tests.Parse_Tests;

namespace Jeebs.WordPress.TypeHandlers.PostStatusTypeHandler_Tests;

public class SetValue_Tests
{
	[Theory]
	[MemberData(nameof(Base.Returns_Correct_PostStatus_Data), MemberType = typeof(Base))]
	public void Sets_Value_To_CommentType_Name(string expected, PostStatus input)
	{
		// Arrange
		var handler = new PostStatusTypeHandler();
		var parameter = Substitute.For<IDbDataParameter>();

		// Act
		handler.SetValue(parameter, input);

		// Assert
		parameter.Received().Value = expected;
	}
}
