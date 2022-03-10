﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.Config.Db;
using Jeebs.Config.WordPress;
using Jeebs.Data;
using Jeebs.Data.Map;
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

		var connection = Substitute.For<IDbConnection>();
		_ = client.Connect(Arg.Any<string>()).Returns(connection);

		var dbConfig = Substitute.For<IOptions<DbConfig>>();

		var name = Rnd.Str;
		_ = dbConfig.Value.Returns(new DbConfig
		{
			Default = name,
			Connections = new()
			{
				{ name, new DbConnectionConfig() }
			}
		});

		var log = Substitute.For<ILog>();
		var logForQuery = Substitute.For<ILog<IWpDbQuery>>();
		_ = log.ForContext<IWpDbQuery>().Returns(logForQuery);

		var wpConfig = Substitute.For<IOptions<WpConfig>>();
		_ = wpConfig.Value.Returns(new WpConfig
		{
			TablePrefix = Rnd.Str
		});

		// Act
		_ = new WpDb<Comment, CommentMeta, Link, Opt, Post, PostMeta, Term, TermMeta,
			TermRelationship, TermTaxonomy, User, UserMeta>(client, dbConfig, wpConfig, log);

		// Assert
		var s0 = Mapper.Instance.GetTableMapFor<Comment>().AssertSome();
		_ = Assert.IsAssignableFrom<CommentsTable>(s0.Table);

		var s1 = Mapper.Instance.GetTableMapFor<CommentMeta>().AssertSome();
		_ = Assert.IsAssignableFrom<CommentsMetaTable>(s1.Table);

		var s2 = Mapper.Instance.GetTableMapFor<Link>().AssertSome();
		_ = Assert.IsAssignableFrom<LinksTable>(s2.Table);

		var s3 = Mapper.Instance.GetTableMapFor<Opt>().AssertSome();
		_ = Assert.IsAssignableFrom<OptionsTable>(s3.Table);

		var s4 = Mapper.Instance.GetTableMapFor<Post>().AssertSome();
		_ = Assert.IsAssignableFrom<PostsTable>(s4.Table);

		var s5 = Mapper.Instance.GetTableMapFor<PostMeta>().AssertSome();
		_ = Assert.IsAssignableFrom<PostsMetaTable>(s5.Table);

		var s6 = Mapper.Instance.GetTableMapFor<Term>().AssertSome();
		_ = Assert.IsAssignableFrom<TermsTable>(s6.Table);

		var s7 = Mapper.Instance.GetTableMapFor<TermMeta>().AssertSome();
		_ = Assert.IsAssignableFrom<TermsMetaTable>(s7.Table);

		var s8 = Mapper.Instance.GetTableMapFor<TermRelationship>().AssertSome();
		_ = Assert.IsAssignableFrom<TermRelationshipsTable>(s8.Table);

		var s9 = Mapper.Instance.GetTableMapFor<TermTaxonomy>().AssertSome();
		_ = Assert.IsAssignableFrom<TermTaxonomiesTable>(s9.Table);

		var s10 = Mapper.Instance.GetTableMapFor<User>().AssertSome();
		_ = Assert.IsAssignableFrom<UsersTable>(s10.Table);

		var s11 = Mapper.Instance.GetTableMapFor<UserMeta>().AssertSome();
		_ = Assert.IsAssignableFrom<UsersMetaTable>(s11.Table);
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
