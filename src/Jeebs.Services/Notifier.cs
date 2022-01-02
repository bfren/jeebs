// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;

namespace Jeebs;

/// <inheritdoc/>
public sealed class Notifier : INotifier
{
	private readonly IEnumerable<INotificationListener> listeners;

	/// <summary>
	/// Create using the specified listeners
	/// </summary>
	/// <param name="listeners"></param>
	public Notifier(IEnumerable<INotificationListener> listeners) =>
		this.listeners = listeners;

	/// <inheritdoc/>
	public void Send(string message) =>
		Send(message, NotificationLevel.Information);

	/// <inheritdoc/>
	public void Send(string message, NotificationLevel level)
	{
		foreach (var listener in listeners)
		{
			listener.Send(message, level);
		}
	}

	/// <inheritdoc/>
	public void Send(Msg msg)
	{
		foreach (var listener in listeners)
		{
			listener.Send(msg);
		}
	}
}
