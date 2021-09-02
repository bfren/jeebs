// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.WordPress.Data.Enums;
using NSubstitute;
using Xunit;
using Base = Jeebs.WordPress.Data.Enums.CommentType_Tests.Parse_Tests;

namespace Jeebs.WordPress.Data.TypeHandlers.CommentTypeTypeHandler_Tests
{
	public class SetValue_Tests
	{
		[Theory]
		[MemberData(nameof(Base.Returns_Correct_CommentType_Data), MemberType = typeof(Base))]
		public void Sets_Value_To_CommentType_Name(string expected, CommentType input)
		{
			// Arrange
			var handler = new CommentTypeTypeHandler();
			var parameter = Substitute.For<IDbDataParameter>();

			// Act
			handler.SetValue(parameter, input);

			// Assert
			parameter.Received().Value = expected;
		}
	}
}
