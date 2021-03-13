// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Jeebs.Exceptions;
using static F.OptionF;

namespace Jeebs
{
	/// <summary>
	/// Holds Message classes for <see cref="Option{T}"/>
	/// </summary>
	public abstract partial class Option
	{
		internal Option() { }
	}

	/// <summary>
	/// Option type - enables null-safe returning by wrapping value in <see cref="Some{T}"/> and null in <see cref="None{T}"/>
	/// </summary>
	/// <typeparam name="T">Option value type</typeparam>
	public abstract partial class Option<T> : Option
	{
		/// <summary>
		/// Return as <see cref="Option{T}"/> wrapped in <see cref="Task{TResult}"/>
		/// </summary>
		public Task<Option<T>> AsTask =>
			Task.FromResult(this);

		internal Option() { }

		/// <summary>
		/// Return:
		///    Value (if this is <see cref="Some{T}"/> and Value is not null)
		///    Reason (if this is <see cref="None{T}"/> and it has a reason)
		/// </summary>
		public override string ToString() =>
			F.OptionF.Switch(
				this,
				some: v =>
					v?.ToString() switch
					{
						string value =>
							value,

						_ =>
							"Some: " + typeof(T).ToString()
					},

				none: r =>
					r?.ToString() switch
					{
						string reason =>
							reason,

						_ =>
							"None: " + typeof(T).ToString()
					}
			);

		#region Operators

		/// <summary>
		/// Wrap a value in a <see cref="Some{T}"/>
		/// </summary>
		/// <param name="value">Value</param>
		public static implicit operator Option<T>(T value) =>
			Return(value);

		/// <summary>
		/// Compare an option type with a value type
		/// <para>If <paramref name="l"/> is a <see cref="Some{T}"/> the <see cref="Some{T}.Value"/> will be compared to <paramref name="r"/></para>
		/// </summary>
		/// <param name="l">Option</param>
		/// <param name="r">Value</param>
		public static bool operator ==(Option<T> l, T r) =>
			F.OptionF.Switch(
				l,
				some: v => Equals(v, r),
				none: _ => false
			);

		/// <summary>
		/// Compare an option type with a value type
		/// <para>If <paramref name="l"/> is a <see cref="Some{T}"/> the <see cref="Some{T}.Value"/> will be compared to <paramref name="r"/></para>
		/// </summary>
		/// <param name="l">Option</param>
		/// <param name="r">Value</param>
		public static bool operator !=(Option<T> l, T r) =>
			F.OptionF.Switch(
				l,
				some: v => !Equals(v, r),
				none: _ => true
			);

		#endregion

		#region Equals

		/// <summary>
		/// Compare this <see cref="Option{T}"/> with another object
		/// <para>If both are a <see cref="Some{T}"/> each <see cref="Some{T}.Value"/> will be compared</para>
		/// <para>If both are a <see cref="None{T}"/> this will return true</para>
		/// <para>Otherwise this will return false</para>
		/// </summary>
		/// <param name="other">Object to compare to this <see cref="Option{T}"/></param>
		public override bool Equals(object? other) =>
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
					throw new UnknownOptionException() // as Option<T> is internal implementation only this should never happen...
			};

		#endregion
	}
}
