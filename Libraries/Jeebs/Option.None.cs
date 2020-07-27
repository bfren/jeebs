using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// 'None' option - used to replace null returns (see <seealso cref="Some{T}"/>)
	/// </summary>
	/// <typeparam name="T">Option value type</typeparam>
	public sealed class None<T> : Option<T>
	{

	}
}