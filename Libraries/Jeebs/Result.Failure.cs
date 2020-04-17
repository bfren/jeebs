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
		public static IResult<bool> Failure(string[] errors) => new Failure(errors);

		/// <summary>
		/// Simple failure
		/// </summary>
		/// <param name="errors">List of errors - MUST contain at least one</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static async Task<IResult<bool>> FailureAsync(string[] errors) => await Task.FromResult(Failure(errors));

		/// <summary>
		/// Simple failure
		/// </summary>
		/// <param name="err">IErrorList</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static IResult<bool> Failure(IErrorList err) => new Failure(err);

		/// <summary>
		/// Simple failure
		/// </summary>
		/// <param name="err">IErrorList</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static async Task<IResult<bool>> FailureAsync(IErrorList err) => await Task.FromResult(Failure(err));

		/// <summary>
		/// Simple failure
		/// </summary>
		/// <param name="error">Single error</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static IResult<bool> Failure(string error) => Failure(new[] { error });

		/// <summary>
		/// Simple failure
		/// </summary>
		/// <param name="error">Single error</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static async Task<IResult<bool>> FailureAsync(string error) => await Task.FromResult(Failure(error));

		/// <summary>
		/// Feailure result
		/// </summary>
		/// <typeparam name="T">Result success value type</typeparam>
		/// <param name="errors">List of errors - MUST contain at least one</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static IResult<T> Failure<T>(string[] errors) => new Failure<T>(errors);

		/// <summary>
		/// Feailure result
		/// </summary>
		/// <typeparam name="T">Result success value type</typeparam>
		/// <param name="errors">List of errors - MUST contain at least one</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static async Task<IResult<T>> FailureAsync<T>(string[] errors) => await Task.FromResult(Failure<T>(errors));

		/// <summary>
		/// Feailure result
		/// </summary>
		/// <typeparam name="T">Result success value type</typeparam>
		/// <param name="err">IErrorList</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static IResult<T> Failure<T>(IErrorList err) => new Failure<T>(err);

		/// <summary>
		/// Feailure result
		/// </summary>
		/// <typeparam name="T">Result success value type</typeparam>
		/// <param name="err">IErrorList</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static async Task<IResult<T>> FailureAsync<T>(IErrorList err) => await Task.FromResult(Failure<T>(err));

		/// <summary>
		/// Feailure result
		/// </summary>
		/// <typeparam name="T">Result success value type</typeparam>
		/// <param name="error">Single error</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static IResult<T> Failure<T>(string error) => Failure<T>(new[] { error });

		/// <summary>
		/// Feailure result
		/// </summary>
		/// <typeparam name="T">Result success value type</typeparam>
		/// <param name="error">Single error</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static async Task<IResult<T>> FailureAsync<T>(string error) => await Task.FromResult(Failure<T>(error));
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
		/// <param name="err">IErrorList</param>
		internal Failure(IErrorList err) : base(err) { }
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
		/// <param name="err">IErrorList</param>
		internal Failure(IErrorList err) : base(err) { }
	}
}
