// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;

namespace Jeebs
{
	/// <summary>
	/// Option Equality Comparer
	/// </summary>
	public sealed class OptionEqualityComparer<T> : IEqualityComparer<Option<T>>
	{
		/// <summary>
		/// Compare two <see cref="Option{T}"/> objects
		/// <para>If both are a <see cref="Some{T}"/> each <see cref="Some{T}.Value"/> will be compared</para>
		/// <para>If both are a <see cref="None{T}"/> this will return true</para>
		/// <para>Otherwise this will return false</para>
		/// </summary>
		/// <param name="x">First Option</param>
		/// <param name="y">Second Option</param>
		public bool Equals(Option<T>? x, Option<T>? y) =>
			x switch
			{
				Some<T> a when y is Some<T> b =>
					Equals(objA: a.Value, objB: b.Value),

				None<T> a when y is None<T> b =>
					Equals(objA: a.Reason, objB: b.Reason),

				_ =>
					false
			};

		/// <inheritdoc cref="Option{T}.GetHashCode"/>
		public int GetHashCode(Option<T> obj) =>
			obj.GetHashCode();
	}
}
