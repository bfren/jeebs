﻿using System;
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
		/// Failure result
		/// </summary>
		/// <typeparam name="T">Result success value type</typeparam>
		/// <param name="errors">List of errors - MUST contain at least one</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static IResult<T> Failure<T>(string[] errors) => new Failure<T>(errors);

		/// <inheritdoc cref="Failure{T}(string[])"/>
		public static async Task<IResult<T>> FailureAsync<T>(string[] errors) => await Task.FromResult(Failure<T>(errors));

		/// <summary>
		/// Failure result
		/// </summary>
		/// <typeparam name="T">Result success value type</typeparam>
		/// <param name="err">IErrorList</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static IResult<T> Failure<T>(IErrorList err) => new Failure<T>(err);

		/// <inheritdoc cref="Failure(IErrorList)"/>
		public static async Task<IResult<T>> FailureAsync<T>(IErrorList err) => await Task.FromResult(Failure<T>(err));

		/// <summary>
		/// Failure result
		/// </summary>
		/// <typeparam name="T">Result success value type</typeparam>
		/// <param name="error">Single error</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public static IResult<T> Failure<T>(string error) => Failure<T>(new[] { error });

		/// <inheritdoc cref="Failure{T}(string)"/>
		public static async Task<IResult<T>> FailureAsync<T>(string error) => await Task.FromResult(Failure<T>(error));
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
