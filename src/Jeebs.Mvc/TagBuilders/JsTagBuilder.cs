// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Jeebs.Mvc.TagBuilders;

/// <summary>
/// JS TagBuilder.
/// </summary>
public sealed class JsTagBuilder : TagBuilder
{
	/// <summary>
	/// Create object.
	/// </summary>
	public JsTagBuilder() : base("script") =>
		Attributes.Add("type", "text/javascript");

	/// <summary>
	/// Construct object with async and defer disabled.
	/// </summary>
	/// <param name="src">Script file source.</param>
	public JsTagBuilder(string src) : this(src, false, false) { }

	/// <summary>
	/// Construct object.
	/// </summary>
	/// <param name="src">Script file source.</param>
	/// <param name="useAsync">Output async attribute.</param>
	/// <param name="useDefer">Output defer attribute.</param>
	public JsTagBuilder(string src, bool useAsync, bool useDefer) : this()
	{
		Attributes.Add("src", src);

		if (useAsync)
		{
			Attributes.Add("async", "async");
		}

		if (useDefer)
		{
			Attributes.Add("defer", "defer");
		}
	}
}
