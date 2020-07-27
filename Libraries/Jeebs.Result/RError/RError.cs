using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <inheritdoc cref="IError{TValue}"/>
	public class RError<TValue> : R<TValue>, IError<TValue>
	{
		internal RError() { }
	}
}
