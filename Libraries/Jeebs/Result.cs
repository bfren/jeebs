using System;
using System.Collections.Generic;
using System.Text;


namespace Jeebs
{
	/// <summary>
	/// True / false result class
	/// </summary>
	public partial class Result : Result<bool>
	{
		/// <summary>
		/// Create success result using specified value
		/// </summary>
		/// <param name="value">Success value</param>
		public Result(bool value) : base(value) { }

		/// <summary>
		/// Create failure result using specified errors
		/// </summary>
		/// <param name="errors">List of errors - MUST contain at least one</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public Result(string[] errors) : base(errors) { }

		/// <summary>
		/// Create failure result using specified errors
		/// </summary>
		/// <param name="err">IErrorList</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public Result(IErrorList err) : base(err) { }
	}

	/// <inheritdoc cref="IResult{T}"/>
	public class Result<T> : IResult<T>
	{
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

		/// <summary>
		/// Field to hold success value
		/// </summary>
		private readonly T value;

		/// <inheritdoc/>
		public IErrorList? Err { get; }

		/// <summary>
		/// Create success result using specified value
		/// </summary>
		/// <param name="value">Success value</param>
		protected Result(T value) => this.value = value;

		/// <summary>
		/// Create failure result using specified errors
		/// </summary>
		/// <param name="errors">List of errors - MUST contain at least one</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
		protected Result(string[] errors)
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
		{
			if (errors.Length == 0)
			{
				throw new Jx.ResultException("You must pass at least one error.");
			}

			Err = new ErrorList(errors);
		}

		/// <summary>
		/// Create failure result using specified errors
		/// </summary>
		/// <param name="err">IErrorList</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
		protected Result(IErrorList err) => Err = err;
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

		/// <summary>
		/// Return Err (IErrorList) if there was an error, or Val (T) if not
		/// </summary>
		public override string ToString()
		{
			return
				Err is ErrorList
				? Err.ToString()
				: (
					Val is T
					? Val.ToString()
					: "Unknown result."
				)
			;
		}
	}
}
