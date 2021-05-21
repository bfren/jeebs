// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using Jeebs;
using Jeebs.Config;
using Jeebs.Data;
using Jeebs.Data.Mapping;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Tables;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Tests.Jeebs.WordPress.Data.WpDb_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Maps_Entities_To_Correct_Tables()
		{
			// Arrange
			var client = Substitute.For<IDbClient>();

			var connection = Substitute.For<IDbConnection>();
			client.Connect(Arg.Any<string>()).Returns(connection);

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

			var wpConfig = Substitute.For<IOptions<WpConfig>>();
			wpConfig.Value.Returns(new WpConfig
			{
				TablePrefix = F.Rnd.Str
			});

			// Act
			var _ = new WpDb<Comment, CommentMeta, Link, Option, Post, PostMeta, Term, TermMeta,
				TermRelationship, TermTaxonomy, User, UserMeta>(client, dbConfig, wpConfig, log);

			// Assert
			var s0 = Mapper.Instance.GetTableMapFor<Comment>().AssertSome();
			Assert.IsAssignableFrom<CommentTable>(s0.Table);

			var s1 = Mapper.Instance.GetTableMapFor<CommentMeta>().AssertSome();
			Assert.IsAssignableFrom<CommentMetaTable>(s1.Table);

			var s2 = Mapper.Instance.GetTableMapFor<Link>().AssertSome();
			Assert.IsAssignableFrom<LinkTable>(s2.Table);

			var s3 = Mapper.Instance.GetTableMapFor<Option>().AssertSome();
			Assert.IsAssignableFrom<OptionTable>(s3.Table);

			var s4 = Mapper.Instance.GetTableMapFor<Post>().AssertSome();
			Assert.IsAssignableFrom<PostTable>(s4.Table);

			var s5 = Mapper.Instance.GetTableMapFor<PostMeta>().AssertSome();
			Assert.IsAssignableFrom<PostMetaTable>(s5.Table);

			var s6 = Mapper.Instance.GetTableMapFor<Term>().AssertSome();
			Assert.IsAssignableFrom<TermTable>(s6.Table);

			var s7 = Mapper.Instance.GetTableMapFor<TermMeta>().AssertSome();
			Assert.IsAssignableFrom<TermMetaTable>(s7.Table);

			var s8 = Mapper.Instance.GetTableMapFor<TermRelationship>().AssertSome();
			Assert.IsAssignableFrom<TermRelationshipTable>(s8.Table);

			var s9 = Mapper.Instance.GetTableMapFor<TermTaxonomy>().AssertSome();
			Assert.IsAssignableFrom<TermTaxonomyTable>(s9.Table);

			var s10 = Mapper.Instance.GetTableMapFor<User>().AssertSome();
			Assert.IsAssignableFrom<UserTable>(s10.Table);

			var s11 = Mapper.Instance.GetTableMapFor<UserMeta>().AssertSome();
			Assert.IsAssignableFrom<UserMetaTable>(s11.Table);
		}

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
