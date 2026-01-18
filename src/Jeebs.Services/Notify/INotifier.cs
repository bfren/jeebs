// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Services.Notify;

/// <summary>
/// Handles notifications - like logging, but bypasses the minimum level filters.
/// </summary>
public interface INotifier
{
	/// <summary>
	/// Send an information notification.
	/// </summary>
	/// <param name="message">Message content</param>
	void Send(string message);

	/// <summary>
	/// Send a notification.
	/// </summary>
	/// <param name="message">Message content</param>
	/// <param name="level">Notification level</param>
	void Send(string message, NotificationLevel level);

	/// <summary>
	/// Send a notification message.
	/// </summary>
	/// <param name="msg">The message to send as a notification</param>
	void Send(IMsg msg);
}
