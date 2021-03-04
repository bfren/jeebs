using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Jm.Option;

namespace Jeebs
{
	/// <summary>
	/// Create option types
	/// </summary>
	public static class Option
	{
		/// <summary>
		/// Create a None option
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		public static None<T> None<T>() =>
			new();

		/// <summary>
		/// Create a Some option, containing a value
		/// <para>If <paramref name="value"/> is null, <see cref="Jeebs.None{T}"/> will be returned instead</para>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="value">Some value</param>
		public static Option<T> Wrap<T>(T value) =>
			value switch
			{
				T x =>
					new Some<T>(x),

				_ =>
					new None<T>().AddReason<SomeValueWasNullMsg>()
			};

		/// <summary>
		/// Wrap <paramref name="value"/> in <see cref="Wrap{T}(T)"/> if <paramref name="predicate"/> is true
		/// <para>Otherwise, will return <see cref="Jeebs.None{T}"/></para>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="predicate">Predicate to evaluate</param>
		/// <param name="value">Function to return value</param>
		public static Option<T> WrapIf<T>(Func<bool> predicate, Func<T> value) =>
			predicate() switch
			{
				true =>
					Wrap(value()),

				false =>
					None<T>()
			};
	}

	/// <summary>
	/// Option type - enables null-safe returning by wrapping value in <see cref="Some{T}"/> and null in <see cref="None{T}"/>
	/// </summary>
	/// <typeparam name="T">Option value type</typeparam>
	public abstract class Option<T> : IEquatable<Option<T>>, IStructuralEquatable
	{
		internal Option() { }

		private U Switch<U>(Func<T, U> some, Func<IMsg?, U> none) =>
			this switch
			{
				Some<T> x =>
					some(x.Value),

				None<T> y =>
					none(y.Reason),

				_ =>
					throw new Exception() // as Option<T> is internal implementation only this should never happen...
			};

		private U Switch<U>(Func<T, U> some, Func<U> none) =>
			Switch(
				some: some,
				none: _ => none()
			);

		/// <summary>
		/// Run a function depending on whether this is a <see cref="Some{T}"/> or <see cref="None{T}"/>
		/// </summary>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="None{T}"/></param>
		public U Match<U>(Func<T, U> some, Func<U> none) =>
			Switch(
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
			Switch(
				some: some,
				none: () => none
			);

		/// <summary>
		/// Use <paramref name="bind"/> to convert the current Option to a new type - if this is a <see cref="Some{T}"/>
		/// </summary>
		/// <typeparam name="U">Next Option value type</typeparam>
		/// <param name="bind">Binding function - will receive <see cref="Some{T}.Value"/> if this is a <see cref="Some{T}"/></param>
		public Option<U> Bind<U>(Func<T, Option<U>> bind) =>
			Switch(
				some: x =>
					bind(x).Switch<Option<U>>(
						some: Option.Wrap,
						none: r => new None<U>(r)
					),

				none: r =>
					new None<U>(r)
			);

		/// <summary>
		/// Use <paramref name="map"/> to convert the current Option to a new type - if this is a <see cref="Some{T}"/>
		/// </summary>
		/// <typeparam name="U">Next Option value type</typeparam>
		/// <param name="map">Mapping function - will receive <see cref="Some{T}.Value"/> if this is a <see cref="Some{T}"/></param>
		public Option<U> Map<U>(Func<T, U> map) =>
			Switch(
				some: v => Option.Wrap(map(v)),
				none: r => new None<U>(r)
			);

		/// <summary>
		/// Unwrap the value of this option - if this is a <see cref="Some{T}"/>
		/// </summary>
		/// <param name="ifNone">Function to return <typeparamref name="T"/> if this is a <see cref="None{T}"/></param>
		public T Unwrap(Func<T> ifNone) =>
			Switch(
				some: x => x,
				none: ifNone
			);

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
			l.Switch(
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
			l.Switch(
				some: x => !Equals(x, r),
				none: () => true
			);

		#endregion

		#region Overrides

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
		public bool Equals(Option<T>? other) =>
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
					throw new Exception() // as Option<T> is internal implementation only this should never happen...
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
					throw new Exception() // as Option<T> is internal implementation only this should never happen...
			};

		#endregion
	}
}
