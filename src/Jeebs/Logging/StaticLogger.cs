// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Logging;

/// <summary>
/// Used to access logging functionality when dependency injection is not available.
/// </summary>
public static class StaticLogger
{
	/// <summary>
	/// Factory to create a static logger.
	/// </summary>
	public static Lazy<ILog> Factory { private get; set; } = new(new NullLogger());

	/// <summary>
	/// Create a new logger.
	/// </summary>
	public static ILog Log =>
		Factory.Value;

	/// <summary>
	/// Return a new log instance for a specific context.
	/// </summary>
	/// <typeparam name="T">Log context type.</typeparam>
	/// <returns>A new <see cref="ILog"/> for context specified by <typeparamref name="T"/>.</returns>
	public static ILog<T> ForContext<T>() =>
		Factory.Value.ForContext<T>();
}
