﻿// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections;

namespace Jeebs
{
	/// <summary>
	/// Option type - enables null-safe returning by wrapping value in <see cref="Some{T}"/> and null in <see cref="None{T}"/>
	/// </summary>
	/// <typeparam name="T">Option value type</typeparam>
	public abstract partial record Option<T> : IEquatable<Option<T>>, IStructuralEquatable
	{
		internal Option() { }

		/// <summary>
		/// Run an action depending on whether this is a <see cref="Some{T}"/> or <see cref="None{T}"/>
		/// </summary>
		/// <param name="some">Action to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Action to run if <see cref="None{T}"/></param>
		public void Switch(Action<T> some, Action none) =>
			SwitchAction(
				some: some,
				none: none
			);

		/// <summary>
		/// Run an action depending on whether this is a <see cref="Some{T}"/> or <see cref="None{T}"/>
		/// </summary>
		/// <param name="some">Action to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Action to run if <see cref="None{T}"/></param>
		public void Switch(Action<T> some, Action<IMsg?> none) =>
			SwitchAction(
				some: some,
				none: none
			);

		/// <summary>
		/// Run a function depending on whether this is a <see cref="Some{T}"/> or <see cref="None{T}"/>
		/// </summary>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="None{T}"/></param>
		public U Match<U>(Func<T, U> some, Func<U> none) =>
			SwitchFunc(
				some: some,
				none: none
			);

		/// <summary>
		/// Run a function depending if this is a <see cref="Some{T}"/> or return value <paramref name="none"/>
		/// </summary>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Value to return if <see cref="None{T}"/></param>
		public U Match<U>(Func<T, U> some, U none) =>
			SwitchFunc(
				some: some,
				none: () => none
			);

		/// <summary>
		/// Use <paramref name="map"/> to convert the current Option to a new type - if this is a <see cref="Some{T}"/>
		/// </summary>
		/// <typeparam name="U">Next value type</typeparam>
		/// <param name="map">Mapping function - will receive <see cref="Some{T}.Value"/> if this is a <see cref="Some{T}"/></param>
		public Option<U> Map<U>(Func<T, U> map) =>
			SwitchFunc(
				some: v => Option.Wrap(map(v)),
				none: r => Option.None<U>(r)
			);

		/// <summary>
		/// Use <paramref name="bind"/> to convert the current Option to a new type - if this is a <see cref="Some{T}"/>
		/// </summary>
		/// <typeparam name="U">Next value type</typeparam>
		/// <param name="bind">Binding function - will receive <see cref="Some{T}.Value"/> if this is a <see cref="Some{T}"/></param>
		public Option<U> Bind<U>(Func<T, Option<U>> bind) =>
			SwitchFunc(
				some: x =>
					bind(x),

				none: r =>
					Option.None<U>(r)
			);

		/// <summary>
		/// Unwrap the value of this option - if this is a <see cref="Some{T}"/>
		/// </summary>
		/// <param name="ifNone">Function to return <typeparamref name="T"/> if this is a <see cref="None{T}"/></param>
		public T Unwrap(Func<T> ifNone) =>
			SwitchFunc(
				some: x => x,
				none: ifNone
			);

		#region Helpers

		private void SwitchAction(Action<T> some, Action<IMsg?> none)
		{
			if (this is Some<T> x)
			{
				some(x.Value);
			}
			else if (this is None<T> y)
			{
				none(y.Reason);
			}
			else
			{
				throw new Jx.Option.UnknownOptionException(); // as Option<T> is internal implementation only this should never happen...
			}
		}

		private void SwitchAction(Action<T> some, Action none) =>
			SwitchAction(
				some: some,
				none: _ => none()
			);

		private U SwitchFunc<U>(Func<T, U> some, Func<IMsg?, U> none) =>
			this switch
			{
				Some<T> x =>
					some(x.Value),

				None<T> y =>
					none.Invoke(y.Reason),

				_ =>
					throw new Jx.Option.UnknownOptionException() // as Option<T> is internal implementation only this should never happen...
			};

		private U SwitchFunc<U>(Func<T, U> some, Func<U> none) =>
			SwitchFunc(
				some: some,
				none: _ => none()
			);

		#endregion

		#region Operators

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
			l.SwitchFunc(
				some: x => Equals(x, r),
				none: () => false
			);

		/// <summary>
		/// Compare an option type with a value type
		/// <para>If <paramref name="l"/> is a <see cref="Some{T}"/> the <see cref="Some{T}.Value"/> will be compared to <paramref name="r"/></para>
		/// </summary>
		/// <param name="l">Option</param>
		/// <param name="r">Value</param>
		public static bool operator !=(Option<T> l, T r) =>
			l.SwitchFunc(
				some: x => !Equals(x, r),
				none: () => true
			);

		#endregion

		#region Overrides

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

		#endregion
	}
}
