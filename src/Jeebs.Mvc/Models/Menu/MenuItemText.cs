// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Mvc.Models
{
	/// <summary>
	/// MenuItem Text
	/// </summary>
	public record MenuItemText : MenuItem
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
