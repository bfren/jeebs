// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using Jeebs.WordPress.Data.Enums;
using NSubstitute;
using Xunit;
using Base = Jeebs.WordPress.Data.Enums.PostType_Tests.Parse_Tests;

namespace Jeebs.WordPress.Data.TypeHandlers.PostTypeTypeHandler_Tests
{
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
}
