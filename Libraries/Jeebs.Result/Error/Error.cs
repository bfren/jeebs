using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <inheritdoc cref="IError{TResult}"/>
	public class Error<TResult> : R<TResult>, IError<TResult>
	{
		/// <summary>
		/// Error value.
		/// </summary>
		public override bool Val => false;

		internal Error() { }
	}
}
