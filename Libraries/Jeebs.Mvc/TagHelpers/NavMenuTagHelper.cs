using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using Jeebs.Mvc.Models;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Jeebs.Mvc.TagHelpers
{
	/// <summary>
	/// Nav Menu TagHelper
	/// </summary>
	[HtmlTargetElement("nav-menu", TagStructure = TagStructure.WithoutEndTag)]
	public sealed class NavMenuTagHelper : UrlResolutionTagHelper
	{
		/// <summary>
		/// Menu
		/// </summary>
		public Menu? Menu { get; set; }

		/// <summary>
		/// Default: nav nav-pills flex-column
		/// </summary>
		public string ListClass { get; set; } = "nav nav-pills flex-column";

		/// <summary>
		/// Default: nav-item
		/// </summary>
		public string ListItemClass { get; set; } = "nav-item";

		/// <summary>
		/// Default: nav-link
		/// </summary>
		public string LinkClass { get; set; } = "nav-link";

		/// <summary>
		/// Default: active
		/// </summary>
		public string LinkActiveClass { get; set; } = "active";

		/// <summary>
		/// Default: false
		/// </summary>
		public bool IncludeChildren { get; set; }

		/// <summary>
		/// Default: nav-child
		/// </summary>
		public string ChildMenuClass { get; set; } = "nav-child";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="urlHelperFactory">IUrlHelperFactory object</param>
		/// <param name="htmlEncoder">HtmlEncoder</param>
		public NavMenuTagHelper(IUrlHelperFactory urlHelperFactory, HtmlEncoder htmlEncoder) : base(urlHelperFactory, htmlEncoder)
		{
		}

		/// <summary>
		/// Process TagHelper
		/// </summary>
		/// <param name="context">TagHelperContext</param>
		/// <param name="output">TagHelperOutput</param>
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			// Return if no menu or items to output
			if (Menu == null || Menu.Items.Count == 0)
			{
				return;
			}

			// Setup objects
			var currentController = ViewContext.ControllerName().ToLower();
			var currentAction = ((ControllerActionDescriptor)ViewContext.ActionDescriptor).ActionName.ToLower();
			var urlHelper = UrlHelperFactory.GetUrlHelper(ViewContext);

			// Add each menu item to the menu
			foreach (var item in Menu.Items)
			{
				// Create list item
				TagBuilder listItem = new TagBuilder("li");
				listItem.AddCssClass(ListItemClass);

				// Create link
				TagBuilder link = new TagBuilder("a");
				link.AddCssClass(LinkClass);

				// For parent menu items, only check the controller for active links
				if (item.Controller.ToLower() == currentController)
				{
					link.AddCssClass(LinkActiveClass);
				}

				// Generate the link URL
				var urlActionContext = new UrlActionContext { Controller = item.Controller, Action = "Index" };
				link.Attributes.Add("href", urlHelper.Action(urlActionContext));

				// Add link text - if not set use controller name
				link.InnerHtml.Append(item.Text ?? item.Controller);

				// Add the link to the list item
				listItem.InnerHtml.AppendHtml(link);

				// Check for child menu
				if (item.Children?.Count > 0)
				{
					var childMenu = BuildChildMenu(item.Children);
					listItem.InnerHtml.AppendHtml(childMenu);
				}

				// Add list item to menu
				output.Content.AppendHtml(listItem);
			}

			// Set output properties
			output.TagName = "ul";
			output.TagMode = TagMode.StartTagAndEndTag;
			output.Attributes.Add("class", ListClass);

			// Build a child menu
			TagBuilder BuildChildMenu(List<MenuItem> childMenuItems)
			{
				// Create child menu
				TagBuilder childMenu = new TagBuilder("ul");
				childMenu.AddCssClass(ListClass);
				childMenu.AddCssClass(ChildMenuClass);

				// Add each menu item to the menu
				foreach (var item in childMenuItems)
				{
					// Create list item
					TagBuilder listItem = new TagBuilder("li");
					listItem.AddCssClass(ListItemClass);

					// Create link
					TagBuilder link = new TagBuilder("a");
					link.AddCssClass(LinkClass);

					// For child menu items, only check the action for active links
					if (item.Action.ToLower() == currentAction)
					{
						link.AddCssClass(LinkActiveClass);
					}

					// Generate the link URL
					var urlActionContext = new UrlActionContext { Controller = item.Controller, Action = item.Action };
					link.Attributes.Add("href", urlHelper.Action(urlActionContext));

					// Add link text - if not set use action name
					link.InnerHtml.Append(item.Text ?? item.Action);

					// Add the link to the list item
					listItem.InnerHtml.AppendHtml(link);

					// Add list item to child menu
					childMenu.InnerHtml.AppendHtml(listItem);
				}

				return childMenu;
			}
		}
	}
}
