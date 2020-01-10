using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// DbResult
	/// </summary>
	public static partial class DbResult
	{
		/// <summary>
		/// Simple structure for safely performing operations and returning a successful result, or a list of errors
		/// Shows that the operation completed successfully
		/// </summary>
		/// <typeparam name="T">Type of return object</typeparam>
		public class DbSuccess<T> : Success<T>, IDbResult<T>
		{
			/// <summary>
			/// Create object from value
			/// </summary>
			/// <param name="value">Value to return</param>
			internal DbSuccess(in T value) : base(value) { }

			/// <summary>
			/// Create object from value with optional message to display
			/// </summary>
			/// <param name="message">Message to display</param>
			/// <param name="value">Value to return</param>
			internal DbSuccess(in string message, in T value) : base(message, value) { }
		}

		/// <summary>
		/// Simple structure for safely performing operations and returning a successful result, or a list of errors
		/// Special case - to be used instead of a return type of 'bool' for success / failure
		/// </summary>
		public sealed class DbSuccess : DbSuccess<bool>, IDbResult
		{
			internal DbSuccess() : base(true) { }
		}
	}
}
