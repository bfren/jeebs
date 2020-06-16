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
		/// </summary>
		/// <param name="success">[Optional] Action to run if the current result is Ok</param>
		/// <param name="failure">[Optional] Action to run if the current result is Error</param>
		public R<T> AuditSwitch<TOk>(Action<TOk>? success = null, Action<Error<T>>? failure = null)
			where TOk : Ok<T>
		{
			if (success == null && failure == null)
			{
				Messages.Add<Jm.AuditActionMissingError>();
				return this;
			}

			Action audit = this switch
			{
				TOk ok => () => success?.Invoke(ok),
				Error<T> error => () => failure?.Invoke(error),
				_ => () => throw new InvalidOperationException("Unknown R<> type.")
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
