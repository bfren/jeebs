// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Mvc.Models
{
	/// <summary>
	/// MenuItem Text
	/// </summary>
	public class MenuItemText : MenuItem
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
