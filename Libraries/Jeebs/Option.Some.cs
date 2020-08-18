using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// 'Some' option - wraps value to enable safe non-null returns (see <seealso cref="None{T}"/>)
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public sealed class Some<T> : Option<T>
	{
		/// <summary>
		/// Option Value - nullability will match the nullability of <typeparamref name="T"/>
		/// </summary>
		public T Value { get; }

		internal Some(T value)
			=> Value = value;
	}
}
