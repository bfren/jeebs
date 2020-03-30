using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Jeebs.Mvc.TagBuilders
{
	/// <summary>
	/// CSS TagBuilder
	/// </summary>
	public sealed class CssTagBuilder : TagBuilder
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="href">Stylesheet URI</param>
		public CssTagBuilder(string href) : base("link")
		{
			Attributes.Add("rel", "stylesheet");
			Attributes.Add("href", href);
		}
	}
}
