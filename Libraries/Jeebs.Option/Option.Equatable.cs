// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <summary>
		/// Compare this <see cref="Option{T}"/> with another object
		/// <para>If both are a <see cref="Some{T}"/> each <see cref="Some{T}.Value"/> will be compared</para>
		/// <para>If both are a <see cref="None{T}"/> this will return true</para>
		/// <para>Otherwise this will return false</para>
		/// </summary>
		/// <param name="other">Object to compare to this <see cref="Option{T}"/></param>
		public override bool Equals(object? other) =>
			other switch
			{
				Option<T> x =>
					Equals(x),

				_ =>
					false
			};

		/// <summary>
		/// Compare this <see cref="Option{T}"/> with another <see cref="Option{T}"/>
		/// <para>If both are a <see cref="Some{T}"/> each <see cref="Some{T}.Value"/> will be compared</para>
		/// <para>If both are a <see cref="None{T}"/> this will return true</para>
		/// <para>Otherwise this will return false</para>
		/// </summary>
		/// <param name="other"><see cref="Option{T}"/> to compare to this <see cref="Option{T}"/></param>
		public virtual bool Equals(Option<T>? other) =>
			this switch
			{
				Some<T> x when other is Some<T> y =>
					Equals(x.Value, y.Value),

				None<T> x when other is None<T> y =>
					Equals(x.Reason, y.Reason),

				_ =>
					false
			};

		/// <summary>
		/// Compare this <see cref="Option{T}"/> with another <see cref="Option{T}"/>
		/// <para>If both are a <see cref="Some{T}"/> each <see cref="Some{T}.Value"/> will be compared</para>
		/// <para>If both are a <see cref="None{T}"/> this will return true</para>
		/// <para>Otherwise this will return false</para>
		/// </summary>
		/// <param name="other"><see cref="Option{T}"/> to compare to this <see cref="Option{T}"/></param>
		/// <param name="comparer">Equality Comparer</param>
		public bool Equals(object? other, IEqualityComparer comparer) =>
			this switch
			{
				Some<T> x when other is Some<T> y =>
					comparer.Equals(x.Value, y.Value),

				None<T> x when other is None<T> y =>
					comparer.Equals(x.Reason, y.Reason),

				_ =>
					false
			};

		/// <summary>
		/// Generate custom HashCode
		/// </summary>
		public override int GetHashCode() =>
			this switch
			{
				Some<T> x when x.Value is T y =>
					typeof(Some<>).GetHashCode() ^ y.GetHashCode(),

				None<T> x when x.Reason is IMsg y =>
					typeof(None<>).GetHashCode() ^ y.GetHashCode(),

				None<T> _ =>
					typeof(None<>).GetHashCode() ^ typeof(T).GetHashCode(),

				_ =>
					throw new Jx.Option.UnknownOptionException() // as Option<T> is internal implementation only this should never happen...
			};

		/// <inheritdoc cref="GetHashCode()"/>
		public int GetHashCode(IEqualityComparer comparer) =>
			this switch
			{
				Some<T> x when x.Value is T y =>
					typeof(Some<>).GetHashCode() ^ comparer.GetHashCode(y),

				None<T> x when x.Reason is IMsg y =>
					typeof(None<>).GetHashCode() ^ comparer.GetHashCode(y),

				None<T> _ =>
					typeof(None<>).GetHashCode() ^ typeof(T).GetHashCode(),

				_ =>
					throw new Jx.Option.UnknownOptionException() // as Option<T> is internal implementation only this should never happen...
			};
	}
}
