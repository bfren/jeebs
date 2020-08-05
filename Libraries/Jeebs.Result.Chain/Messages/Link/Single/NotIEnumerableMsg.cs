using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm.Link.Single
{
	/// <summary>
	/// Used in <see cref="ILink{TValue}.Unwrap{TSingle}"/> when the value type is not an <see cref="IEnumerable{T}"/>
	/// </summary>
	public sealed class NotIEnumerableMsg : IMsg
	{
		/// <summary>
		/// Create Message
		/// </summary>
		public NotIEnumerableMsg() { }
	}
}
