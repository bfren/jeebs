// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data.Common;
using Jeebs.Config.Db;
using Jeebs.Config.WordPress;
using Jeebs.Data;
using Jeebs.Data.Common;
using Jeebs.Logging;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Tables;
using Microsoft.Extensions.Options;

namespace Jeebs.WordPress.WpDb_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Maps_Entities_To_Correct_Tables()
	{
		// Arrange
		var client = Substitute.For<IDbClient>();

		var connection = Substitute.ForPartsOf<DbConnection>();
		client.GetConnection(Arg.Any<string>()).Returns(connection);

		var dbConfig = Substitute.For<IOptions<DbConfig>>();

		var name = Rnd.Str;
		dbConfig.Value.Returns(new DbConfig
		{
			Default = name,
			Connections = { { name, new DbConnectionConfig() } }
		});

		var log = Substitute.For<ILog>();
		var logForQuery = Substitute.For<ILog<IWpDbQuery>>();
		log.ForContext<IWpDbQuery>().Returns(logForQuery);

		var wpConfig = Substitute.For<IOptions<WpConfig>>();
		wpConfig.Value.Returns(new WpConfig
		{
			Db = name,
			TablePrefix = Rnd.Str
		});

		// Act
		_ = new WpDb<Comment, CommentMeta, Link, Opt, Post, PostMeta, Term, TermMeta,
			TermRelationship, TermTaxonomy, User, UserMeta>(client, dbConfig, wpConfig, log);

		// Assert
		var s0 = EntityMapper.Instance.GetTableMapFor<Comment>().AssertOk();
		Assert.IsType<CommentsTable>(s0.Table, exactMatch: false);

		var s1 = EntityMapper.Instance.GetTableMapFor<CommentMeta>().AssertOk();
		Assert.IsType<CommentsMetaTable>(s1.Table);

		var s2 = EntityMapper.Instance.GetTableMapFor<Link>().AssertOk();
		Assert.IsType<LinksTable>(s2.Table);

		var s3 = EntityMapper.Instance.GetTableMapFor<Opt>().AssertOk();
		Assert.IsType<OptionsTable>(s3.Table);

		var s4 = EntityMapper.Instance.GetTableMapFor<Post>().AssertOk();
		Assert.IsType<PostsTable>(s4.Table);

		var s5 = EntityMapper.Instance.GetTableMapFor<PostMeta>().AssertOk();
		Assert.IsType<PostsMetaTable>(s5.Table);

		var s6 = EntityMapper.Instance.GetTableMapFor<Term>().AssertOk();
		Assert.IsType<TermsTable>(s6.Table);

		var s7 = EntityMapper.Instance.GetTableMapFor<TermMeta>().AssertOk();
		Assert.IsType<TermsMetaTable>(s7.Table);

		var s8 = EntityMapper.Instance.GetTableMapFor<TermRelationship>().AssertOk();
		Assert.IsType<TermRelationshipsTable>(s8.Table);

		var s9 = EntityMapper.Instance.GetTableMapFor<TermTaxonomy>().AssertOk();
		Assert.IsType<TermTaxonomiesTable>(s9.Table);

		var s10 = EntityMapper.Instance.GetTableMapFor<User>().AssertOk();
		Assert.IsType<UsersTable>(s10.Table);

		var s11 = EntityMapper.Instance.GetTableMapFor<UserMeta>().AssertOk();
		Assert.IsType<UsersMetaTable>(s11.Table);
	}

	public sealed record class Comment : WpCommentEntity { }
	public sealed record class CommentMeta : WpCommentMetaEntity { }
	public sealed record class Link : WpLinkEntity { }
	public sealed record class Opt : WpOptionEntity { }
	public sealed record class Post : WpPostEntity { }
	public sealed record class PostMeta : WpPostMetaEntity { }
	public sealed record class Term : WpTermEntity { }
	public sealed record class TermMeta : WpTermMetaEntity { }
	public sealed record class TermRelationship : WpTermRelationshipEntity { }
	public sealed record class TermTaxonomy : WpTermTaxonomyEntity { }
	public sealed record class User : WpUserEntity { }
	public sealed record class UserMeta : WpUserMetaEntity { }
}
