// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <summary>
		/// Wrap a value in a <see cref="Some{T}"/>
		/// </summary>
		/// <param name="value">Value</param>
		public static implicit operator Option<T>(T value) =>
			Option.Wrap(value);

		/// <summary>
		/// Compare an option type with a value type
		/// <para>If <paramref name="l"/> is a <see cref="Some{T}"/> the <see cref="Some{T}.Value"/> will be compared to <paramref name="r"/></para>
		/// </summary>
		/// <param name="l">Option</param>
		/// <param name="r">Value</param>
		public static bool operator ==(Option<T> l, T r) =>
			l.Switch(
				some: v => Equals(v, r),
				none: () => false
			);

		/// <summary>
		/// Compare an option type with a value type
		/// <para>If <paramref name="l"/> is a <see cref="Some{T}"/> the <see cref="Some{T}.Value"/> will be compared to <paramref name="r"/></para>
		/// </summary>
		/// <param name="l">Option</param>
		/// <param name="r">Value</param>
		public static bool operator !=(Option<T> l, T r) =>
			l.Switch(
				some: v => !Equals(v, r),
				none: () => true
			);
	}
}
