// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using NSubstitute;
using Xunit;
using Base = Jeebs.MimeType_Tests.Parse_Tests;

namespace Jeebs.Data.TypeHandlers.MimeTypeTypeHandler_Tests
{
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
}
