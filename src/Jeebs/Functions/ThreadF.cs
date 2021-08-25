﻿// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace F
{
	/// <summary>
	/// Thread functions
	/// </summary>
	public static class ThreadF
	{
		/// <summary>
		/// Start a task and forget about it
		/// </summary>
		/// <param name="task">The task be sync or async - doesn't matter</param>
		public static void FireAndForget(Action task) =>
			ThreadPool.QueueUserWorkItem(_ => task());

		/// <summary>
		/// Start a task and forget about it
		/// </summary>
		/// <typeparam name="T">Task state object</typeparam>
		/// <param name="state">State to pass to the task</param>
		/// <param name="task">The task be sync or async - doesn't matter</param>
		public static void FireAndForget<T>(T state, Action<T> task) =>
			ThreadPool.QueueUserWorkItem(task, state, false);
	}
}
