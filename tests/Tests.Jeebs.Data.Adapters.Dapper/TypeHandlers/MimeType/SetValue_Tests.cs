// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;

namespace Jeebs.Data.Adapters.Dapper.TypeHandlers.MimeTypeTypeHandler_Tests;

public class SetValue_Tests
{
	[Theory]
	[MemberData(nameof(Parse_Tests.Returns_Correct_MimeType_Data), MemberType = typeof(Parse_Tests))]
	public void Sets_Value_To_MimeType_Name(string expected, MimeType input)
	{
		// Arrange
		var handler = new MimeTypeTypeHandler();
		var parameter = Substitute.For<IDbDataParameter>();

		// Act
		handler.SetValue(parameter, input);

		// Assert
		parameter.Received().Value = expected;
	}
}
