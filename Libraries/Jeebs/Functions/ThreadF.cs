using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

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
		/// <param name="task">ThreadStart can be sync or async - doesn't matter</param>
		public static void FireAndForget(ThreadStart task)
		{
			var thread = new Thread(task) { IsBackground = true };
			thread.Start();
		}
	}
}
