using Jeebs.WordPress.Entities;

namespace AppConsoleWordPress.Bcg.Entities
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
