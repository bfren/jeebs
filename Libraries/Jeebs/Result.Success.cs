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
		/// Simple Success
		/// </summary>
		public static Success Success() => new Success();

		/// <summary>
		/// Simple Success
		/// </summary>
		public static async Task<Success> SuccessAsync() => await Task.FromResult(Success());

		/// <summary>
		/// Success result
		/// </summary>
		/// <typeparam name="T">Result success value type</typeparam>
		/// <param name="value">Success value</param>
		public static Success<T> Success<T>(T value) => new Success<T>(value);

		/// <summary>
		/// Success result
		/// </summary>
		/// <typeparam name="T">Result success value type</typeparam>
		/// <param name="value">Success value</param>
		public static async Task<Success<T>> SuccessAsync<T>(T value) => await Task.FromResult(Success(value));
	}

	/// <summary>
	/// Alias for simple success
	/// </summary>
	public sealed class Success : Result
	{
		/// <summary>
		/// Create object with true value
		/// </summary>
		internal Success() : base(true) { }
	}

	/// <summary>
	/// Alias for success
	/// </summary>
	/// <typeparam name="T">Success value type</typeparam>
	public sealed class Success<T> : Result<T>
	{
		/// <summary>
		/// Create object with specified value
		/// </summary>
		/// <param name="value">Success value</param>
		internal Success(T value) : base(value) { }
	}
}
