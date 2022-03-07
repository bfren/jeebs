// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Jeebs.Mvc.TagHelpers;

/// <summary>
/// Email TagHelper
/// </summary>
[HtmlTargetElement("email", TagStructure = TagStructure.WithoutEndTag)]
public sealed class EmailTagHelper : UrlResolutionTagHelper
{
	/// <summary>
	/// Email address
	/// </summary>
	public string To { get; set; } = string.Empty;

	/// <summary>
	/// The text to display in the link - if null, will display the email address
	/// </summary>
	public string? Display { get; set; }

	/// <summary>
	/// Email subject
	/// </summary>
	public string Subject { get; set; } = string.Empty;

	/// <summary>
	/// Setup dependencies
	/// </summary>
	/// <param name="urlHelperFactory">IUrlHelperFactory object</param>
	/// <param name="htmlEncoder">HtmlEncoder</param>
	public EmailTagHelper(IUrlHelperFactory urlHelperFactory, HtmlEncoder htmlEncoder) : base(urlHelperFactory, htmlEncoder) { }

	/// <summary>
	/// Process the tag helper
	/// </summary>
	/// <param name="context">TagHelperContext object</param>
	/// <param name="output">TagHelperOutput object</param>
	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		// Check To
		if (string.IsNullOrWhiteSpace(To))
		{
			output.SuppressOutput();
			return;
		}

		// Create the mailto link
		var href = "mailto:" + To;
		if (!string.IsNullOrEmpty(Subject))
		{
			href += "?Subject=" + Subject;
		}

		// Encode href / display text
		var hrefEncoded = new HtmlString(href.ToASCII());
		var displayEncoded = new HtmlString((Display ?? To).ToASCII());

		// We can't use tag builder here because it encodes attributes, which we have already done
		output.TagName = "a";
		output.TagMode = TagMode.StartTagAndEndTag;
		output.Attributes.Add("href", hrefEncoded);
		_ = output.Content.SetHtmlContent(displayEncoded);
	}
}
