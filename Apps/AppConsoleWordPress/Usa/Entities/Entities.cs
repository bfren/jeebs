using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.WordPress.Entities;

namespace AppConsoleWordPress.Usa.Entities
{
	public sealed class Comment : WpCommentEntity { }
	public sealed class CommentMeta : WpCommentMetaEntity { }
	public sealed class Link : WpLinkEntity { }
	public sealed class Option : WpOptionEntity { }
	public sealed class Post : WpPostEntity { }
	public sealed class PostMeta : WpPostMetaEntity { }
	public sealed class Term : WpTermEntity { }
	public sealed class TermMeta : WpTermMetaEntity { }
	public sealed class TermRelationship : WpTermRelationshipEntity { }
	public sealed class TermTaxonomy : WpTermTaxonomyEntity { }
	public sealed class User : WpUserEntity { }
	public sealed class UserMeta : WpUserMetaEntity { }
}
