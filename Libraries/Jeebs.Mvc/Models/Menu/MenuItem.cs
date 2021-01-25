using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Mvc.Models
{
	/// <summary>
	/// Menu Item
	/// </summary>
	public class MenuItem
	{
		/// <summary>
		/// Each menu item needs a unique identifier
		/// </summary>
		public Guid Guid { get; set; } = Guid.NewGuid();

		/// <summary>
		/// The text to display in the link
		/// </summary>
		public string? Text { get; set; }

		/// <summary>
		/// The link description - if set, will be added as the title attribute
		/// </summary>
		public string? Description { get; set; }

		/// <summary>
		/// The controller to use in the link
		/// </summary>
		public string Controller { get; set; } = string.Empty;

		/// <summary>
		/// The action to use in the link
		/// </summary>
		public string Action { get; set; } = "Index";

		/// <summary>
		/// Optional route values to be added to the URL
		/// </summary>
		public object RouteValues { get; set; } = new object();

		/// <summary>
		/// List of child menu items, for hierarchical menus
		/// </summary>
		public List<MenuItem> Children { get; set; } = new List<MenuItem>();

		/// <summary>
		/// Whether or not this menu item is a link (if false, it is just text to output)
		/// </summary>
		public bool IsLink { get; protected set; } = true;

		/// <summary>
		/// Required user roles - if set, the item will only be shown if the user HAS one of the specified roles
		/// </summary>
		public string[] Roles { get; set; } = Array.Empty<string>();

		/// <summary>
		/// Add a child menu item, using action as text if text is null
		/// The controller will be set to the same as the parent menu item
		/// </summary>
		/// <param name="action">The action for the link</param>
		/// <param name="text">[Optional] The text to display in the link - if null, action will be used instead</param>
		/// <param name="description">[Optional] If set, will add a description and title attribute to the menu item</param>
		public void AddChild(string action, string? text = null, string? description = null)
		{
			// If the text is empty, use action by default
			if (string.IsNullOrEmpty(text))
			{
				text = action;
			}

			// Add the child item
			Children.Add(new MenuItem
			{
				Controller = Controller,
				Action = action,
				Text = text,
				Description = description,
				RouteValues = RouteValues
			});
		}
	}
}
