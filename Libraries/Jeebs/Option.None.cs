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
		/// <summary>
		/// Sometimes a reason for the 'None' value may be set
		/// </summary>
		public string? Reason { get; private set; }

		internal None() { }

		internal void AddReason(string reason)
			=> Reason = reason;
	}
}