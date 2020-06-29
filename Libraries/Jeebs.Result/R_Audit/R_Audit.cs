using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public abstract partial class R<TResult>
	{
		/// <inheritdoc/>
		public IR<TResult> Audit(Action<IR<TResult>> audit)
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
		public IR<TResult> AuditSwitch(
			Action<IOk<TResult>>? isOk = null,
			Action<IOkV<TResult>>? isOkV = null,
			Action<IError<TResult>>? isError = null,
			Action? isUnknown = null
		)
		{
			if (isOk == null && isOkV == null && isError == null && isUnknown == null)
			{
				return this;
			}

			Action audit = this switch
			{
				IError<TResult> error => () => isError?.Invoke(error),
				IOkV<TResult> okV => () => isOkV?.Invoke(okV),
				IOk<TResult> ok => () => isOk?.Invoke(ok),
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
