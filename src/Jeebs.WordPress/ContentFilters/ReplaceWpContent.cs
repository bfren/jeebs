﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.WordPress.ContentFilters;

/// <summary>
/// Replace wp-content/ URLs (e.g. with static file server)
/// </summary>
public sealed class ReplaceWpContent : ContentFilter
{
	/// <inheritdoc/>
	private ReplaceWpContent(Func<string, string> filter) : base(filter) { }

	/// <summary>
	/// Create filter
	/// </summary>
	/// <param name="from">Search URL</param>
	/// <param name="to">Replacement URL</param>
	public static ContentFilter Create(string from, string to) =>
		new ReplaceWpContent(content => content.Replace(from, to));
}
