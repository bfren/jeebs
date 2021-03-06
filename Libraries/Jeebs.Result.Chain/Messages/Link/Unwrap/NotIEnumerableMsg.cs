// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System.Collections.Generic;
using Jeebs;

namespace Jm.Link.Unwrap
{
	/// <summary>
	/// Used in <see cref="ILink{TValue}.UnwrapSingle{TSingle}"/> when the value type is not an <see cref="IEnumerable{T}"/>
	/// </summary>
	public sealed class NotIEnumerableMsg : IMsg { }
}
