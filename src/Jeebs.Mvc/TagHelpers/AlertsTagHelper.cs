// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Mvc.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Jeebs.Mvc.TagHelpers
{
	/// <summary>
	/// Alert TagHelper
	/// </summary>
	[HtmlTargetElement("alerts", TagStructure = TagStructure.WithoutEndTag)]
	public sealed class AlertsTagHelper : TagHelper
	{
		/// <summary>
		/// ViewContext object
		/// </summary>
		[ViewContext]
		public ViewContext? ViewContext { get; set; }

		/// <summary>
		/// Output any alert messages
		/// </summary>
		/// <param name="context">TagHelperContext</param>
		/// <param name="output">TagHelperOutput</param>
		public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			// If there are alerts, display them
			if (ViewContext?.TempData.GetAlerts() is List<Alert> alerts && alerts.Count > 0)
			{
				// Create wrapper
				output.TagName = "div";
				output.TagMode = TagMode.StartTagAndEndTag;
				output.Attributes.Add("class", "jeebs-alerts");

				// Add each alert
				foreach (var alert in alerts)
				{
					var alertTag = new TagBuilder("div");
					alertTag.MergeAttribute("class", $"jeebs-alert jeebs-alert-{alert.Type}");
					alertTag.InnerHtml.Append(alert.Text);
					output.Content.AppendHtml(alertTag);
				}
			}
			else
			{
				output.SuppressOutput();
			}

			return Task.CompletedTask;
		}
	}
}
