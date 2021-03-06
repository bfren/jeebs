// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

namespace Jeebs
{
	/// <inheritdoc cref="IError{TValue}"/>
	public class RError<TValue> : R<TValue>, IError<TValue>
	{
		internal RError() { }
	}
}
