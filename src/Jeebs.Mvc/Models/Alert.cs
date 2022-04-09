// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Mvc.Enums;

namespace Jeebs.Mvc.Models;

/// <summary>
/// User feedback alert
/// </summary>
/// <param name="Type">Alert type</param>
/// <param name="Text">Alert message</param>
public sealed record class Alert(AlertType Type, string Text)
{
	/// <summary>
	/// Create an error alert
	/// </summary>
	/// <param name="message"></param>
	public static Alert Error(string message) =>
		new(AlertType.Error, message);

	/// <summary>
	/// Create an info alert
	/// </summary>
	/// <param name="message"></param>
	public static Alert Info(string message) =>
		new(AlertType.Info, message);

	/// <summary>
	/// Create success alert
	/// </summary>
	/// <param name="message"></param>
	public static Alert Success(string message) =>
		new(AlertType.Success, message);

	/// <summary>
	/// Create a warning alert
	/// </summary>
	/// <param name="message"></param>
	public static Alert Warning(string message) =>
		new(AlertType.Warning, message);
}
