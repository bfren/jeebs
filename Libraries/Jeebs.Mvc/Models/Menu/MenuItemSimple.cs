// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

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
