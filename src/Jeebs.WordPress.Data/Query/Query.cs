// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// Wrapper class for WordPress query objects
	/// </summary>
	public static partial class Query
	{
		/// <summary>
		/// Get default PostsOptions and modify
		/// </summary>
		/// <param name="opt">Modify default options</param>
		public delegate PostsOptions GetPostsOptions(PostsOptions opt);

		/// <summary>
		/// Get default PostsMetaOptions and modify
		/// </summary>
		/// <param name="opt">Modify default options</param>
		public delegate PostsMetaOptions GetPostsMetaOptions(PostsMetaOptions opt);

		/// <summary>
		/// Get default PostsTaxonomyOptions and modify
		/// </summary>
		/// <param name="opt">Modify default options</param>
		public delegate PostsTaxonomyOptions GetPostsTaxonomyOptions(PostsTaxonomyOptions opt);

		/// <summary>
		/// Get default TermsOptions and modify
		/// </summary>
		/// <param name="opt">Modify default options</param>
		public delegate TermsOptions GetTermsOptions(TermsOptions opt);
	}
}
