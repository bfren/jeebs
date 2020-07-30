using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

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
	}

	/// <summary>
	/// Option type - enables null-safe returning by wrapping value in <see cref="Some{T}"/> and null in <see cref="None{T}"/>
	/// </summary>
	/// <typeparam name="T">Option value type</typeparam>
	public abstract class Option<T> : IEquatable<Option<T>>, IStructuralEquatable
	{
		internal Option() { }

		/// <summary>
		/// True if this Option is <see cref="Some{T}"/>
		/// </summary>
		public bool IsSome
			=> this is Some<T>;

		/// <summary>
		/// True if this Option is <see cref="None{T}"/>
		/// </summary>
		public bool IsNone
			=> this is None<T>;

		public Option<U> Bind<U>(Func<T, Option<U>> map)
			=> Match(
				x => map(x).Match<Option<U>>(Option.Some, Option.None<U>),
				Option.None<U>
			);

		public Option<U> Map<U>(Func<T, U> map)
			=> Match<Option<U>>(
				v => Option.Some(map(v)),
				Option.None<U>
			);

		/// <summary>
		/// Run a function depending on whether this is a <see cref="Some{T}"/> or <see cref="None{T}"/>
		/// </summary>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="None{T}"/></param>
		public U Match<U>(Func<T, U> some, Func<U> none)
			=> this switch
			{
				Some<T> x => some(x.Value),
				None<T> _ => none(),
				_ => throw new Exception() // as Option<T> is internal implementation only this should never happen...
			};

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

		/// <summary>
		/// Get the value of this option
		/// </summary>
		/// <param name="ifNone">Function to return <typeparamref name="T"/> if this is a <see cref="None{T}"/></param>
		public T Unwrap(Func<T> ifNone)
			=> this switch
			{
				Some<T> x => x.Value,
				None<T> _ => ifNone(),
				_ => throw new Exception() // as Option<T> is internal implementation only this should never happen...
			};

		/// <summary>
		/// Folds the value if <see cref="Some{T}"/>, otherwise returns <paramref name="ifNone"/>
		/// </summary>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="fold">Fold function</param>
		/// <param name="ifNone">Value if <see cref="None{T}"/></param>
		public U GetOrElse<U>(Func<T, U> fold, U ifNone)
			=> Match(
				fold,
				() => ifNone
			);

		#region Operators

		/// <summary>
		/// Wrap a value in a <see cref="Some{T}"/>
		/// </summary>
		/// <param name="value">Value</param>
		public static implicit operator Option<T>(T value)
			=> Option.Some(value);

		/// <summary>
		/// Compare an option type with a value type
		/// <para>If <paramref name="l"/> is a <see cref="Some{T}"/> the <see cref="Some{T}.Value"/> will be compared to <paramref name="r"/></para>
		/// </summary>
		/// <param name="l">Option</param>
		/// <param name="r">Value</param>
		public static bool operator ==(Option<T> l, T r)
			=> l switch
			{
				Some<T> x => Equals(x.Value, r),
				None<T> _ => false,
				_ => throw new Exception() // as Option<T> is internal implementation only this should never happen...
			};

		/// <summary>
		/// Compare an option type with a value type
		/// <para>If <paramref name="l"/> is a <see cref="Some{T}"/> the <see cref="Some{T}.Value"/> will be compared to <paramref name="r"/></para>
		/// </summary>
		/// <param name="l">Option</param>
		/// <param name="r">Value</param>
		public static bool operator !=(Option<T> l, T r)
			=> l switch
			{
				Some<T> x => !Equals(x.Value, r),
				None<T> _ => true,
				_ => throw new Exception() // as Option<T> is internal implementation only this should never happen...
			};

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
		/// If this is a <see cref="Some{T}"/> get the hash code from <see cref="Some{T}.Value"/>, otherwise use base method
		/// </summary>
		public override int GetHashCode()
			=> this switch
			{
				Some<T> x => "Some".GetHashCode() ^ typeof(T).GetHashCode() ^ x.GetHashCode(),
				None<T> _ => "None".GetHashCode() ^ typeof(T).GetHashCode(),
				_ => throw new Exception() // as Option<T> is internal implementation only this should never happen...
			};

		public int GetHashCode(IEqualityComparer comparer)
			=> this switch
			{
				Some<T> x => "Some".GetHashCode() ^ typeof(T).GetHashCode() ^ comparer.GetHashCode(x.Value),
				None<T> _ => "None".GetHashCode() ^ typeof(T).GetHashCode(),
				_ => throw new Exception() // as Option<T> is internal implementation only this should never happen...
			};

		#endregion
	}
}
