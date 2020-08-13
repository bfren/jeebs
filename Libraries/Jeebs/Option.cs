using System;
using System.Collections;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Create option types
	/// </summary>
	public static class Option
	{
		/// <summary>
		/// Create a Some option, containing a value
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="value">Some value</param>
		public static Some<T> Some<T>(T value)
			=> new Some<T>(value);

		/// <summary>
		/// Create a None option
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		public static None<T> None<T>()
			=> new None<T>();

		internal static None<T> None<T>(IMsg? reason)
			=> new None<T>(reason);

		/// <summary>
		/// Wrap <paramref name="value"/> in <see cref="Some{T}(T)"/> if <paramref name="predicate"/> is true
		/// <para>Otherwise, will return <see cref="None{T}"/></para>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="predicate">Predicate to evaluate</param>
		/// <param name="value">Function to return value</param>
		public static Option<T> WrapIf<T>(Func<bool> predicate, Func<T> value)
			=> predicate() switch
			{
				true => Some(value()),
				false => None<T>()
			};
	}

	/// <summary>
	/// Option type - enables null-safe returning by wrapping value in <see cref="Some{T}"/> and null in <see cref="None{T}"/>
	/// </summary>
	/// <typeparam name="T">Option value type</typeparam>
	public abstract class Option<T> : IEquatable<Option<T>>, IStructuralEquatable
	{
		internal Option() { }

		private U Switch<U>(Func<T, U> some, Func<IMsg?, U> none)
			=> this switch
			{
				Some<T> x => some(x.Value),
				None<T> y => none(y.Reason),
				_ => throw new Exception() // as Option<T> is internal implementation only this should never happen...
			};

		private U Switch<U>(Func<T, U> some, Func<U> none)
			=> Switch(some, _ => none());

		/// <summary>
		/// Run a function depending on whether this is a <see cref="Some{T}"/> or <see cref="None{T}"/>
		/// </summary>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="None{T}"/></param>
		public U Match<U>(Func<T, U> some, Func<U> none)
			=> Switch(
				some: some,
				none: none
			);

		/// <summary>
		/// Run a function depending if this is a <see cref="Some{T}"/> or return value <paramref name="none"/>
		/// </summary>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Value to return if <see cref="None{T}"/></param>
		public U Match<U>(Func<T, U> some, U none)
			=> Switch(
				some: some,
				none: () => none
			);

		/// <summary>
		/// Use <paramref name="map"/> to convert the current Option to a new type - if this is a <see cref="Some{T}"/>
		/// </summary>
		/// <typeparam name="U">Next Option value type</typeparam>
		/// <param name="map">Mapping function - will receive <see cref="Some{T}.Value"/> if this is a <see cref="Some{T}"/></param>
		public Option<U> Bind<U>(Func<T, Option<U>> map)
			=> Switch(
				some: x => map(x).Switch<Option<U>>(
					some: Option.Some,
					none: r => Option.None<U>(r)
				),
				none: r => Option.None<U>(r)
			);

		/// <summary>
		/// Use <paramref name="map"/> to convert the current Option to a new type - if this is a <see cref="Some{T}"/>
		/// </summary>
		/// <typeparam name="U">Next Option value type</typeparam>
		/// <param name="map">Mapping function - will receive <see cref="Some{T}.Value"/> if this is a <see cref="Some{T}"/></param>
		public Option<U> Map<U>(Func<T, U> map)
			=> Switch<Option<U>>(
				some: v => Option.Some(map(v)),
				none: r => Option.None<U>(r)
			);

		/// <summary>
		/// Unwrap the value of this option - if this is a <see cref="Some{T}"/>
		/// </summary>
		/// <param name="ifNone">Function to return <typeparamref name="T"/> if this is a <see cref="None{T}"/></param>
		public T Unwrap(Func<T> ifNone)
			=> Switch(
				some: x => x,
				none: ifNone
			);

		/// <summary>
		/// Alias for <see cref="Option.Some{T}(T)"/>
		/// </summary>
		/// <param name="value">Value to wrap</param>
		public Option<T> Return(T value)
			=> Option.Some(value);

		/// <summary>
		/// Alias for <see cref="Option.Some{T}(T)"/>
		/// </summary>
		/// <param name="value">Value to wrap</param>
		public Option<T> Wrap(T value)
			=> Option.Some(value);

		#region Operators

		/// <summary>
		/// Wrap a value in a <see cref="Some{T}"/>
		/// </summary>
		/// <param name="value">Value</param>
		public static implicit operator Option<T>(T value)
			=> value switch
			{
				T x => Option.Some(x),
				_ => Option.None<T>()
			};

		/// <summary>
		/// Compare an option type with a value type
		/// <para>If <paramref name="l"/> is a <see cref="Some{T}"/> the <see cref="Some{T}.Value"/> will be compared to <paramref name="r"/></para>
		/// </summary>
		/// <param name="l">Option</param>
		/// <param name="r">Value</param>
		public static bool operator ==(Option<T> l, T r)
			=> l.Switch(
				some: x => Equals(x, r),
				none: () => false
			);

		/// <summary>
		/// Compare an option type with a value type
		/// <para>If <paramref name="l"/> is a <see cref="Some{T}"/> the <see cref="Some{T}.Value"/> will be compared to <paramref name="r"/></para>
		/// </summary>
		/// <param name="l">Option</param>
		/// <param name="r">Value</param>
		public static bool operator !=(Option<T> l, T r)
			=> l.Switch(
				some: x => !Equals(x, r),
				none: () => true
			);

		/// <summary>
		/// Compare this <see cref="Option{T}"/> with another object
		/// <para>If both are a <see cref="Some{T}"/> each <see cref="Some{T}.Value"/> will be compared</para>
		/// <para>If both are a <see cref="None{T}"/> this will return true</para>
		/// <para>Otherwise this will return false</para>
		/// </summary>
		/// <param name="other">Object to compare to this <see cref="Option{T}"/></param>
		public override bool Equals(object other)
			=> other switch
			{
				Option<T> x => x.Equals(this),
				_ => false
			};

		/// <summary>
		/// Compare this <see cref="Option{T}"/> with another <see cref="Option{T}"/>
		/// <para>If both are a <see cref="Some{T}"/> each <see cref="Some{T}.Value"/> will be compared</para>
		/// <para>If both are a <see cref="None{T}"/> this will return true</para>
		/// <para>Otherwise this will return false</para>
		/// </summary>
		/// <param name="other"><see cref="Option{T}"/> to compare to this <see cref="Option{T}"/></param>
		public bool Equals(Option<T> other)
			=> other switch
			{
				Some<T> x when this is Some<T> y => Equals(x.Value, y.Value),
				None<T> _ when this is None<T> _ => true,
				_ => false
			};

		/// <summary>
		/// Compare this <see cref="Option{T}"/> with another <see cref="Option{T}"/>
		/// <para>If both are a <see cref="Some{T}"/> each <see cref="Some{T}.Value"/> will be compared</para>
		/// <para>If both are a <see cref="None{T}"/> this will return true</para>
		/// <para>Otherwise this will return false</para>
		/// </summary>
		/// <param name="other"><see cref="Option{T}"/> to compare to this <see cref="Option{T}"/></param>
		/// <param name="comparer">Equality Comparer</param>
		public bool Equals(object other, IEqualityComparer comparer)
			=> other switch
			{
				Some<T> x when this is Some<T> y => comparer.Equals(x.Value, y.Value),
				None<T> _ when this is None<T> _ => true,
				_ => false
			};

		/// <summary>
		/// Generate custom HashCode
		/// </summary>
		public override int GetHashCode()
			=> this switch
			{
				Some<T> x when x.Value is T y => typeof(Some<>).GetHashCode() ^ y.GetHashCode(),
				None<T> _ => typeof(None<>).GetHashCode() ^ typeof(T).GetHashCode(),
				_ => throw new Exception() // as Option<T> is internal implementation only this should never happen...
			};

		/// <inheritdoc cref="GetHashCode()"/>
		public int GetHashCode(IEqualityComparer comparer)
			=> this switch
			{
				Some<T> x when x.Value is T y => typeof(Some<>).GetHashCode() ^ comparer.GetHashCode(y),
				None<T> _ => typeof(None<>).GetHashCode() ^ typeof(T).GetHashCode(),
				_ => throw new Exception() // as Option<T> is internal implementation only this should never happen...
			};

		#endregion
	}
}
