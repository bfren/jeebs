using System;
using System.Collections.Generic;
using System.Linq;
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
		/// Not Found failure - simple
		/// </summary>
		/// <param name="errors">List of errors - MUST contain at least one</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static IResult<bool> NotFound(params string[] errors) => new NotFound<bool>(errors);

		/// <inheritdoc cref="NotFound(string[])"/>
		public static async Task<IResult<bool>> NotFoundAsync(params string[] errors) => await Task.FromResult(NotFound(errors));

		/// <summary>
		/// Not Found failure - simple
		/// </summary>
		/// <param name="err">IErrorList</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static IResult<bool> NotFound(IErrorList err) => new NotFound<bool>(err);

		/// <inheritdoc cref="NotFound(IErrorList)"/>
		public static async Task<IResult<bool>> NotFoundAsync(IErrorList err) => await Task.FromResult(NotFound(err));

		/// <summary>
		/// Not Found failure - with result type
		/// </summary>
		/// <typeparam name="T">Result success value type</typeparam>
		/// <param name="errors">List of errors - MUST contain at least one</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static IResult<T> NotFound<T>(params string[] errors) => new Failure<T>(errors);

		/// <inheritdoc cref="NotFound{T}(string[])"/>
		public static async Task<IResult<T>> NotFoundAsync<T>(params string[] errors) => await Task.FromResult(NotFound<T>(errors));

		/// <summary>
		/// Not Found failure - with result type
		/// </summary>
		/// <typeparam name="T">Result success value type</typeparam>
		/// <param name="err">IErrorList</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static IResult<T> NotFound<T>(IErrorList err) => new NotFound<T>(err);

		/// <inheritdoc cref="NotFound(IErrorList)"/>
		public static async Task<IResult<T>> NotFoundAsync<T>(IErrorList err) => await Task.FromResult(NotFound<T>(err));
	}

	/// <summary>
	/// Alias for Not Found failure
	/// </summary>
	public sealed class NotFound<T> : Result<T>
	{
		/// <summary>
		/// Create object with errors, and mark 'NotFound' as true
		/// </summary>
		/// <param name="errors">List of errors</param>
		internal NotFound(string[] errors) : base(errors.Length == 0 ? new[] { "Not found." } : errors, true) { }

		/// <summary>
		/// Create object with errors, and mark 'NotFound' as true
		/// </summary>
		/// <param name="err">IErrorList</param>
		internal NotFound(IErrorList err) : base(new ErrorList(err.ToArray()) { NotFound = true }) { }
	}
}
