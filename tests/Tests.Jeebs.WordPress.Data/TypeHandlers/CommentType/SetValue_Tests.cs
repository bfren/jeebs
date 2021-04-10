// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using Jeebs.WordPress.Data.Enums;
using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.TypeHandlers.CommentTypeTypeHandler_Tests
{
	public class SetValue_Tests
	{
		public static TheoryData<CommentType, string> Sets_Value_To_CommentType_Name_Data =>
			new()
			{
				{ CommentType.Blank, string.Empty },
				{ CommentType.Pingback, "pingback" }
			};

		[Theory]
		[MemberData(nameof(Sets_Value_To_CommentType_Name_Data))]
		public void Sets_Value_To_CommentType_Name(CommentType input, string expected)
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
