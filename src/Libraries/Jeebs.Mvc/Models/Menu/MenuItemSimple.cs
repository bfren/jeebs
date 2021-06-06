// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;

namespace Jeebs.Mvc.Models
{
	/// <summary>
	/// MenuItem Simple
	/// </summary>
	public struct MenuItemSimple
	{
		/// <summary>
		/// Each menu item needs a unique identifier
		/// </summary>
		public Guid Guid { get; set; }

		/// <summary>
		/// Menu item display text
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// Fully qualified URI to this item
		/// </summary>
		public string Url { get; set; }
	}
}
