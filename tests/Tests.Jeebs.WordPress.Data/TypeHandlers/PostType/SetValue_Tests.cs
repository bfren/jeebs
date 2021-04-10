// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using Jeebs.WordPress.Data.Enums;
using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.TypeHandlers.PostTypeTypeHandler_Tests
{
	public class SetValue_Tests
	{
		public static TheoryData<PostType, string> Sets_Value_To_PostType_Name_Data =>
			new()
			{
				{ PostType.AdvancedCustomField, "acf" },
				{ PostType.Attachment, "attachment" },
				{ PostType.MenuItem, "nav_menu_item" },
				{ PostType.Page, "page" },
				{ PostType.Post, "post" },
				{ PostType.Revision, "revision" }
			};

		[Theory]
		[MemberData(nameof(Sets_Value_To_PostType_Name_Data))]
		public void Sets_Value_To_PostType_Name(PostType input, string expected)
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
