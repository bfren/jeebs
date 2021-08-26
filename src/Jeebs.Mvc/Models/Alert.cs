// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Mvc.Models
{
	/// <summary>
	/// User feedback alert
	/// </summary>
	/// <param name="Type">Alert type</param>
	/// <param name="Text">Alert text</param>
	public sealed record class Alert(AlertType Type, string Text);
}
