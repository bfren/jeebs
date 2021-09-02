// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Data;
using Xunit;

namespace Tests.Jeebs.WordPress.Data.WpDbSchema_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Creates_Tables_With_TablePrefix()
		{
			// Arrange
			var prefix = F.Rnd.Str;

			// Act
			var result = new WpDbSchema(prefix);

			// Assert
			Assert.StartsWith(prefix, result.Comment.GetName());
			Assert.StartsWith(prefix, result.CommentMeta.GetName());
			Assert.StartsWith(prefix, result.Link.GetName());
			Assert.StartsWith(prefix, result.Option.GetName());
			Assert.StartsWith(prefix, result.Post.GetName());
			Assert.StartsWith(prefix, result.PostMeta.GetName());
			Assert.StartsWith(prefix, result.Term.GetName());
			Assert.StartsWith(prefix, result.TermMeta.GetName());
			Assert.StartsWith(prefix, result.TermRelationship.GetName());
			Assert.StartsWith(prefix, result.TermTaxonomy.GetName());
			Assert.StartsWith(prefix, result.User.GetName());
			Assert.StartsWith(prefix, result.UserMeta.GetName());
		}
	}
}
