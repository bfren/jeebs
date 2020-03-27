using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
		/// <param name="errors">List of errors - MUST contain at least one</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static async Task<Failure> FailureAsync(string[] errors) => await Task.FromResult(Failure(errors));

		/// <summary>
		/// Simple failure
		/// </summary>
		/// <param name="err">ErrorList</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static Failure Failure(ErrorList err) => new Failure(err);

		/// <summary>
		/// Simple failure
		/// </summary>
		/// <param name="err">ErrorList</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static async Task<Failure> FailureAsync(ErrorList err) => await Task.FromResult(Failure(err));

		/// <summary>
		/// Simple failure
		/// </summary>
		/// <param name="error">Single error</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static Failure Failure(string error) => Failure(new[] { error });

		/// <summary>
		/// Simple failure
		/// </summary>
		/// <param name="error">Single error</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static async Task<Failure> FailureAsync(string error) => await Task.FromResult(Failure(error));

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
		/// <param name="errors">List of errors - MUST contain at least one</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static async Task<Failure<T>> FailureAsync<T>(string[] errors) => await Task.FromResult(Failure<T>(errors));

		/// <summary>
		/// Feailure result
		/// </summary>
		/// <typeparam name="T">Result success value type</typeparam>
		/// <param name="err">ErrorList</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static Failure<T> Failure<T>(ErrorList err) => new Failure<T>(err);

		/// <summary>
		/// Feailure result
		/// </summary>
		/// <typeparam name="T">Result success value type</typeparam>
		/// <param name="err">ErrorList</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static async Task<Failure<T>> FailureAsync<T>(ErrorList err) => await Task.FromResult(Failure<T>(err));

		/// <summary>
		/// Feailure result
		/// </summary>
		/// <typeparam name="T">Result success value type</typeparam>
		/// <param name="error">Single error</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static Failure<T> Failure<T>(string error) => Failure<T>(new[] { error });

		/// <summary>
		/// Feailure result
		/// </summary>
		/// <typeparam name="T">Result success value type</typeparam>
		/// <param name="error">Single error</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static async Task<Failure<T>> FailureAsync<T>(string error) => await Task.FromResult(Failure<T>(error));
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

		/// <summary>
		/// Create object with errors
		/// </summary>
		/// <param name="err">ErrorList</param>
		internal Failure(ErrorList err) : base(err) { }
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

		/// <summary>
		/// Create object with errors
		/// </summary>
		/// <param name="err">ErrorList</param>
		internal Failure(ErrorList err) : base(err) { }
	}
}
