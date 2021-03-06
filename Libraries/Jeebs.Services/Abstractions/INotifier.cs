// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

namespace Jeebs
{
	/// <summary>
	/// Handles notifications - like logging, but bypasses the minimum level filters
	/// </summary>
	public interface INotifier
	{
		/// <summary>
		/// Send a notification
		/// </summary>
		/// <param name="message">Message content</param>
		/// <param name="level">[Optional] Notification level</param>
		void Send(string message, NotificationLevel level = NotificationLevel.Information);

		/// <summary>
		/// Send a notification message
		/// </summary>
		/// <param name="msg">The message to send as a notification</param>
		void Send(IMsg msg);
	}
}
