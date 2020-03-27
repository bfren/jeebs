using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Factory methods for creating Results
	/// </summary>
	public partial class Result
	{
		/// <summary>
		/// Simple failure
		/// </summary>
		/// <param name="errors">List of errors - MUST contain at least one</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static Failure Failure(string[] errors) => new Failure(errors);

		/// <summary>
		/// Simple failure
		/// </summary>
		/// <param name="error">Single error</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static Failure Failure(string error) => Failure(new[] { error });

		/// <summary>
		/// Feailure result
		/// </summary>
		/// <typeparam name="T">Result success value type</typeparam>
		/// <param name="errors">List of errors - MUST contain at least one</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static Failure<T> Failure<T>(string[] errors) => new Failure<T>(errors);

		/// <summary>
		/// Feailure result
		/// </summary>
		/// <typeparam name="T">Result success value type</typeparam>
		/// <param name="error">Single error</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static Failure<T> Failure<T>(string error) => Failure<T>(new[] { error });
	}

	/// <summary>
	/// Alias for simple failure
	/// </summary>
	public sealed class Failure : Result
	{
		/// <summary>
		/// Create object with errors
		/// </summary>
		/// <param name="errors">List of errors</param>
		internal Failure(string[] errors) : base(errors) { }
	}

	/// <summary>
	/// Alias for failure
	/// </summary>
	/// <typeparam name="T">Success value type</typeparam>
	public sealed class Failure<T> : Result<T>
	{
		/// <summary>
		/// Create object with errors
		/// </summary>
		/// <param name="errors">List of errors</param>
		internal Failure(string[] errors) : base(errors) { }
	}
}
