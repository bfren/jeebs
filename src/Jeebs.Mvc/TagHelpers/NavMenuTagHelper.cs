// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Encodings.Web;
using Jeebs.Mvc.Models;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Jeebs.Mvc.TagHelpers;

/// <summary>
/// Nav Menu TagHelper.
/// </summary>
/// <remarks>
/// Create object
/// </remarks>
/// <param name="urlHelperFactory">IUrlHelperFactory object.</param>
/// <param name="htmlEncoder">HtmlEncoder.</param>
[HtmlTargetElement("nav-menu", TagStructure = TagStructure.WithoutEndTag)]
public sealed class NavMenuTagHelper(
	IUrlHelperFactory urlHelperFactory,
	HtmlEncoder htmlEncoder
) : UrlResolutionTagHelper(urlHelperFactory, htmlEncoder)
{
	/// <summary>
	/// Menu.
	/// </summary>
	public Menu? Menu { get; set; }

	/// <summary>
	/// Default: ul.
	/// </summary>
	public string WrapperElement { get; set; } = "ul";

	/// <summary>
	/// Default: nav nav-pills flex-column.
	/// </summary>
	public string WrapperClass { get; set; } = "nav nav-pills flex-column";

	/// <summary>
	/// Default: li.
	/// </summary>
	public string ItemElement { get; set; } = "li";

	/// <summary>
	/// Default: nav-item.
	/// </summary>
	public string ItemClass { get; set; } = "nav-item";

	/// <summary>
	/// Default: nav-link.
	/// </summary>
	public string LinkClass { get; set; } = "nav-link";

	/// <summary>
	/// Default: active.
	/// </summary>
	public string LinkActiveClass { get; set; } = "active";

	/// <summary>
	/// Default: false.
	/// </summary>
	public bool IncludeChildren { get; set; }

	/// <summary>
	/// Default: nav-child.
	/// </summary>
	public string ChildMenuWrapperClass { get; set; } = "nav-child";

	/// <summary>
	/// Process TagHelper.
	/// </summary>
	/// <param name="context">TagHelperContext.</param>
	/// <param name="output">TagHelperOutput.</param>
	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		// Return if no menu or items to output
		if (Menu is null || Menu.Items.Count == 0)
		{
			output.SuppressOutput();
			return;
		}

		// Setup objects
		var currentController = ViewContext.ControllerName().ToLower(CultureInfo.InvariantCulture);
		var currentAction = ViewContext.ActionName().ToLower(CultureInfo.InvariantCulture);
		var urlHelper = UrlHelperFactory.GetUrlHelper(ViewContext);

		// Build the menu
		BuildMenu(
			Menu.Items,
			mi => string.Equals(mi.Controller, currentController, StringComparison.OrdinalIgnoreCase),
			mi => mi.Controller,
			el => output.Content.AppendHtml(el)
		);

		// Set output properties
		output.TagName = WrapperElement;
		output.TagMode = TagMode.StartTagAndEndTag;
		output.Attributes.Add("class", WrapperClass);

		// Build a menu (allows recursion for child menus)
		//
		//		items:		The menu items to build a menu for
		//		isActive:	Function which determines whether or not the current item is the active page
		//		getText:	If MenuItem.Text is null, returns the text to show in the link
		//		append:		Action to append the element to the menu wrapper
		void BuildMenu(List<MenuItem> items, Func<MenuItem, bool> isActive, Func<MenuItem, string> getText, Action<TagBuilder> append)
		{
			// Add each menu item to the menu
			foreach (var menuItem in items)
			{
				// Create item element
				var item = new TagBuilder(ItemElement);
				item.AddCssClass(ItemClass);

				// Create link element
				var link = new TagBuilder("a");
				link.AddCssClass(LinkClass);

				// Add GUID
				link.MergeAttribute("id", $"link-{menuItem.Id}");

				// Check whether or not this is an active link / section
				if (isActive(menuItem))
				{
					link.AddCssClass(LinkActiveClass);
				}

				// Generate the link URL
				var urlActionContext = new UrlActionContext
				{
					Controller = menuItem.Controller,
					Action = menuItem.Action,
					Values = menuItem.RouteValues
				};

				link.Attributes.Add("href", urlHelper.Action(urlActionContext));

				// Add link text - if not set use getText() function
				_ = link.InnerHtml.Append(menuItem.Text ?? getText(menuItem));

				// Add the link to the list item
				_ = item.InnerHtml.AppendHtml(link);

				// Check for child menu
				if (IncludeChildren && menuItem.Children.Count > 0)
				{
					// Create child menu wrapper
					var childMenu = new TagBuilder(WrapperElement);
					childMenu.AddCssClass(ChildMenuWrapperClass);

					// Build child menu
					BuildMenu(
						menuItem.Children,
						mi => string.Equals(mi.Action, currentAction, StringComparison.OrdinalIgnoreCase),
						mi => mi.Action,
						el => childMenu.InnerHtml.AppendHtml(el)
					);

					// Add child menu to item
					_ = item.InnerHtml.AppendHtml(childMenu);
				}

				// Append item
				append(item);
			}
		}
	}
}
