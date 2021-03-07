// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs
{
	/// <summary>
	/// Extension methods for <see cref="IR"/> interface: UseLog
	/// </summary>
	public static class RExtensions_UseLog
	{
		/// <summary>
		/// Use the specified log for the current result
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="log">ILog</param>
		public static TResult UseLog<TResult>(this TResult @this, ILog log)
			where TResult : IR
		{
			if (@this.Logger is Logger logger)
			{
				logger.log = log;
			}

			return @this;
		}
	}
}
