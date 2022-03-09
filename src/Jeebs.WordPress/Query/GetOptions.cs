// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Querying;

/// <summary>
/// Get default AttachmentsOptions and modify
/// </summary>
/// <param name="opt">Modify default options</param>
public delegate AttachmentsOptions GetAttachmentsOptions(AttachmentsOptions opt);

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
