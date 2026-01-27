// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Jeebs.Mvc.TagBuilders;

/// <summary>
/// CSS TagBuilder.
/// </summary>
public sealed class CssTagBuilder : TagBuilder
{
	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="href">Stylesheet URI.</param>
	public CssTagBuilder(string href) : base("link")
	{
		Attributes.Add("rel", "stylesheet");
		Attributes.Add("href", href);
	}
}
