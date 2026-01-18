// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Apps.Web.Constants;

/// <summary>
/// Cache Profiles.
/// </summary>
public static class CacheProfiles
{
	/// <summary>
	/// Specifies no caching.
	/// </summary>
	/// <remarks>
	/// It is a constant so it can be used in the <see cref="Microsoft.AspNetCore.Mvc.CacheProfile"/> attribute
	/// </remarks>
	public const string None = nameof(None);

	/// <summary>
	/// Specifies default caching profile.
	/// </summary>
	/// <remarks>
	/// It is a constant so it can be used in the <see cref="Microsoft.AspNetCore.Mvc.CacheProfile"/> attribute
	/// </remarks>
	public const string Default = nameof(Default);
}
