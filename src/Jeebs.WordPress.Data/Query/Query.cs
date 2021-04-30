// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.WordPress.Data.Entities;

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
		/// <typeparam name="TPost">Post Entity type</typeparam>
		/// <param name="opt">Modify default options</param>
		public delegate PostsOptions<TPost> GetPostsOptions<TPost>(PostsOptions<TPost> opt)
			where TPost : WpPostEntity;

		/// <summary>
		/// Get default PostsMetaOptions and modify
		/// </summary>
		/// <typeparam name="TTaxonomy">Post Meta Entity type</typeparam>
		/// <param name="opt">Modify default options</param>
		public delegate PostsMetaOptions<TTaxonomy> GetPostsMetaOptions<TTaxonomy>(PostsMetaOptions<TTaxonomy> opt)
			where TTaxonomy : WpPostMetaEntity;

		/// <summary>
		/// Get default TaxonomyOptions and modify
		/// </summary>
		/// <typeparam name="TTerm">Term Entity type</typeparam>
		/// <param name="opt">Modify default options</param>
		public delegate PostsTaxonomy<TTerm> GetTermOptions<TTerm>(PostsTaxonomy<TTerm> opt)
			where TTerm : WpTermEntity;
	}
}
