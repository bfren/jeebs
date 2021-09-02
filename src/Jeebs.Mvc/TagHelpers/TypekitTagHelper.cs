// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Jeebs.Mvc.TagHelpers
{
	/// <summary>
	/// Typekit TagHelper
	/// </summary>
	[HtmlTargetElement("typekit", TagStructure = TagStructure.WithoutEndTag)]
	public sealed class TypekitTagHelper : TagHelper
	{
		/// <summary>
		/// Typekit Library reference
		/// </summary>
		public string Library { get; set; } = string.Empty;

		/// <summary>
		/// Process the tag helper
		/// </summary>
		/// <param name="context">TagHelperContext object</param>
		/// <param name="output">TagHelperOutput object</param>
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			// Check library
			if (string.IsNullOrWhiteSpace(Library))
			{
				output.SuppressOutput();
				return;
			}

			// Set tag details
			output.TagName = "link";
			output.TagMode = TagMode.SelfClosing;

			// Add Typekit code
			output.Attributes.Add("rel", "stylesheet");
			output.Attributes.Add("href", $"//use.typekit.net/{Library}.css");
		}
	}
}
