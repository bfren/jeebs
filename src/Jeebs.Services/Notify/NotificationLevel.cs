// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Services.Notify;

/// <summary>
/// Message levels.
/// </summary>
public enum NotificationLevel
{
	/// <summary>
	/// Information, or 'Green' message
	/// </summary>
	Information = 0,

	/// <summary>
	/// Warning, or 'Amber' message
	/// </summary>
	Warning = 1,

	/// <summary>
	/// Error, or 'Red' message
	/// </summary>
	Error = 2
}
