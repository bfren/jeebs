using System;
using System.Collections.Generic;
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
		public static Some<T> Some<T>(T value) => new Some<T>(value);

		/// <summary>
		/// Create a None option
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		public static None<T> None<T>() => new None<T>();
	}

	/// <summary>
	/// Option type - enables null-safe returning by wrapping value in <see cref="Some{T}"/> and null in <see cref="None{T}"/>
	/// </summary>
	/// <typeparam name="T">Option value type</typeparam>
	public abstract class Option<T>
	{
		internal Option() { }

		/// <summary>
		/// Get the value of this option
		/// </summary>
		/// <param name="ifNone">Function to return <typeparamref name="T"/> if this is a <see cref="None{T}"/></param>
		public T Unwrap(Func<T> ifNone) => this switch
		{
			Some<T> s => s.Value,
			None<T> _ => ifNone(),
			_ => throw new Exception("This shouldn't happen!")
		};

		/// <summary>
		/// Convert this option to a result:
		/// <para><see cref="Some{T}"/> -> <see cref="IOkV{TValue}"/></para>
		/// <para><see cref="None{T}"/> -> <see cref="IError{TValue}"/></para>
		/// </summary>
		public IR<T> ToResult() => this switch
		{
			Some<T> s => R.OkV(s.Value),
			None<T> _ => R.Error<T>(),
			_ => throw new Exception("This shouldn't happen!")
		};
	}
}
