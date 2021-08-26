// Jeebs Test Applications
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.WordPress.Data.Entities;

namespace AppConsoleWp.Bcg.Entities
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
