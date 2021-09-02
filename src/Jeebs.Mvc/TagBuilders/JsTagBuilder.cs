﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Jeebs.Mvc.TagBuilders
{
	/// <summary>
	/// JS TagBuilder
	/// </summary>
	public sealed class JsTagBuilder : TagBuilder
	{
		/// <summary>
		/// Create object
		/// </summary>
		public JsTagBuilder() : base("script") =>
			Attributes.Add("type", "text/javascript");

		/// <summary>
		/// Construct object
		/// </summary>
		/// <param name="src">Script file source</param>
		/// <param name="async">[Optional] Output async attribute</param>
		/// <param name="defer">[Optional] Output defer attribute</param>
		public JsTagBuilder(string src, bool async = false, bool defer = false) : this()
		{
			Attributes.Add("src", src);

			if (async) Attributes.Add("async", "async");
			if (defer) Attributes.Add("defer", "defer");
		}
	}
}
