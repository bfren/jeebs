// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.PostsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.PostsPartsBuilder_Tests
{
	public class AddWhereStatus_Tests : QueryPartsBuilder_Tests<Query.PostsPartsBuilder, WpPostId>
	{
		protected override Query.PostsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
			GetBuilder(extract);

		[Fact]
		public void Adds_Status_To_Where()
		{
			// Arrange
			var (builder, v) = Setup();
			var status = new PostStatus(F.Rnd.Str);

			// Act
			var result = builder.AddWhereStatus(v.Parts, status);

			// Assert
			AssertWhere(v.Parts, result, Post.Status, Compare.Equal, status);
		}
	}
}
