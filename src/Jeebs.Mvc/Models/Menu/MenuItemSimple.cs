// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;

namespace Jeebs.Mvc.Models
{
	/// <summary>
	/// MenuItem Simple
	/// </summary>
	/// <param name="Guid">Each menu item needs a unique identifier</param>
	/// <param name="Text">Menu item display text</param>
	/// <param name="Url">Fully qualified URI to this item</param>
	public sealed record class MenuItemSimple(Guid Guid, string Text, string Url);
}
