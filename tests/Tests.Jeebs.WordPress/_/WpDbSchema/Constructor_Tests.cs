// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.WpDbSchema_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Creates_Tables_With_TablePrefix()
	{
		// Arrange
		var prefix = Rnd.Str;

		// Act
		var result = new WpDbSchema(prefix);

		// Assert
		Assert.StartsWith(prefix, result.Comments.ToString());
		Assert.StartsWith(prefix, result.CommentsMeta.ToString());
		Assert.StartsWith(prefix, result.Links.ToString());
		Assert.StartsWith(prefix, result.Options.ToString());
		Assert.StartsWith(prefix, result.Posts.ToString());
		Assert.StartsWith(prefix, result.PostsMeta.ToString());
		Assert.StartsWith(prefix, result.Terms.ToString());
		Assert.StartsWith(prefix, result.TermsMeta.ToString());
		Assert.StartsWith(prefix, result.TermRelationships.ToString());
		Assert.StartsWith(prefix, result.TermTaxonomies.ToString());
		Assert.StartsWith(prefix, result.Users.ToString());
		Assert.StartsWith(prefix, result.UsersMeta.ToString());
	}
}
