// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Jeebs;

/// <summary>
/// Supports asynchronous <see cref="Lazy{T}"/> values.
/// </summary>
/// <typeparam name="T">Return value.</typeparam>
public sealed class LazyAsync<T>
{
	/// <summary>
	/// Get the awaitable task for this value (it will only be run the first time).
	/// </summary>
	public Task<T> Value =>
		task.Value;

	/// <summary>
	/// True once <see cref="Value"/> has been requested (the task has started, but may not have finished).
	/// </summary>
	public bool Initialised =>
		task.IsValueCreated;

	/// <summary>
	/// Await this object directly, instead of <see cref="Value"/>.
	/// </summary>
	public TaskAwaiter<T> GetAwaiter() =>
		Value.GetAwaiter();

	private readonly Lazy<Task<T>> task;

	/// <summary>
	/// Create a LazyAsync object with a task.
	/// </summary>
	/// <param name="task">Task to get value.</param>
	public LazyAsync(Task<T> task) =>
		this.task = new(() => task, true);

	/// <summary>
	/// Create a LazyAsync object with a function that returns a task.
	/// </summary>
	/// <param name="f">Awaitable function to get value.</param>
	public LazyAsync(Func<Task<T>> f) =>
		task = new(f, true);
}
