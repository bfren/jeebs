// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Data;
using Xunit;

namespace Tests.Jeebs.WordPress.Data.WpDbSchema_Tests;

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
		Assert.StartsWith(prefix, result.Comment.ToString());
		Assert.StartsWith(prefix, result.CommentMeta.ToString());
		Assert.StartsWith(prefix, result.Link.ToString());
		Assert.StartsWith(prefix, result.Option.ToString());
		Assert.StartsWith(prefix, result.Post.ToString());
		Assert.StartsWith(prefix, result.PostMeta.ToString());
		Assert.StartsWith(prefix, result.Term.ToString());
		Assert.StartsWith(prefix, result.TermMeta.ToString());
		Assert.StartsWith(prefix, result.TermRelationship.ToString());
		Assert.StartsWith(prefix, result.TermTaxonomy.ToString());
		Assert.StartsWith(prefix, result.User.ToString());
		Assert.StartsWith(prefix, result.UserMeta.ToString());
	}
}
