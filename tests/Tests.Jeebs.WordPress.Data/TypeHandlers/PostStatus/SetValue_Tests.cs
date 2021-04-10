// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using Jeebs.WordPress.Data.Enums;
using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.TypeHandlers.PostStatusTypeHandler_Tests
{
	public class SetValue_Tests
	{
		public static TheoryData<PostStatus, string> Sets_Value_To_PostStatus_Name_Data =>
			new()
			{
				{ PostStatus.AutoDraft, "auto-draft" },
				{ PostStatus.Draft, "draft" },
				{ PostStatus.Inherit, "inherit" },
				{ PostStatus.Pending, "pending" },
				{ PostStatus.Publish, "publish" }
			};

		[Theory]
		[MemberData(nameof(Sets_Value_To_PostStatus_Name_Data))]
		public void Sets_Value_To_CommentType_Name(PostStatus input, string expected)
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
}
