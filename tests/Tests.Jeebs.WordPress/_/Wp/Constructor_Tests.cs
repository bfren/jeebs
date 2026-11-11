// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data.Common;
using Jeebs.Config.Db;
using Jeebs.Config.WordPress;
using Jeebs.Logging;
using Jeebs.WordPress.Entities;
using Microsoft.Extensions.Options;

namespace Jeebs.WordPress.Wp_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Each_Config_Type_Registers_Its_Custom_Types()
	{
		// Arrange
		var client = Substitute.For<Data.Common.IDbClient>();
		client.GetConnection(Arg.Any<string>()).Returns(Substitute.ForPartsOf<DbConnection>());

		var name = Rnd.Str;
		var dbConfig = Substitute.For<IOptions<DbConfig>>();
		dbConfig.Value.Returns(new DbConfig
		{
			Default = name,
			Connections = { { name, new DbConnectionConfig() } }
		});

		var log = Substitute.For<ILog>();

		// Act
		var one = new WpOne(client, dbConfig, Options.Create(new ConfigOne { Db = name }), log);
		var two = new WpTwo(client, dbConfig, Options.Create(new ConfigTwo { Db = name }), log);

		// Assert
		Assert.Equal(1, one.PostTypesRegistered);
		Assert.Equal(1, one.TaxonomiesRegistered);
		Assert.Equal(1, two.PostTypesRegistered);
		Assert.Equal(1, two.TaxonomiesRegistered);
	}

	public sealed record class ConfigOne : WpConfig;
	public sealed record class ConfigTwo : WpConfig;

	public abstract class TestWp<TConfig>(Data.Common.IDbClient client, IOptions<DbConfig> dbConfig, IOptions<TConfig> wpConfig, ILog log) :
		Wp<TConfig, Comment, CommentMeta, Link, Opt, Post, PostMeta, Term, TermMeta, TermRelationship, TermTaxonomy, User, UserMeta>(
			client, dbConfig, wpConfig, log
		)
		where TConfig : WpConfig
	{
		public int PostTypesRegistered { get; private set; }

		public int TaxonomiesRegistered { get; private set; }

		public override void RegisterCustomPostTypes() =>
			PostTypesRegistered++;

		public override void RegisterCustomTaxonomies() =>
			TaxonomiesRegistered++;
	}

	public sealed class WpOne(Data.Common.IDbClient client, IOptions<DbConfig> dbConfig, IOptions<ConfigOne> wpConfig, ILog log) :
		TestWp<ConfigOne>(client, dbConfig, wpConfig, log);

	public sealed class WpTwo(Data.Common.IDbClient client, IOptions<DbConfig> dbConfig, IOptions<ConfigTwo> wpConfig, ILog log) :
		TestWp<ConfigTwo>(client, dbConfig, wpConfig, log);

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
