using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Jeebs.Mvc
{
	/// <summary>
	/// HtmlHelper Extensions - Paging
	/// </summary>
	public static class HtmlHelperExtensions
	{
		/// <summary>
		/// Output paging links
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="this">IHtmlHelper</param>
		/// <param name="values">IPagedList</param>
		/// <param name="url">Base URL</param>
		/// <param name="query">Search query</param>
		/// <param name="queryPrefix">Query prefix</param>
		/// <param name="wrapperHtmlTag">Wrapper HTML tag</param>
		/// <param name="wrapperClass">Wrapper CSS class</param>
		/// <param name="linkHtmlTag">Link HTML tag</param>
		/// <param name="linkOnClass">Active link CSS class</param>
		/// <returns></returns>
		public static IHtmlContent Paging(
			this IHtmlHelper @this,
			IPagingValues values,
			string url,
			string query,
			string queryPrefix = "q",
			string wrapperHtmlTag = "div",
			string wrapperClass = "paging",
			string linkHtmlTag = "span",
			string linkOnClass = "on"
		)
		{
			// If there aren't any results, don't display anything
			if (values.Items == 0)
			{
				return HtmlString.Empty;
			}

			// Calculate paging values

			// Return nothing for one page
			if (values.Pages <= 1)
			{
				return HtmlString.Empty;
			}

			// Build the href string, containing a placeholder to replace with the page number
			var href = url + "?p={0}";
			if (!string.IsNullOrEmpty(query))
			{
				href += $"&{queryPrefix}={query}";
			}

			// Create the wrapper
			TagBuilder wrapper = new TagBuilder(wrapperHtmlTag);
			wrapper.Attributes.Add("class", wrapperClass);

			// 'First' link
			AddLink(1, "First");

			// 'Prev' link
			if (values.Page > 1)
			{
				AddLink(values.Page - 1, "Prev");
			}

			// Loop through all pages and add links
			for (double i = values.LowerPage; i <= values.UpperPage + 1; i++)
			{
				// Add previous 'Page of Pages' link
				if (i == values.LowerPage && i > 1)
				{
					AddLink(i - 1, "<");
					AddLink(i, i.ToString());
					continue;
				}

				// Add next 'Page of Pages' link
				if (i == values.UpperPage + 1 && i <= values.Pages)
				{
					AddLink(i, ">");
					continue;
				}

				// The OR condition is in case we ever want to display the current page link differently
				if (i == values.Page || i <= values.Pages)
				{
					AddLink(i, i.ToString());
				}
			}

			// 'Next' link
			if (values.Page < values.Pages)
			{
				AddLink(values.Page + 1, "Next");
			}

			// 'Last' link
			AddLink(values.Pages, "Last");

			// Return the wrapper with all the links inside
			return wrapper;

			// Add a link to the wrapper
			void AddLink(double page, string text)
			{
				// Get CSS class
				var css = page == values.Page ? linkOnClass : "";

				// Build link - we can't use TagBuilder as it encodes the URL which is already encoded
				var a = $"<{linkHtmlTag} class=\"{css}\"><a href=\"{string.Format(href, page)}\">{text}</a></{linkHtmlTag}>";

				// Add to the wrapper
				wrapper.InnerHtml.AppendHtml(a);
			}
		}
	}
}
