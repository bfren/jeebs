// Jeebs Test Applications
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.WordPress.Data.Entities;

namespace AppConsoleWp.Usa.Entities
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
