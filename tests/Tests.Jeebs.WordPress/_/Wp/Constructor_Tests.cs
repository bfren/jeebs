// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Config;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Wp_Tests
{
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

		public sealed record TestWpConfig : WpConfig;

		public static class E
		{
			public sealed record Comment : WpCommentEntity { }
			public sealed record CommentMeta : WpCommentMetaEntity { }
			public sealed record Link : WpLinkEntity { }
			public sealed record Option : WpOptionEntity { }
			public sealed record Post : WpPostEntity { }
			public sealed record PostMeta : WpPostMetaEntity { }
			public sealed record Term : WpTermEntity { }
			public sealed record TermMeta : WpTermMetaEntity { }
			public sealed record TermRelationship : WpTermRelationshipEntity { }
			public sealed record TermTaxonomy : WpTermTaxonomyEntity { }
			public sealed record User : WpUserEntity { }
			public sealed record UserMeta : WpUserMetaEntity { }
		}
	}
}
