using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// <para>Simple structure for safely performing operations and returning a successful result, or a list of errors</para>
	/// <para>Shows that the operation completed successfully</para>
	/// </summary>
	/// <typeparam name="T">Type of return object</typeparam>
	public class Success<T> : IResult<T>
	{
		/// <summary>
		/// Optional message to display
		/// </summary>
		public string Message { get; }

		/// <summary>
		/// Value of returned object
		/// </summary>
		public T Value { get; }

		/// <summary>
		/// Create object from value
		/// </summary>
		/// <param name="value">Value to return</param>
		public Success(in T value) : this(value?.ToString() ?? typeof(T).ToString(), value) { }

		/// <summary>
		/// Create object from value with optional message to display
		/// </summary>
		/// <param name="message">Message to display</param>
		/// <param name="value">Value to return</param>
		public Success(in string message, in T value)
		{
			Message = message;
			Value = value;
		}
	}

	/// <summary>
	/// <para>Simple structure for safely performing operations and returning a successful result, or a list of errors</para>
	/// <para>Special case - to be used instead of a return type of 'bool' for success / failure</para>
	/// </summary>
	public sealed class Success : Success<bool>, IResult
	{
		/// <summary>
		/// Create 'true' result
		/// </summary>
		public Success() : base(true) { }
	}
}