using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm.Link.Unwrap
{
	/// <summary>
	/// Used in <see cref="ILink{TValue}.UnwrapSingle{TSingle}"/> when the value type is not an <see cref="IEnumerable{T}"/>
	/// </summary>
	public sealed class NotIEnumerableMsg : IMsg { }
}
