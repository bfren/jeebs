// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Wp_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Sets_Properties()
	{
		// Arrange
		var dbConfig = Substitute.For<IOptions<DbConfig>>();

		var name = F.Rnd.Str;
		dbConfig.Value.Returns(new DbConfig
		{
			Default = name,
			Connections = new()
			{
				{ name, new DbConnectionConfig() }
			}
		});

		var log = Substitute.For<ILog>();
		var logForQuery = Substitute.For<ILog<IWpDbQuery>>();
		log.ForContext<IWpDbQuery>().Returns(logForQuery);

		var wpConfig = Substitute.For<IOptions<TestWpConfig>>();
		var wpConfigValue = new TestWpConfig
		{
			TablePrefix = F.Rnd.Str
		};
		wpConfig.Value.Returns(wpConfigValue);

		// Act
		var result = new TestWp(dbConfig, wpConfig, log);

		// Assert
		Assert.Same(wpConfigValue, result.Config);
		Assert.IsType<WpDb<E.Comment, E.CommentMeta, E.Link, E.Option, E.Post, E.PostMeta, E.Term, E.TermMeta, E.TermRelationship, E.TermTaxonomy, E.User, E.UserMeta>>(result.Db);
	}

	public sealed class TestWp : Wp<TestWpConfig, E.Comment, E.CommentMeta, E.Link, E.Option, E.Post, E.PostMeta, E.Term, E.TermMeta, E.TermRelationship, E.TermTaxonomy, E.User, E.UserMeta>
	{
		public TestWp(IOptions<DbConfig> dbConfig, IOptions<TestWpConfig> wpConfig, ILog logForDb) : base(dbConfig, wpConfig, logForDb) { }

		public override void RegisterCustomPostTypes() { }

		public override void RegisterCustomTaxonomies() { }
	}

	public sealed record class TestWpConfig : WpConfig;

	public static class E
	{
		public sealed record class Comment : WpCommentEntity { }
		public sealed record class CommentMeta : WpCommentMetaEntity { }
		public sealed record class Link : WpLinkEntity { }
		public sealed record class Option : WpOptionEntity { }
		public sealed record class Post : WpPostEntity { }
		public sealed record class PostMeta : WpPostMetaEntity { }
		public sealed record class Term : WpTermEntity { }
		public sealed record class TermMeta : WpTermMetaEntity { }
		public sealed record class TermRelationship : WpTermRelationshipEntity { }
		public sealed record class TermTaxonomy : WpTermTaxonomyEntity { }
		public sealed record class User : WpUserEntity { }
		public sealed record class UserMeta : WpUserMetaEntity { }
	}
}
