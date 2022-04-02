// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Globalization;
using Jeebs.Collections;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Jeebs.Mvc.TagHelpers;

/// <summary>
/// Paging TagHelper
/// </summary>
[HtmlTargetElement("paging", TagStructure = TagStructure.WithoutEndTag)]
public sealed class PagingTagHelper : TagHelper
{
	/// <summary>
	/// Paging Values - normally returned from a PagedList
	/// </summary>
	public IPagingValues Values { get; set; } = new PagingValues();

	/// <summary>
	/// Base URL for paging links
	/// </summary>
	public string Url { get; set; } = string.Empty;

	/// <summary>
	/// The current Query string (for passing to each page via the URL Query string)
	/// </summary>
	public string Query { get; set; } = string.Empty;

	/// <summary>
	/// The Query prefix (for passing to each page via the URL Query string)
	/// </summary>
	public string QueryPrefix { get; set; } = "q";

	/// <summary>
	/// Wrapper HTML Tag
	/// </summary>
	public string WrapperHtmlTag { get; set; } = "div";

	/// <summary>
	/// Wrapper CSS class
	/// </summary>
	public string WrapperClass { get; set; } = "paging";

	/// <summary>
	/// Link HTML tag
	/// </summary>
	public string LinkHtmlTag { get; set; } = "span";

	/// <summary>
	/// Active page CSS class
	/// </summary>
	public string LinkOnClass { get; set; } = "on";

	/// <summary>
	/// Output paging values
	/// </summary>
	/// <param name="context">TagHelperContext</param>
	/// <param name="output">TagHelperOutput</param>
	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		// If there aren't any results, don't display anything
		if (Values.Items == 0)
		{
			output.SuppressOutput();
			return;
		}

		// Return nothing for one page
		if (Values.Pages <= 1)
		{
			output.SuppressOutput();
			return;
		}

		// Build the href string, containing a placeholder to replace with the page number
		var href = Url + "?p={0}";
		if (!string.IsNullOrEmpty(Query))
		{
			href += $"&amp;{QueryPrefix}={Query}";
		}

		// Create the wrapper
		output.TagName = WrapperHtmlTag;
		output.TagMode = TagMode.StartTagAndEndTag;
		output.Attributes.Add("class", WrapperClass);

		// 'First' link
		AddLink(1, "First");

		// 'Prev' link
		if (Values.Page > 1)
		{
			AddLink(Values.Page - 1, "Prev");
		}

		// Loop through all pages and add links
		for (double i = Values.LowerPage; i <= Values.UpperPage + 1; i++)
		{
			// Add previous 'Page of Pages' link
			if (i == Values.LowerPage && i > 1)
			{
				AddLink(i - 1, "<");
				AddLink(i, i.ToString(CultureInfo.InvariantCulture));
				continue;
			}

			// Add next 'Page of Pages' link
			if (i == Values.UpperPage + 1 && i <= Values.Pages)
			{
				AddLink(i, ">");
				continue;
			}

			// The OR condition is in case we ever want to display the current page link differently
			if (i == Values.Page || i <= Values.Pages)
			{
				AddLink(i, i.ToString(CultureInfo.InvariantCulture));
			}
		}

		// 'Next' link
		if (Values.Page < Values.Pages)
		{
			AddLink(Values.Page + 1, "Next");
		}

		// 'Last' link
		AddLink(Values.Pages, "Last");

		// Add a link to the output
		void AddLink(double page, string text)
		{
			// Get CSS class
			var css = page == Values.Page ? LinkOnClass : "";

			// Build link - we can't use TagBuilder as it encodes the URL which is already encoded
			var a = $"<{LinkHtmlTag} class=\"{css}\"><a href=\"{string.Format(CultureInfo.InvariantCulture, href, page)}\">{text}</a></{LinkHtmlTag}>";

			// Add to the wrapper
			_ = output.Content.AppendHtml(a);
		}
	}
}
