// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Extensions;

namespace Jeebs.WordPress.Data.ContentFilters;

/// <summary>
/// Curly Quotes
/// </summary>
public sealed class CurlQuotes : ContentFilter
{
	/// <inheritdoc/>
	private CurlQuotes(Func<string, string> filter) : base(filter) { }

	/// <summary>
	/// Create filter
	/// </summary>
	public static ContentFilter Create() =>
		new CurlQuotes(content => content.ConvertInnerHtmlQuotes());
}
