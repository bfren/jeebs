using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public abstract partial class R<TResult, TState>
	{
		/// <inheritdoc/>
		public IR<TResult, TState> Audit(Action<IR<TResult, TState>> audit)
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

		/// <inheritdoc/>
		public IR<TResult, TState> AuditSwitch(
			Action<IOk<TResult, TState>>? isOk = null,
			Action<IOkV<TResult, TState>>? isOkV = null,
			Action<IError<TResult, TState>>? isError = null,
			Action? isUnknown = null
		)
		{
			if (isOk == null && isOkV == null && isError == null && isUnknown == null)
			{
				return this;
			}

			Action audit = this switch
			{
				IError<TResult, TState> error => () => isError?.Invoke(error),
				IOkV<TResult, TState> okV => () => isOkV?.Invoke(okV),
				IOk<TResult, TState> ok => () => isOk?.Invoke(ok),
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
