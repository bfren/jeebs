using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public abstract partial class R<T>
	{
		/// <summary>
		/// Audit the current result state and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditException"/> message</para>
		/// </summary>
		/// <param name="audit">Audit action</param>
		public R<T> Audit(Action<R<T>> audit)
		{
			try
			{
				audit(this);
			}
			catch (Exception ex)
			{
				Messages.Add(new Jm.AuditException(ex));
			}

			return this;
		}

		/// <summary>
		/// Audit the current result state and return unmodified
		/// <para>If no arguments  <see cref="Jm.AuditException"/> message</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditException"/> message</para>
		/// </summary>
		/// <param name="isOk">[Optional] Action to run if the current result is <see cref="Jeebs.Ok{T}"/></param>
		/// <param name="isOkV">[Optional] Action to run if the current result is <see cref="Jeebs.OkV{T}"/></param>
		/// <param name="isError">[Optional] Action to run if the current result is <see cref="Jeebs.Error{T}"/></param>
		/// <param name="isUnknown">[Optional] Action to run if the current result is unknown</param>
		public R<T> AuditSwitch(Action<Ok<T>>? isOk = null, Action<OkV<T>>? isOkV = null, Action<Error<T>>? isError = null, Action? isUnknown = null)
		{
			if (isOk == null && isOkV == null && isError == null && isUnknown == null)
			{
				return this;
			}

			Action audit = this switch
			{
				Error<T> error => () => isError?.Invoke(error),
				OkV<T> okV => () => isOkV?.Invoke(okV),
				Ok<T> ok => () => isOk?.Invoke(ok),
				_ => isUnknown ?? (() => throw new InvalidOperationException($"Unknown R<> subtype: '{GetType()}'."))
			};

			try
			{
				audit();
			}
			catch (Exception ex)
			{
				Messages.Add(new Jm.AuditException(ex));
			}

			return this;
		}
	}
}
