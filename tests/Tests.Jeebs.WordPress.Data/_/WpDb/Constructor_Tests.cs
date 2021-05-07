// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using Jeebs;
using Jeebs.Config;
using Jeebs.Data;
using Jeebs.Data.Mapping;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Tests.Jeebs.WordPress.Data.WpDb_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Maps_All_Entities_With_TablePrefix()
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

			var prefix = F.Rnd.Str;
			var wpConfig = Substitute.For<IOptions<WpConfig>>();
			wpConfig.Value.Returns(new WpConfig
			{
				TablePrefix = prefix
			});

			// Act
			var _ = new WpDb<Comment, CommentMeta, Link, Option, Post, PostMeta, Term, TermMeta,
				TermRelationship, TermTaxonomy, User, UserMeta>(client, dbConfig, wpConfig, log);

			// Assert
			var s0 = Mapper.Instance.GetTableMapFor<Comment>().AssertSome();
			Assert.StartsWith(prefix, s0.Table.GetName());

			var s1 = Mapper.Instance.GetTableMapFor<CommentMeta>().AssertSome();
			Assert.StartsWith(prefix, s1.Table.GetName());

			var s2 = Mapper.Instance.GetTableMapFor<Link>().AssertSome();
			Assert.StartsWith(prefix, s2.Table.GetName());

			var s3 = Mapper.Instance.GetTableMapFor<Option>().AssertSome();
			Assert.StartsWith(prefix, s3.Table.GetName());

			var s4 = Mapper.Instance.GetTableMapFor<Post>().AssertSome();
			Assert.StartsWith(prefix, s4.Table.GetName());

			var s5 = Mapper.Instance.GetTableMapFor<PostMeta>().AssertSome();
			Assert.StartsWith(prefix, s5.Table.GetName());

			var s6 = Mapper.Instance.GetTableMapFor<Term>().AssertSome();
			Assert.StartsWith(prefix, s6.Table.GetName());

			var s7 = Mapper.Instance.GetTableMapFor<TermMeta>().AssertSome();
			Assert.StartsWith(prefix, s7.Table.GetName());

			var s8 = Mapper.Instance.GetTableMapFor<TermRelationship>().AssertSome();
			Assert.StartsWith(prefix, s8.Table.GetName());

			var s9 = Mapper.Instance.GetTableMapFor<TermTaxonomy>().AssertSome();
			Assert.StartsWith(prefix, s9.Table.GetName());

			var s10 = Mapper.Instance.GetTableMapFor<User>().AssertSome();
			Assert.StartsWith(prefix, s10.Table.GetName());

			var s11 = Mapper.Instance.GetTableMapFor<UserMeta>().AssertSome();
			Assert.StartsWith(prefix, s11.Table.GetName());
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
