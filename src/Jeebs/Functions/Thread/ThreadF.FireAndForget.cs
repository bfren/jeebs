// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;

namespace Jeebs.Functions;

public static partial class ThreadF
{
	/// <summary>
	/// Start a task and forget about it.
	/// </summary>
	/// <param name="task">The task be anything you want - doesn't matter.</param>
	public static void FireAndForget(Func<Task> task) =>
		_ = Task.Run(task);

	/// <summary>
	/// Start a task and forget about it.
	/// </summary>
	/// <param name="task">The task can be sync or async - doesn't matter.</param>
	/// <param name="onError">Function to handle errors.</param>
	public static void FireAndForget(Func<Task> task, Action<Exception> onError) =>
		_ = Task.Run(async () =>
		{
			try
			{
				await task();
			}
			catch (Exception ex)
			{
				onError(ex);
			}
		});
}
