// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Query;
using Jeebs.WordPress.Entities.StrongIds;

namespace Jeebs.WordPress;

/// <summary>
/// Query Terms Options
/// </summary>
public interface IQueryTermsOptions : IQueryOptions<WpTermId>
{
	/// <summary>
	/// Search taxonomy type
	/// </summary>
	Enums.Taxonomy? Taxonomy { get; init; }

	/// <summary>
	/// Search taxonomy term
	/// </summary>
	string? Slug { get; init; }

	/// <summary>
	/// Search taxonomy count (default: 1)
	/// (to override and show everything, set to zero)
	/// </summary>
	long CountAtLeast { get; init; }
}
