// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

namespace Jeebs.Mvc.Models
{
	/// <summary>
	/// User feedback alert
	/// </summary>
	/// <param name="Type">Alert type</param>
	/// <param name="Text">Alert text</param>
	public sealed record Alert(AlertType Type, string Text);
}
