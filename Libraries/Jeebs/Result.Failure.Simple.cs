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

		/// <inheritdoc cref="Failure(string[])"/>
		public static async Task<IResult<bool>> FailureAsync(string[] errors) => await Task.FromResult(Failure(errors));

		/// <summary>
		/// Simple failure
		/// </summary>
		/// <param name="err">IErrorList</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static IResult<bool> Failure(IErrorList err) => new Failure(err);

		/// <inheritdoc cref="Failure(IErrorList)"/>
		public static async Task<IResult<bool>> FailureAsync(IErrorList err) => await Task.FromResult(Failure(err));

		/// <summary>
		/// Simple failure
		/// </summary>
		/// <param name="error">Single error</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static IResult<bool> Failure(string error) => Failure(new[] { error });

		/// <inheritdoc cref="Failure(string)"/>
		public static async Task<IResult<bool>> FailureAsync(string error) => await Task.FromResult(Failure(error));
	}

	/// <summary>
	/// Alias for simple failure
	/// </summary>
	public sealed class Failure : Result<bool>
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
