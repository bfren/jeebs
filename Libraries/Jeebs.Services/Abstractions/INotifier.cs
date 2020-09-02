using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Constants;

namespace Jeebs
{
	/// <summary>
	/// Handles notifications - like logging, but bypasses the minimum level filters
	/// </summary>
	public interface INotifier
	{
		/// <summary>
		/// Send a notification message
		/// </summary>
		/// <param name="message"></param>
		void Send(string message);

		/// <summary>
		/// Send a notification message
		/// </summary>
		/// <param name="msg">The message to send as a notification</param>
		void Send(IMsg msg);
	}
}
