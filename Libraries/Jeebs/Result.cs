using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <inheritdoc cref="IResult{T}"/>
	public abstract class Result<T> : IResult<T>
	{
		/// <summary>
		/// Field to hold success value
		/// </summary>
		private readonly T value;

		/// <inheritdoc/>
		public T Val
		{
			get
			{
				if (value is T) // Correct access
				{
					return value;
				}
				else if (Err is IErrorList) // Val has been accessed
				{
					throw new Jx.ResultException("Result value has not been set and there are errors - " +
						"use `Result.Err is IErrorList` before accessing Result.Val.", Err);
				}
				else // we should never get here!
				{
					throw new NullReferenceException("Result value is null, and no errors have been set.");
				}
			}
		}

		/// <inheritdoc/>
		public IErrorList? Err { get; }

		/// <summary>
		/// Create success result using specified value
		/// </summary>
		/// <param name="value">Success value</param>
		protected Result(T value) => this.value = value;

		/// <inheritdoc/>
		public IResult<TNew> Pipe<TNew>(Func<T, IResult<TNew>> next)
		{
			if(Err is IErrorList err)
			{
				return Result.Failure<TNew>(err);
			}

			return next(Val);
		}

		/// <summary>
		/// Return Err (IErrorList) if there was an error, or Val (T) if not
		/// </summary>
		public override string ToString()
		{
			return
				Err is IErrorList
				? Err.ToString()
				: (
					Val is T
					? Val.ToString()
					: "Unknown result."
				)
			;
		}

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

		/// <summary>
		/// Create failure result using specified errors
		/// </summary>
		/// <param name="errors">List of errors - MUST contain at least one</param>
		/// <param name="notFound">Set to true to mark this as a 'Not Found' error</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		protected Result(string[] errors, bool notFound = false)
		{
			if(errors.Length == 0)
			{
				throw new Jx.ResultException("You must pass at least one error.");
			}

			Err = new ErrorList(errors) { NotFound = notFound };
		}

		/// <summary>
		/// Create failure result using specified errors
		/// </summary>
		/// <param name="err">IErrorList</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		protected Result(IErrorList err) => Err = err;

#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
	}
}
