using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Represents an error result
	/// </summary>
	/// <typeparam name="T">Result value type - not used in Error classes, only Ok classes have a value</typeparam>
	public class Error<T> : R<T>, IError<T>
	{
		/// <summary>
		/// Error value.
		/// </summary>
		public override bool Val => false;

		/// <summary>
		/// Only created in <see cref="R{T}.ErrorNew{TNext, TMessage}"/>.
		/// </summary>
		internal Error() { }
	}
}
