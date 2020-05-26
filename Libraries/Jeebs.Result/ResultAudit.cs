using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public abstract partial class R<T>
	{
		/// <summary>
		/// Audit the current result state and return unmodified
		/// </summary>
		/// <param name="audit">Audit action</param>
		public R<T> Audit(Action<R<T>> audit)
		{
			audit(this);
			return this;
		}

		/// <summary>
		/// Audit the current result state and return unmodified
		/// </summary>
		/// <param name="success">[Optional] Action to run if the current result is Ok</param>
		/// <param name="failure">[Optional] Action to run if the current result is Error</param>
		public R<T> AuditSwitch(Action<Ok<T>>? success = null, Action<Error<T>>? failure = null)
		{
			Action audit = this switch
			{
				Ok<T> ok => () => success?.Invoke(ok),
				Error<T> error => () => failure?.Invoke(error),
				_ => () => throw new InvalidOperationException()
			};

			audit();

			return this;
		}

		/// <summary>
		/// Audit the current result state and return unmodified
		/// </summary>
		/// <param name="success">[Optional] Action to run if the current result is OkV</param>
		/// <param name="failure">[Optional] Action to run if the current result is Error</param>
		public R<T> AuditSwitch(Action<OkV<T>>? success = null, Action<Error<T>>? failure = null)
		{
			Action audit = this switch
			{
				OkV<T> ok => () => success?.Invoke(ok),
				Error<T> error => () => failure?.Invoke(error),
				_ => () => throw new InvalidOperationException()
			};

			audit();

			return this;
		}
	}
}
