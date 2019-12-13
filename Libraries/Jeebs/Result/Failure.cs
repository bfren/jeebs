using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Simple structure for safely performing operations and returning a successful result, or a list of errors
	/// Shows that the operation resulted in one or more error(s)
	/// </summary>
	public class Failure<T> : List<string>, IResult<T>
	{
		/// <summary>
		/// Returns the list of errors as a newline-separated string
		/// </summary>
		public string Message { get => string.Join("\n", this); }

		/// <summary>
		/// Create from a predefined list
		/// </summary>
		/// <param name="errors">List of errors</param>
		public Failure(in List<string> errors) => AddRange(errors as IEnumerable<string>);

		/// <summary>
		/// Create from a parameterised array
		/// </summary>
		/// <param name="errors">Array of errors</param>
		public Failure(params string[] errors) => AddRange(errors);

		/// <summary>
		/// Output Message when ToString() is called
		/// </summary>
		public override string ToString() => Message;
	}

	/// <summary>
	/// Simple structure for safely performing operations and returning a successful result, or a list of errors
	/// Special case - to be used instead of a return type of 'bool' for success / failure
	/// </summary>
	public sealed class Failure : Failure<bool>, IResult
	{
		/// <summary>
		/// Create from a predefined list
		/// </summary>
		/// <param name="errors">List of errors</param>
		public Failure(in List<string> errors) => AddRange(errors as IEnumerable<string>);

		/// <summary>
		/// Create from a parameterised array
		/// </summary>
		/// <param name="errors">Array of errors</param>
		public Failure(params string[] errors) => AddRange(errors);
	}
}
