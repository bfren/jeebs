using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Represents a simple error result
	/// </summary>
	public class Error : Error<object>
	{
		internal Error() { }
	}

	/// <summary>
	/// Represents an error result
	/// </summary>
	/// <typeparam name="T">Result value type - not used in Error classes, only Ok classes have a value</typeparam>
	public class Error<T> : R<T>
	{
		/// <summary>
		/// Only created in <see cref="R{T}.ErrorNew{TNext, TMessage}"/>.
		/// </summary>
		internal Error() { }
	}
}
