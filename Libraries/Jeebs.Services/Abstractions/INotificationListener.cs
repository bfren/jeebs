using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Used to make a service listen for notifications
	/// </summary>
	public interface INotificationListener
	{
		/// <summary>
		/// Send a notification message
		/// </summary>
		/// <param name="message"></param>
		void Send(string message);

		/// <summary>
		/// Send an IMsg
		/// </summary>
		/// <param name="msg"></param>
		public void Send(IMsg msg);
	}
}
