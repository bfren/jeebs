// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Jeebs.Mvc.TagHelpers;

/// <summary>
/// Image TagHelper
/// </summary>
[HtmlTargetElement("image", TagStructure = TagStructure.WithoutEndTag)]
public sealed class ImageTagHelper : UrlResolutionTagHelper
{
	/// <summary>
	/// Image src - if this starts '/' then it is assumed it is a path within the wwwroot/img directory -
	/// otherwise use ~/... to reference an image elsewhere within wwwroot or an absolute URL
	/// </summary>
	public string Src { get; set; } = string.Empty;

	/// <summary>
	/// If true, will override the '/' test and use Src as the image source
	/// </summary>
	public bool SrcDirect { get; set; }

	/// <summary>
	/// Required alternative text (will be used to set the title attribute as well)
	/// </summary>
	public string Alt { get; set; } = string.Empty;

	/// <summary>
	/// [Optional] CSS class to apply to the element
	/// </summary>
	public string CssClass { get; set; } = string.Empty;

	/// <summary>
	/// FileVersionProvider object
	/// </summary>
	private readonly IFileVersionProvider fileVersionProvider;

	/// <summary>
	/// Setup dependencies
	/// </summary>
	/// <param name="fileVersionProvider">IFileVersionProvider object</param>
	/// <param name="urlHelperFactory">IUrlHelperFactory object</param>
	/// <param name="htmlEncoder">HtmlEncoder</param>
	public ImageTagHelper(
		IFileVersionProvider fileVersionProvider,
		IUrlHelperFactory urlHelperFactory,
		HtmlEncoder htmlEncoder)
		: base(urlHelperFactory, htmlEncoder) =>
		this.fileVersionProvider = fileVersionProvider;

	/// <summary>
	/// Process the tag helper
	/// </summary>
	/// <param name="context">TagHelperContext object</param>
	/// <param name="output">TagHelperOutput object</param>
	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		// Check source
		if (string.IsNullOrWhiteSpace(Src))
		{
			output.SuppressOutput();
			return;
		}

		// By default the url is the raw Src attribute
		var url = Src;

		// Add file version to the URL if it is a local URL
		if (!SrcDirect && url.StartsWith("/", StringComparison.InvariantCulture) && TryResolveUrl($"~/img{Src}", out url))
		{
			// Add file version to the image
			url = fileVersionProvider.AddFileVersionToPath(ViewContext.HttpContext.Request.PathBase, url);
		}

		// Set tag options
		output.TagName = "img";
		output.TagMode = TagMode.SelfClosing;

		// Add src
		output.Attributes.Add("src", url);

		// Use Alt attribute
		output.Attributes.Add("alt", Alt);
		output.Attributes.Add("title", Alt);

		// Set CSS class
		if (!string.IsNullOrEmpty(CssClass))
		{
			output.Attributes.Add("class", CssClass);
		}
	}
}
