// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Mvc.Models
{
	/// <summary>
	/// MenuItem Text
	/// </summary>
	public sealed record class MenuItemText : MenuItem
	{
		/// <summary>
		/// Set IsLink to be false
		/// </summary>
		public MenuItemText(string text)
		{
			IsLink = false;
			Text = text;
		}
	}
}
