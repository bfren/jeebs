﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs;

/// <summary>
/// Used to make a service listen for notifications
/// </summary>
public interface INotificationListener
{
	/// <inheritdoc cref="INotifier.Send(string, NotificationLevel)"/>
	void Send(string message);

	/// <inheritdoc cref="INotifier.Send(string, NotificationLevel)"/>
	void Send(string message, NotificationLevel level);

	/// <inheritdoc cref="INotifier.Send(Msg)"/>
	public void Send(Msg msg);
}
