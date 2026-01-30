// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Base = Jeebs.MimeType_Tests.Parse_Tests;

namespace Jeebs.Data.Common.TypeHandlers.MimeTypeTypeHandler_Tests;

public class SetValue_Tests
{
	[Theory]
	[MemberData(nameof(Base.Returns_Correct_MimeType_Data), MemberType = typeof(Base))]
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
