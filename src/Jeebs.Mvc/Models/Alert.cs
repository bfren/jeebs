// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Mvc.Models
{
	/// <summary>
	/// User feedback alert
	/// </summary>
	/// <param name="Type">Alert type</param>
	/// <param name="Text">Alert text</param>
	public sealed record Alert(AlertType Type, string Text);
}
