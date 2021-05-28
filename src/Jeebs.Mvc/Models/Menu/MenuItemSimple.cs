// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs.Mvc.Models
{
	/// <summary>
	/// MenuItem Simple
	/// </summary>
	/// <param name="Guid">Each menu item needs a unique identifier</param>
	/// <param name="Text">Menu item display text</param>
	/// <param name="Url">Fully qualified URI to this item</param>
	public record MenuItemSimple(Guid Guid, string Text, string Url);
}
