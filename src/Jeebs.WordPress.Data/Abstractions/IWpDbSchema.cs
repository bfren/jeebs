// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using Jeebs.WordPress.Data.Tables;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// WordPress Database schema
	/// </summary>
	public interface IWpDbSchema
	{
		/// <summary>
		/// Comment Table
		/// </summary>
		CommentTable Comment { get; }

		/// <summary>
		/// Comment Meta Table
		/// </summary>
		CommentMetaTable CommentMeta { get; }

		/// <summary>
		/// Link Table
		/// </summary>
		LinkTable Link { get; }

		/// <summary>
		/// Option Table
		/// </summary>
		OptionTable Option { get; }

		/// <summary>
		/// Post Table
		/// </summary>
		PostTable Post { get; }

		/// <summary>
		/// Post Meta Table
		/// </summary>
		PostMetaTable PostMeta { get; }

		/// <summary>
		/// Term Table
		/// </summary>
		TermTable Term { get; }

		/// <summary>
		/// Term Meta Table
		/// </summary>
		TermMetaTable TermMeta { get; }

		/// <summary>
		/// Term Relationship Table
		/// </summary>
		TermRelationshipTable TermRelationship { get; }

		/// <summary>
		/// Term Taxonomy Table
		/// </summary>
		TermTaxonomyTable TermTaxonomy { get; }

		/// <summary>
		/// User Table
		/// </summary>
		UserTable User { get; }

		/// <summary>
		/// User Meta Table
		/// </summary>
		UserMetaTable UserMeta { get; }
	}
}
