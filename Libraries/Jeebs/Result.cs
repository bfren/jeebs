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
		/// <param name="err">ErrorList</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
		public Result(ErrorList err) : base(err) { }
	}

	/// <summary>
	/// Result class
	/// </summary>
	/// <typeparam name="T">Result success value type</typeparam>
	public class Result<T>
	{
		/// <summary>
		/// Success value - you MUST use <code>Result.Err is ErrorList</code> (or similar) before accessing this
		/// </summary>
		/// <exception cref="Jx.ResultException">If value is not set and there are errors, InnerException will contain the ErrorList</exception>
		/// <exception cref="NullReferenceException">This should never happen - it means Val and Err are both null</exception>
		public T Val
		{
			get
			{
				if (value is T) // Correct access
				{
					return value;
				}
				else if (Err is ErrorList) // Val has been accessed
				{
					throw new Jx.ResultException("Result value has not been set and there are errors - " +
						"use `Result.Err is ErrorList` before accessing Result.Val.", Err);
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

		/// <summary>
		/// List of errors when the result is a failure
		/// Use <code>Result.Err is ErrorList</code> to check before accessing Result.Val
		/// </summary>
		public ErrorList? Err { get; }

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
		/// <param name="err">ErrorList</param>
		/// <exception cref="Jx.ResultException">If list contains no errors</exception>
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
		protected Result(ErrorList err) => Err = err;
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

		/// <summary>
		/// Return Err (ErrorList) if there was an error, or Val (T) if not
		/// </summary>
		public override string ToString() => Err is ErrorList ? Err.ToString() : (Val is T ? Val.ToString() : base.ToString());
	}
}
