// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.WordPress.Data.Enums;
using NSubstitute;
using Xunit;
using static Jeebs.WordPress.Data.AttachmentCustomField;

namespace Jeebs.WordPress.Data.CustomFields.AttachmentCustomField_Tests
{
	public class GetAttachment_Tests
	{
		[Fact]
		public void Calls_Db_Query_PostsAsync_With_Id_And_Correct_Values()
		{
			// Arrange
			var query = Substitute.For<IWpDbQuery>();
			var schema = Substitute.For<IWpDbSchema>();
			var db = Substitute.For<IWpDb>();
			db.Query.Returns(query);
			var id = F.Rnd.Lng;

			// Act
			_ = GetAttachment(db, new(id));

			// Assert
			db.Query.Received().PostsAsync<Attachment>(Arg.Is<Query.GetPostsOptions>(opt =>
				opt(new(schema)).Id! == id
				&& opt(new(schema)).Type == PostType.Attachment
				&& opt(new(schema)).Status == PostStatus.Inherit
				&& opt(new(schema)).Maximum == 1
			));
		}
	}
}
