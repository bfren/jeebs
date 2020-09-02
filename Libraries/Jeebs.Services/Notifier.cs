using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Constants;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs
{
	/// <inheritdoc/>
	public sealed class Notifier : INotifier
	{
		private readonly IEnumerable<INotificationListener> listeners;

		/// <summary>
		/// Create using the specified listeners
		/// </summary>
		/// <param name="listeners"></param>
		public Notifier(IEnumerable<INotificationListener> listeners)
			=> this.listeners = listeners;

		/// <inheritdoc/>
		public void Send(string message)
		{
			foreach (var listener in listeners)
			{
				listener.Send(message);
			}
		}

		/// <inheritdoc/>
		public void Send(IMsg msg)
		{
			foreach (var listener in listeners)
			{
				listener.Send(msg);
			}
		}
	}
}
